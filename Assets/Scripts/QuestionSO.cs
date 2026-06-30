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
    private TextMeshProUGUI reference;

    [SerializeField]
    private AudioSource[] audioclips;
    public Image[] images;

    public Sprite CorrectAnswer;
    public Sprite WrongAnswer;
    public Sprite Defaultsprite;

    

    private void Awake()
    {
        headerline.text = question.HeaderLine;
        for(int i = 0; i < question.AsnwerSlot.Length; i++)
        {
            answerslot[i].text = question.AsnwerSlot[i];
        }

        reference.text = question.Reference;
    }

    public void CheckAnswer(int index)
    {
        if (question.IsCorrectAnswer[index])
        {
            Debug.Log("Correct Answer!");

            audioclips[0].Play();

            for (int i = 0; i < images.Length; i++)
            {
                if (i != index)
                {
                    images[i].sprite = WrongAnswer;
                }
                //Debug.Log("Correct Answer index: " + i + "  " + index);

            }

            images[index].sprite = CorrectAnswer;

            reference.gameObject.SetActive(true);

            //GameManager.Instance.CorrectAnswer();
            Invoke(nameof(DelayAnswer), 3f); // Delay the correct answer action by 1 second
        }
        else
        {
            Debug.Log("Wrong Answer!");
            audioclips[1].Play();

            for (int i = 0; i < images.Length; i++)
            {
                images[i].sprite = WrongAnswer;
            }


            for (int i = 0; i < images.Length; i++)
            {
                if (question.IsCorrectAnswer[i])
                {
                    images[i].sprite = CorrectAnswer;
                }

            }

            reference.gameObject.SetActive(true);

            //images[index].sprite = WrongAnswer;

            //GameManager.Instance.WrongAnswer();

            Invoke(nameof(DelayAnswer), 3f);
        }
    }


private void DelayAnswer()
    {
        for(int i = 0; i < images.Length; i++)
        {
            images[i].sprite = Defaultsprite;
        }

        reference.gameObject.SetActive(false);

        GameManager.Instance.AppearingHook();
    }
}
