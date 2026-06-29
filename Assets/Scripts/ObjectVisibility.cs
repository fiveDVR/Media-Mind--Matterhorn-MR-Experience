using System.Collections;
using UnityEngine;

public class ObjectVisibility : MonoBehaviour {

    public GameObject otherHand;
    [SerializeField] AudioSource audioSource;


    private static bool hasTriggered = false;
     bool isRightExercise = false;
    Renderer m_Renderer;

    private void Awake() {
        hasTriggered = false;

        m_Renderer = GetComponent<Renderer>();

    }
    void Update() 
    {
        if (m_Renderer.isVisible && !hasTriggered && isRightExercise) {
            hasTriggered = true;

            StartCoroutine(StopSoundAfterTime(2f));
            successandFailureAction.successAction?.Invoke();
            Debug.Log("[ObjectVisibility] the hands are shown and going to the next step");

        }
    }


    public void SetIsRightEx(bool value) 
    {
        isRightExercise = value;
    }


       
      
    

    public IEnumerator StopSoundAfterTime(float time) {
        yield return new WaitForSeconds(time);  
        audioSource.Stop();

    }


}
