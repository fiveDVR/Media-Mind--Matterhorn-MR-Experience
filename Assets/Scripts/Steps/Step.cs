using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Step : MonoBehaviour
{

 

    public string stepName;

    public bool autoStep;


    public Step previousStep;
    public Step nextStep;

    public UnityEvent onStepDone;

    public UnityEvent onStepFailed;





    public virtual void Activate() {
       
    }

    public virtual void Reactivate() {
     
    }

    public virtual void Succeeded() {
        onStepDone?.Invoke();
    }

    public virtual void Failed() {
        onStepFailed?.Invoke();
    }






}
