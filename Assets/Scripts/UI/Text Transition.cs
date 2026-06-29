using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTransition : MonoBehaviour {
    int roundIdx; // the order of the round in audio 
    [SerializeField] int roundmax; // how many rounds are there 



    [SerializeField] Button[] choiceButtons;

    [SerializeField]
    Color32[] choiceColors;


    TextMeshProUGUI roundText;

    void Start() {
        roundText = GetComponent<TextMeshProUGUI>();
        roundIdx = 0;
        ChangeRoundText();

    }

    public void ChangeRoundText() {
        AssignChoices();
        roundIdx++;
        if(roundIdx<=roundmax){ roundText.text = $" Round {roundIdx} of {roundmax}"; }

    }

    void AssignChoices() {
        for (int i = 0; i < choiceButtons.Length; i++) {
            int choiceIndex = i;
            choiceButtons[choiceIndex].onClick.RemoveAllListeners();
            if (i == roundIdx) {
                choiceButtons[choiceIndex].onClick.AddListener(Success);
                ColorBlock buttonColors = choiceButtons[choiceIndex].colors;
                buttonColors.pressedColor = choiceColors[0];
                choiceButtons[choiceIndex].colors = buttonColors;

            }
            else {
                choiceButtons[choiceIndex].onClick.AddListener(Success);
                ColorBlock buttonColors = choiceButtons[choiceIndex].colors;
                buttonColors.pressedColor = choiceColors[1];
                choiceButtons[choiceIndex].colors = buttonColors;
            }
        }
    }

    public void Success() {

        successandFailureAction.successAction?.Invoke();
        Debug.Log("Right Choice Selected");
    }

    public void Failure() {

        successandFailureAction.failureAction?.Invoke();
        Debug.Log("False Choice Selected");

    }
}
