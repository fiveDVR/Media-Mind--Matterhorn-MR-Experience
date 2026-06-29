using UnityEngine;

public class ResettingPosition : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    [Header("Limits")]
    public float xLimit;
    public float yLimit;
    public float zLimit;

    void Awake()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }
   

    public void CheckBoundsOnRelease()
    {
        Vector3 pos = transform.position;

        if (pos.x > xLimit || pos.x < -xLimit 
            ||
            pos.y > yLimit || pos.y <-yLimit 
            ||
            pos.z > zLimit || pos.y < -zLimit)
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;

    }
}