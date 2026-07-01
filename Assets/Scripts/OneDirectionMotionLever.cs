using UnityEngine;

public class OneDirectionMotionLever : MonoBehaviour
{
    public enum RotationAxis
    {
        X,
        Y,
        Z
    }

    [Header("Rotation Axis")]
    public RotationAxis rotationAxis = RotationAxis.X;

    [Header("Motion")]
    public float startAngle = 0f;
    public float endAngle = 75f;
    public float rotateSpeed = 30f;

    [Header("State")]
    public bool moveOnStart = false;
    public bool isMoving = false;
    public bool reachedEnd = false;

    private Quaternion initialRotation;
    private float currentAngle;

    public GameObject Rope;

    private void Start()
    {
        initialRotation = transform.localRotation;
        currentAngle = startAngle;

        ApplyRotation();

        if (moveOnStart)
            StartLeverMotion();
    }

    private void Update()
    {
        if (!isMoving || reachedEnd)
            return;

        currentAngle = Mathf.MoveTowards(
            currentAngle,
            endAngle,
            rotateSpeed * Time.deltaTime
        );

        ApplyRotation();

        if (Mathf.Approximately(currentAngle, endAngle))
        {
            currentAngle = endAngle;
            reachedEnd = true;
            isMoving = false;
            Rope.SetActive(true);
        }
    }

    public void StartLeverMotion()
    {
        if (reachedEnd)
            return;

        isMoving = true;
    }

    public void StopLeverMotion()
    {
        isMoving = false;
    }

    public void ResetToStart()
    {
        currentAngle = startAngle;
        reachedEnd = false;
        isMoving = false;
        ApplyRotation();
    }

    private void ApplyRotation()
    {
        Vector3 angle = Vector3.zero;

        switch (rotationAxis)
        {
            case RotationAxis.X:
                angle.x = currentAngle;
                break;

            case RotationAxis.Y:
                angle.y = currentAngle;
                break;

            case RotationAxis.Z:
                angle.z = currentAngle;
                break;
        }

        transform.localRotation = initialRotation * Quaternion.Euler(angle);
    }
}
