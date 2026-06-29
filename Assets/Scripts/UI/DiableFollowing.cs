using UnityEngine;

public class DiableFollowing : MonoBehaviour {
    FollowCameraY followCameraY;

    void Awake() {
        followCameraY = GetComponent<FollowCameraY>();
    }

    public void DisableFollowCameraY() {
        followCameraY.enabled = false;

    }
}
