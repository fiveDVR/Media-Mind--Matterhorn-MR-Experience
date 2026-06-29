using UnityEngine;
using UnityEngine.Splines;

public class BoxHitToAppearsQuestion : MonoBehaviour
{
    public GameObject player;
    public GameObject splineContainer;
    public GameObject buttonInteractable;
 
    int index;

    private float resetCameraValue = 3f; // Example initial position value for the camera's Y-axis
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Rope"))
        {
            Debug.Log("Rope has hit the box, character has flying");
            //player.GetComponent<SplineAnimate>().enabled = true;
            GameManager.Instance.EnabledSplineAnimate();
            
        }
    
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has hit the box, question appears!");
            
            Invoke(nameof(AppearsQuestion), 1f); // Delay the question appearance by 1 second
            //GameManager.Instance.WrongAnswer();
            GetComponent<Collider>().enabled = false;
            splineContainer.SetActive(false);
            //player.GetComponent<SplineAnimate>().enabled = false;
            GameManager.Instance.DisabledSplineAnimate();
            buttonInteractable.SetActive(false);
        }
    }

    private void AppearsQuestion()
    {
        GameManager.Instance.AppearsQuestion();
    }

}
