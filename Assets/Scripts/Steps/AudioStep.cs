using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AudioStep : Step
{
    [SerializeField] AudioClip audioClip;


    public UnityEvent onAudioFinished;

    bool audioHasPlayed = false;


    public override void Activate() {
        StartCoroutine(PlayAudio());
        base.Activate();
    }

    public override void Reactivate() {
        StartCoroutine(ReplayAudio());
        base.Reactivate();
    }

 

    public override void Succeeded() {
        base.Succeeded();
    }
    public override void Failed() {

        base.Failed();
    }

    private IEnumerator ReplayAudio() {
        if (MediaManager.Instance != null && MediaManager.Instance.audioSource != null) {

            MediaManager.Instance.audioSource.clip = audioClip;
            Debug.Log("Clip played" + stepName);
            yield return new WaitForSeconds(audioClip.length);

            if (autoStep && StepManager.Instance != null) {
                StepManager.Instance.OnStepSuccess();
            }
        }
        else {
            Debug.LogWarning("Media manager or audio source is null");

        }
    }
    private IEnumerator PlayAudio() {
        if (MediaManager.Instance != null && MediaManager.Instance.audioSource != null) 
        {
       
                MediaManager.Instance.audioSource.clip = audioClip;
            if (!audioHasPlayed) {
                MediaManager.Instance.audioSource.Play();
                audioHasPlayed=true;
            }
            Debug.Log("Clip played" + stepName);
                yield return new WaitForSeconds(audioClip.length);
            onAudioFinished?.Invoke();


            if (autoStep && StepManager.Instance != null) {
                StepManager.Instance.OnStepSuccess();
            }

        }
        else 
        {
            Debug.LogWarning("Media manager or audio source is null");

        }
    
    }

}
