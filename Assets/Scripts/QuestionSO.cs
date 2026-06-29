using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionSO : MonoBehaviour
{
    [SerializeField]
    private Question question;

    [SerializeField]
    private TextMeshProUGUI headerline;

    [SerializeField]
    private TextMeshProUGUI[] answerslot;

    [SerializeField]
    private AudioSource[] audioclips;
    public Image[] images;
    public Sprite sprite;

    

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

            audioclips[0].Play();
            //GameManager.Instance.CorrectAnswer();
            Invoke(nameof(DelayAnswer), 0.5f); // Delay the correct answer action by 1 second
        }
        else
        {
            Debug.Log("Wrong Answer!");
            audioclips[1].Play();
            images[index].sprite = sprite;
            //GameManager.Instance.WrongAnswer();

            Invoke(nameof(DelayAnswer), 0.5f);
        }
    }

    private void DelayAnswer()
    {
        GameManager.Instance.AppearingHook();
    }
}
