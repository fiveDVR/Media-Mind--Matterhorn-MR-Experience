using UnityEngine;

public class Mountaineering : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveDistance = 3.0f;
    [SerializeField] private float smoothSpeed = 5.0f;

    private Vector3 targetPosition;

    bool isClimbing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize targetPosition to the initial position of the GameObject
        targetPosition = transform.position;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnCorrectAnswer += StartClimbing;
        GameManager.Instance.OnWrongAnswer += StopClimbing;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnCorrectAnswer -= StartClimbing;
        GameManager.Instance.OnWrongAnswer -= StopClimbing;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClimbing)
        {
            ClimbMountain();
        }    
    }

    private void ClimbMountain()
    {   
         Debug.Log("Mountaineering action triggered!");
         targetPosition += new Vector3(0, moveDistance, 0);
       
        // Smoothly interpolate towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }

    public void StartClimbing()
    {
        isClimbing = true;
    }

    public void StopClimbing()
    {
        isClimbing = false;
        Debug.Log("Mountaineering action stopped!");
    }
}
