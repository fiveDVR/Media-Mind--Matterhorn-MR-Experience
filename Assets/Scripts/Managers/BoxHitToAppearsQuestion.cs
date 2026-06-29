using UnityEngine;

public class BoxHitToAppearsQuestion : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player has hit the box, question appears!");
        GameManager.Instance.AppearsQuestion();
        GameManager.Instance.WrongAnswer();   
        GetComponent<Collider>().enabled = false;
    }
}
