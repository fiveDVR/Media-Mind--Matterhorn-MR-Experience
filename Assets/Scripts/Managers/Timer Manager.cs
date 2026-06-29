using TMPro;
using UnityEngine;
public class TimerManager : MonoBehaviour {

    [SerializeField] float _constantTime = 120f;

    public int scoreCounter;

    bool isplaced, inTheRightTime;

    public TextMeshProUGUI scoreText;


    float _remainingTime;
    private bool _timerIsRunning = false;

    private void Start() {
        scoreCounter = 0;
        isplaced = inTheRightTime = false;
        successandFailureAction.placedSuccessfully += ObjectInPlace;
        _timerIsRunning = true;
    }

    private void Update() {
        if (_timerIsRunning) {
            if (_remainingTime > 0) {
                _remainingTime -= Time.deltaTime;
                if (isplaced) {
                    isplaced = false;
                    inTheRightTime = true;
                }
            }
            else {
                _remainingTime = 0;
                _timerIsRunning = false;
                if (isplaced) {
                    isplaced = false;

                }
            }
        }
    }


    public void ResetCountdown() {
        if (inTheRightTime) {
            scoreCounter += 100;
        }
        _remainingTime = _constantTime;
        _timerIsRunning = true;
        scoreText.text = scoreCounter.ToString();
        inTheRightTime = false;
        isplaced = false;
        Debug.Log($"Score is now " + scoreText.text);
    }
    /// <summary>
    /// When the object is in its place this function is called
    /// </summary>
    void ObjectInPlace() {
        isplaced = true;
    }

    private void OnDestroy() {
        successandFailureAction.placedSuccessfully -= ObjectInPlace;
    }


}
