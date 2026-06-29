using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Camera Settings")]
    [Tooltip("The camera to shake. If null, it will default to Camera.main.")]
    [SerializeField] private Transform cameraTransform;

    [Header("Default Shake Settings")]
    [Tooltip("How long the camera shakes in seconds.")]
    [SerializeField] private float defaultDuration = 0.5f;

    [Tooltip("The intensity/amplitude of the shake.")]
    [SerializeField] private float defaultMagnitude = 0.15f;

    [Header("Debug/Testing")]
    [Tooltip("Press this key in the Editor to test the shake.")]
    [SerializeField] private KeyCode testShakeKey = KeyCode.K;

    private Vector3 originalLocalPos;
    private Coroutine shakeCoroutine;

    private void Awake()
    {
        // If no camera transform is assigned, try to find the main camera's transform
        if (cameraTransform == null)
        {
            if (Camera.main != null)
            {
                cameraTransform = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning("[CameraShake] No Camera Transform assigned, and Camera.main is null! Shaking this GameObject's transform instead.");
                cameraTransform = transform;
            }
        }
    }

    private void Update()
    {
        /*
        // Test key in editor
        if (Input.GetKeyDown(testShakeKey))
        {
            TriggerShake();
        }
        */
    }

    /// <summary>
    /// Triggers the camera shake using the default inspector settings.
    /// You can link this method directly to a Unity UI Button's onClick event!
    /// </summary>
    public void TriggerShake()
    {
        TriggerShake(defaultDuration, defaultMagnitude);
    }

    /// <summary>
    /// Triggers the camera shake with a custom duration and magnitude.
    /// </summary>
    public void TriggerShake(float duration, float magnitude)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            // Restore the camera to its original position before starting a new shake
            cameraTransform.localPosition = originalLocalPos;
        }

        shakeCoroutine = StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        originalLocalPos = cameraTransform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // Generate a random offset in 3D space
            Vector3 randomOffset = Random.insideUnitSphere * magnitude;

            // Apply the offset relative to the camera's original local position
            cameraTransform.localPosition = originalLocalPos + randomOffset;

            elapsed += Time.deltaTime;

            // Yield until the next frame
            yield return null;
        }

        // Restore the original local position when done
        cameraTransform.localPosition = originalLocalPos;
        shakeCoroutine = null;
    }
}
