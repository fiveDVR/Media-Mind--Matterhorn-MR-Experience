using UnityEngine;

public class HarnessFollowCameraY: MonoBehaviour
{
    [Header("VR Camera / Player")]
    public Transform cameraTarget;

    [Header("Position Offset")]
    public Vector3 positionOffset = new Vector3(0f, -0.4f, -0.25f);

    [Header("Freeze Position Axis")]
    public bool freezeX = false;
    public bool freezeY = false;
    public bool freezeZ = false;

    [Header("Smooth Follow")]
    [Range(1f, 20f)]
    public float smoothSpeed = 8f;

    [Header("Rotation")]
    public bool followYawRotation = true;
    public float rotationOffsetY = 0f;

    private Vector3 initialPosition;

    private void Start()
    {
        if (cameraTarget == null && Camera.main != null)
            cameraTarget = Camera.main.transform;

        initialPosition = transform.position;
    }

    private void LateUpdate()
    {
        if (cameraTarget == null)
            return;

        Vector3 targetPosition = cameraTarget.position + cameraTarget.rotation * positionOffset;

        if (freezeX)
            targetPosition.x = initialPosition.x;

        if (freezeY)
            targetPosition.y = initialPosition.y;

        if (freezeZ)
            targetPosition.z = initialPosition.z;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        if (followYawRotation)
        {
            Vector3 euler = transform.eulerAngles;
            euler.y = cameraTarget.eulerAngles.y + rotationOffsetY;
            transform.eulerAngles = euler;
        }
    }
}

