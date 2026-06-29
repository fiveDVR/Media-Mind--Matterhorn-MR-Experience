using TMPro;
using UnityEngine;

public class QuestionSO : MonoBehaviour
{
    [SerializeField]
    private Question question;

    [SerializeField]
    private TextMeshProUGUI headerline;

    [SerializeField]
    private TextMeshProUGUI[] answerslot;


    private void Awake()
    {
        headerline.text = question.HeaderLine;
        for(int i = 0; i < question.AsnwerSlot.Length; i++)
        {
            answerslot[i].text = question.AsnwerSlot[i];
        }
    }

    public void CheckAnswer(int index)
    {
        if (question.IsCorrectAnswer[index])
        {
            Debug.Log("Correct Answer!");
            GameManager.Instance.CorrectAnswer();
        }
        else
        {
            Debug.Log("Wrong Answer!");
            GameManager.Instance.WrongAnswer();
        }
    }

}
