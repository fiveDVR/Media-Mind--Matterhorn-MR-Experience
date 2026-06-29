using System;
using UnityEngine;

public class successandFailureAction : MonoBehaviour {
    public static Action successAction;
    public static Action failureAction;
    public static Action placedSuccessfully;


    public void Success() {

        successAction?.Invoke();
        Debug.Log("Right Choice Selected");
    }

    public void Failure() {

        failureAction?.Invoke();
        Debug.Log("False Choice Selected");

    }

}
