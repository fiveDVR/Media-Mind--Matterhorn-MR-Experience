using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroGame : MonoBehaviour
{

    public void StartGame()
    {
        int sceneindex = 1;
        SceneManager.LoadScene(sceneindex);
    }
}
