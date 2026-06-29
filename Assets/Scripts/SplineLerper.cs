using System.Collections;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class SplineLerper : MonoBehaviour
{
    [Header("Spline Reference")]
    [Tooltip("The Spline Container to move along.")]
    [SerializeField] private SplineContainer splineContainer;

    [Header("Lerp Settings")]
    [Tooltip("Time in seconds to complete the movement.")]
    [SerializeField] private float travelDuration = 3.0f;

    [Tooltip("The Transform representing the starting position along the spline. If null, it defaults to the start of the spline (t = 0).")]
    [SerializeField] private Transform startPoint;

    [Tooltip("The Transform representing the ending position along the spline. If null, it defaults to the end of the spline (t = 1).")]
    [SerializeField] private Transform endPoint;

    [Tooltip("Easing curve for the movement.")]
    [SerializeField] private AnimationCurve speedCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Rotation Settings")]
    [Tooltip("If true, the object will rotate to face the direction of the spline.")]
    [SerializeField] private bool alignRotation = true;

    [Tooltip("Allows you to offset the rotation if the object is facing the wrong direction.")]
    [SerializeField] private Vector3 rotationOffset;

    [Header("Events / Testing")]
    [Tooltip("Automatically start moving when the scene loads.")]
    [SerializeField] private bool playOnStart = false;

    [Tooltip("Press this key in the Editor to test the movement.")]
    [SerializeField] private KeyCode testPlayKey = KeyCode.P;

    private Coroutine lerpCoroutine;
    private bool isMoving = false;
    private float calculatedStartT;
    private float calculatedEndT;

    private void Start()
    {
        if (playOnStart)
        {
            StartLerping();
        }
    }

    private void Update()
    {
        /*
        // Testing in editor
        if (Input.GetKeyDown(testPlayKey))
        {
            StartLerping();
        }
        */
    }

    /// <summary>
    /// Starts the movement along the spline from startT to endT.
    /// Can be triggered by UI Buttons or other scripts.
    /// </summary>
    public void StartLerping()
    {
        if (splineContainer == null)
        {
            Debug.LogError("[SplineLerper] Spline Container is not assigned!", this);
            return;
        }

        if (lerpCoroutine != null)
        {
            StopCoroutine(lerpCoroutine);
        }

        lerpCoroutine = StartCoroutine(LerpAlongSplineRoutine());
    }

    private IEnumerator LerpAlongSplineRoutine()
    {
        isMoving = true;

        // Wait 1 frame to let VR/XR tracking initialize positions (crucial for cameras!)
        yield return null;

        // Calculate start T based on startPoint Transform, otherwise default to 0 (start of spline)
        if (startPoint != null)
        {
            float3 localStart = (float3)splineContainer.transform.InverseTransformPoint(startPoint.position);
            SplineUtility.GetNearestPoint(splineContainer.Spline, localStart, out _, out calculatedStartT);
        }
        else
        {
            calculatedStartT = 0.0f;
        }

        // Calculate end T based on endPoint Transform, otherwise default to 1 (end of spline)
        if (endPoint != null)
        {
            float3 localEnd = (float3)splineContainer.transform.InverseTransformPoint(endPoint.position);
            SplineUtility.GetNearestPoint(splineContainer.Spline, localEnd, out _, out calculatedEndT);
        }
        else
        {
            calculatedEndT = 1.0f;
        }

        float elapsed = 0.0f;

        while (elapsed < travelDuration)
        {
            elapsed += Time.deltaTime;
            float normalizedProgress = Mathf.Clamp01(elapsed / travelDuration);
            
            // Apply speed/easing curve to the progress
            float curveProgress = speedCurve.Evaluate(normalizedProgress);

            // Interpolate between calculatedStartT and calculatedEndT
            float t = Mathf.Lerp(calculatedStartT, calculatedEndT, curveProgress);

            MoveObjectToSplinePoint(t);

            yield return null;
        }

        // Snap to exact destination
        MoveObjectToSplinePoint(calculatedEndT);

        isMoving = false;
        lerpCoroutine = null;
    }

    private void MoveObjectToSplinePoint(float t)
    {
        // Evaluate local position, tangent, and up vector
        if (splineContainer.Evaluate(t, out float3 localPos, out float3 localTangent, out float3 localUp))
        {
            // Transform local spline coordinates to world space coordinates
            Vector3 worldPos = splineContainer.transform.TransformPoint(localPos);
            Vector3 worldTangent = splineContainer.transform.TransformDirection(localTangent);
            Vector3 worldUp = splineContainer.transform.TransformDirection(localUp);

            // Apply position
            transform.position = worldPos;

            // Apply rotation if enabled
            if (alignRotation && worldTangent != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(worldTangent, worldUp);
                transform.rotation = targetRotation * Quaternion.Euler(rotationOffset);
            }
        }
    }

    /// <summary>
    /// Checks if the object is currently moving along the spline.
    /// </summary>
    public bool IsMoving()
    {
        return isMoving;
    }
}
