using UnityEngine;

/// <summary>
/// Follows only the Y position of main camera, keeping X and Z fixed at their initial values.
/// </summary>
public class FollowCameraY : MonoBehaviour {
    [Tooltip("Main Camera")]
    public Transform target;

    [Tooltip("Player to follow on the Z axis")]
    public Transform player;

    [Tooltip("X offset from the camera's position.")]
    public float xOffset = 0f;

    [Tooltip("Y offset from the camera's position.")]
    public float yOffset = -0.2f;

    [Tooltip("Z offset from the player's position.")]
    public float zOffset = 0f;

    [Tooltip("How smoothly the object follows. Higher = faster.")]
    [Range(1f, 20f)]
    public float smoothSpeed = 5f;

    void Start()
    {
        if (target == null)
            target = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (target == null || player == null)
            return;

        float targetX = target.position.x + xOffset;
        float targetY = target.position.y + yOffset;
        float targetZ = player.position.z + zOffset;

        float smoothedX = Mathf.Lerp(transform.position.x, targetX, smoothSpeed * Time.deltaTime);
        float smoothedY = Mathf.Lerp(transform.position.y, targetY, smoothSpeed * Time.deltaTime);
        float smoothedZ = Mathf.Lerp(transform.position.z, targetZ, smoothSpeed * Time.deltaTime);

        transform.position = new Vector3(
            smoothedX,
            smoothedY,
            smoothedZ
        );
    }

}
