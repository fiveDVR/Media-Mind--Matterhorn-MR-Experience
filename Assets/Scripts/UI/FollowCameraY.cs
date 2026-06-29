using UnityEngine;

/// <summary>
/// Follows only the Y position of main camera, keeping X and Z fixed at their initial values.
/// </summary>
public class FollowCameraY : MonoBehaviour {
    [Tooltip("Main Camera")]
    public Transform target;

    [Tooltip("Y offset from the target's position.")]
    public float yOffset = -0.2f;

    [Tooltip("How smoothly the object follows. Higher = faster.")]
    [Range(1f, 20f)]
    public float smoothSpeed = 5f;

    private float initialX;
    private float initialZ;

    void Start() {
        if (target == null) {
            target = Camera.main.transform;
        }

        // Store the initial X and Z so they never change
        initialX = transform.position.x;
        initialZ = transform.position.z;
    }

    void LateUpdate() {
        if (target == null) return;

        float targetY = target.position.y + yOffset;
        float smoothedY = Mathf.Lerp(transform.position.y, targetY, smoothSpeed * Time.deltaTime);

        transform.position = new Vector3(initialX, smoothedY, initialZ);
    }
}
