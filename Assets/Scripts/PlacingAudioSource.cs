using UnityEngine;

public class PlacingAudioSource : MonoBehaviour
{
    public Transform handTransform;

    private void OnEnable() {
        transform.position = handTransform.position;
    }
}
