using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private GameObject questionPanelParent;

    [SerializeField]
    private List<GameObject> questionCampaignMCQs, questionMedicalMCQs;

    [SerializeField]
    private GameObject[] buttonInteractable;

    [SerializeField]
    private SplineAnimate[] splineAnimate;

    public event Action OnCorrectAnswer;
    public event Action OnWrongAnswer;

    public GameObject TheEndTxt;

    bool isQuestionChoosen;

    int buttonIndex = 0;

    int splineIndex = 0;

    int questionIndex = 0;

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

        if (isQuestionChoosen && questionIndex <= 2)
        {
            int randomIndex;
            randomIndex = UnityEngine.Random.Range(0, questionCampaignMCQs.Count);
            Debug.Log("Random index: " + randomIndex);

            questionCampaignMCQs[randomIndex].SetActive(true);
        }
        else if(!isQuestionChoosen || questionIndex > 2)
        {
            int randomIndex;
            randomIndex = UnityEngine.Random.Range(0, questionMedicalMCQs.Count);
            Debug.Log("Random index: " + randomIndex);

            questionMedicalMCQs[randomIndex].SetActive(true);
        }

        questionIndex++;
        Debug.Log("question index: " + questionIndex); 
    }

    public void DisabledSplineAnimate()
    {
        if (splineIndex >= splineAnimate.Length)
        {
            Debug.Log("spline achieved");
            return;
        }
        splineAnimate[splineIndex].enabled = false;
        splineIndex++;
        Debug.Log("Spline index: " + splineIndex);
    }

    public void EnabledSplineAnimate()
    {
        if(splineIndex >= splineAnimate.Length)
        {
            Debug.Log("spline achieved");
            return;
        }
    
        splineAnimate[splineIndex].enabled = true;
        Debug.Log("Enabled spline animate");
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

        if (buttonIndex >= buttonInteractable.Length)
        {
            TheEndTxt.gameObject.SetActive(true);
            Debug.Log("Button Index Achieved:::");
            return;
        }
    }

    public void AppearingHook()
    {
        HideQuestions();
        if (buttonIndex >= buttonInteractable.Length)
        {
            
            Debug.Log("Button Index Achieved:::");
            return;
        }

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
