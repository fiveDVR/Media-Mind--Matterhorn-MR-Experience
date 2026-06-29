using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private GameObject questionPanelParent;

    [SerializeField]
    private List<GameObject> questionCampaignMCQs, questionMedicalMCQs;

    [SerializeField]
    private GameObject[] buttonInteractable;

    public event Action OnCorrectAnswer;
    public event Action OnWrongAnswer;

    bool isQuestionChoosen;

    int buttonIndex = 0;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    public void AppearsQuestion()
    {
        questionPanelParent.SetActive(true);

        isQuestionChoosen = !isQuestionChoosen;

        Debug.Log("isquestion choosen:::: "  + isQuestionChoosen);

        if (isQuestionChoosen)
        {
            int randomIndex;
            randomIndex = UnityEngine.Random.Range(0, questionCampaignMCQs.Count);
            Debug.Log("Random index: " + randomIndex);

            questionCampaignMCQs[randomIndex].SetActive(true);
        }
        else
        {
            int randomIndex;
            randomIndex = UnityEngine.Random.Range(0, questionMedicalMCQs.Count);
            Debug.Log("Random index: " + randomIndex);

            questionMedicalMCQs[randomIndex].SetActive(true);
        }

    }

    public void HideQuestions()
    {
       

        foreach(GameObject child in questionCampaignMCQs)
        {
            child.SetActive(false);
        }

        foreach (GameObject child in questionMedicalMCQs)
        {
            child.SetActive(false);
        }

        questionPanelParent.SetActive(false);
    }


    public void AppearingHook()
    {
        HideQuestions();
        buttonInteractable[buttonIndex].SetActive(true);
        buttonIndex++;
        Debug.Log("Button index: " + buttonIndex);
    }

    public void CorrectAnswer()
    {
        Debug.Log("Correct answer selected");
        // Add logic for correct answer
        OnCorrectAnswer.Invoke();
    }

    public void WrongAnswer()
    {
        Debug.Log("Wrong answer selected");
        // Add logic for wrong answer
        OnWrongAnswer.Invoke();
    }

    public bool IsQuestionPanelActive()
    {
        foreach (GameObject panel in questionCampaignMCQs)
        {
            if (panel.activeSelf)
            {
                return true;
            }
        }

        foreach (GameObject panel in questionMedicalMCQs)
        {
            if (panel.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
}
