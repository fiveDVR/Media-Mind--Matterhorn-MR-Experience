using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
[RequireComponent(typeof(LineRenderer))]
public class CableSplineManager : MonoBehaviour
{
    [Header("Rope Connections")]
    [Tooltip("The starting point of the rope. If null, it will default to the Main Camera.")]
    [SerializeField] private Transform startPoint;

    [Tooltip("The destination point that the rope needs to reach.")]
    [SerializeField] private Transform endPoint;

    [Header("Cable Settings")]
    [Tooltip("How much the cable sags downward in the middle.")]
    [SerializeField] private float sagAmount = 1.5f;

    [Tooltip("How many points are used to render the curve. Higher is smoother.")]
    [Range(10, 100)]
    [SerializeField] private int resolution = 30;

    [Tooltip("How fast the cable shoots out towards the target (seconds to complete).")]
    [SerializeField] private float travelDuration = 1.5f;

    [Tooltip("Width of the cable rope.")]
    [SerializeField] private float cableWidth = 0.05f;

    [Tooltip("Material for the cable LineRenderer.")]
    [SerializeField] private Material cableMaterial;

    private SplineContainer splineContainer;
    private LineRenderer lineRenderer;
    private Coroutine drawCoroutine;
    private bool isCableDrawn = false;

    private void Awake()
    {
        splineContainer = GetComponent<SplineContainer>();
        lineRenderer = GetComponent<LineRenderer>();

        // Set up the LineRenderer defaults
        lineRenderer.startWidth = cableWidth;
        lineRenderer.endWidth = cableWidth;
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 0;

        if (cableMaterial != null)
        {
            lineRenderer.material = cableMaterial;
        }

        // Default startPoint to Main Camera if not set
        if (startPoint == null && Camera.main != null)
        {
            startPoint = Camera.main.transform;
        }
    }

    private void Update()
    {
        // If the cable is fully drawn or drawing, we update the start position in real-time
        // to follow the camera/player if they move around.
        if (isCableDrawn && startPoint != null && endPoint != null)
        {
            UpdateSplinePositions();
            RenderCable(1.0f); // Render full cable
        }
    }

    /// <summary>
    /// Call this from a UI Button onClick event to trigger the cable shoot-out animation.
    /// </summary>
    public void TriggerCable()
    {
        if (startPoint == null)
        {
            if (Camera.main != null) startPoint = Camera.main.transform;
        }

        if (startPoint == null || endPoint == null)
        {
            Debug.LogError("[CableSplineManager] Start Point or End Point is missing!");
            return;
        }

        if (drawCoroutine != null)
        {
            StopCoroutine(drawCoroutine);
        }

        drawCoroutine = StartCoroutine(DrawCableRoutine());
    }

    /// <summary>
    /// Hides the cable.
    /// </summary>
    public void ClearCable()
    {
        if (drawCoroutine != null)
        {
            StopCoroutine(drawCoroutine);
            drawCoroutine = null;
        }
        isCableDrawn = false;
        lineRenderer.positionCount = 0;
    }

    private IEnumerator DrawCableRoutine()
    {
        isCableDrawn = false;
        float elapsed = 0.0f;

        // Initialize points
        lineRenderer.positionCount = resolution;

        while (elapsed < travelDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / travelDuration);

            // Update spline shape based on current positions
            UpdateSplinePositions();

            // Render only up to the current progress
            RenderCable(progress);

            yield return null;
        }

        isCableDrawn = true;
        drawCoroutine = null;
    }

    private void UpdateSplinePositions()
    {
        if (splineContainer == null || startPoint == null || endPoint == null) return;

        Spline spline = splineContainer.Spline;
        spline.Clear();

        // Convert world positions to local positions relative to the SplineContainer
        Vector3 localStart = splineContainer.transform.InverseTransformPoint(startPoint.position);
        Vector3 localEnd = splineContainer.transform.InverseTransformPoint(endPoint.position);

        // Calculate a middle sag point
        Vector3 midWorldPoint = (startPoint.position + endPoint.position) / 2.0f + (Vector3.down * sagAmount);
        Vector3 localMiddle = splineContainer.transform.InverseTransformPoint(midWorldPoint);

        // Add the knots (Start, Mid, End)
        spline.Add(new BezierKnot(localStart));
        spline.Add(new BezierKnot(localMiddle));
        spline.Add(new BezierKnot(localEnd));

        // Auto-smooth the tangents so the spline curves nicely
        var allKnots = new SplineRange(0, spline.Count);
        spline.SetTangentMode(allKnots, TangentMode.AutoSmooth);
    }

    private void RenderCable(float progress)
    {
        if (lineRenderer == null || splineContainer == null) return;

        lineRenderer.positionCount = resolution;

        for (int i = 0; i < resolution; i++)
        {
            // Calculate progress along the spline for this specific line segment
            float t = (i / (float)(resolution - 1)) * progress;

            // Evaluate world position along the spline
            Vector3 worldPos = splineContainer.EvaluatePosition(t);

            lineRenderer.SetPosition(i, worldPos);
        }
    }

    // Helper to visualize the connection in the Editor scene view when not playing
    private void OnDrawGizmosSelected()
    {
        if (startPoint != null && endPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(startPoint.position, endPoint.position);
            Vector3 midPoint = (startPoint.position + endPoint.position) / 2.0f + (Vector3.down * sagAmount);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(midPoint, 0.1f);
        }
    }
}
