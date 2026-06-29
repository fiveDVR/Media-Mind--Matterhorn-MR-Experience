using System.Collections;
using UnityEngine;

public class HideGif : MonoBehaviour
{
    public float timer = 3f;
    void Start()
    {
        StartCoroutine(HidingGif(timer));

    }

   
    IEnumerator HidingGif(float time) {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
