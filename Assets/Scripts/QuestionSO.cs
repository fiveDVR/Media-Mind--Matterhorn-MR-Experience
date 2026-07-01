using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class QuestionSO : MonoBehaviour
{
    [SerializeField]
    private Question question;

    [SerializeField]
    private TextMeshProUGUI headerline;

    [SerializeField]
    private TextMeshProUGUI[] answerslot;

    [SerializeField]
    private GameObject reference;

    [SerializeField]
    private AudioSource[] audioclips;

    public Image[] images;
    public Image[] CheckImage;

    public Sprite CorrectAnswer;
    public Sprite WrongAnswer;
    public Sprite Defaultsprite;

    public Sprite CheckSprite;
    public Sprite WrongSprite;
    public Sprite[] defualtCheckSprite;

    private void Awake()
    {
        headerline.text = question.HeaderLine;
        for(int i = 0; i < question.AsnwerSlot.Length; i++)
        {
            answerslot[i].text = question.AsnwerSlot[i];
        }

        reference.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question.Reference;
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

            CheckImage[index].sprite = CheckSprite;

            images[index].sprite = CorrectAnswer;

            reference.SetActive(true);

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

            CheckImage[index].sprite = WrongSprite;

            reference.SetActive(true);

            //images[index].sprite = WrongAnswer;

            //GameManager.Instance.WrongAnswer();

            Invoke(nameof(DelayAnswer), 3f);
        }
    }


private void DelayAnswer()
    {
        reference.SetActive(false);

        for(int i = 0; i < images.Length; i++)
        {
            images[i].sprite = Defaultsprite;
        }

        for (int i = 0; i < CheckImage.Length; i++)
        {
            CheckImage[i].sprite = defualtCheckSprite[i];
        }


        GameManager.Instance.AppearingHook();
    }
}
