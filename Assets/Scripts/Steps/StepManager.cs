using System;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class StepManager : MonoBehaviour {

    public static StepManager Instance;

    public Step currentStep;

    public Step secondStep;


    #region Step States Actions

    public Action stepSucceeded;


    public Action stepFailed;

    #endregion

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
        if (ScenesManager.isSecondTime) {
            currentStep = secondStep;

        }
    }

    private void Start() {

        successandFailureAction.successAction += OnStepSuccess;
        successandFailureAction.failureAction += OnStepFailure;


        if (currentStep != null) {
            currentStep.Activate();
        }

    }

    /// <summary>
    /// Move to the next step
    /// </summary>
    public void OnStepSuccess() {
        if (currentStep == null) return;

        currentStep.Succeeded();
        currentStep = currentStep.nextStep;

        if (currentStep != null) {
            Step next = currentStep;
            next.Activate();
            Debug.Log($"[Step Manager] Moved to next step: {next.name}");
            stepSucceeded?.Invoke();
        }
        else {
            Debug.Log("[Step Manager] No more steps.");
        }
    }

    public void OnStepFailure() {
        if (currentStep == null) return;

        Debug.Log($"[Step Manager] Failed step: {currentStep.name}");
        currentStep.Failed();

        if (currentStep.previousStep != null) {
            currentStep = currentStep.previousStep;
            currentStep.Reactivate();
            stepFailed?.Invoke();
        }
        else {
            Debug.Log("[Step Manager] No previous step to rollback to.");
        }
    }

    private void OnDisable() {
        successandFailureAction.successAction -= OnStepSuccess;
        successandFailureAction.failureAction -= OnStepFailure;
    }
    private void OnDestroy() {
        successandFailureAction.successAction -= OnStepSuccess;
        successandFailureAction.failureAction -= OnStepFailure;
    }



}
