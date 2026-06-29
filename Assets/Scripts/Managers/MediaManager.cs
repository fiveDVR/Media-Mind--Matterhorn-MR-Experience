using UnityEngine;
using UnityEngine.Video;

public class MediaManager : MonoBehaviour {

    public static MediaManager Instance;

    public AudioSource audioSource;

    public VideoPlayer videoPlayer;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject); 
        }
        else {
            Instance = this;
        }
    }

}
