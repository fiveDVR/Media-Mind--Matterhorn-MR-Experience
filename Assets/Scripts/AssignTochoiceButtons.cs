using System;
using UnityEngine;

public class AssignTochoiceButtons : MonoBehaviour {
    public WhichNextStep[] whichNextStep;
    int roundIdx;

    private void Start() {
        roundIdx = 0;
    }
    public void addNextStep(int i) {
        if (roundIdx == i) {
            if (whichNextStep[roundIdx].rightStep == whichNextStep[roundIdx].wrongStep) {
                whichNextStep[i].buttonStep.nextStep = whichNextStep[i].rightStep;
                //Debug.Log($"[AssignTochoiceButtons] Exercise {roundIdx} has opened and button next step is {whichNextStep[i].buttonStep.nextStep}");
                StepManager.Instance.currentStep = whichNextStep[i].buttonStep;
            }
            else {
                whichNextStep[roundIdx].buttonStep.nextStep = whichNextStep[roundIdx].rightStep;
                //Debug.Log($"[AssignTochoiceButtons] Exercise {roundIdx} has opened and button next step is {whichNextStep[roundIdx].buttonStep.nextStep}");
                StepManager.Instance.currentStep = whichNextStep[roundIdx].buttonStep;
            }
        }
        else {
            if (whichNextStep[roundIdx].rightStep == whichNextStep[roundIdx].wrongStep) {
                whichNextStep[i].buttonStep.nextStep = whichNextStep[i].wrongStep;
                StepManager.Instance.currentStep = whichNextStep[i].buttonStep;

                //Debug.Log($"[AssignTochoiceButtons] Exercise {roundIdx} has opened and button next step is {whichNextStep[i].buttonStep.nextStep}");
            }
            else {
                whichNextStep[roundIdx].buttonStep.nextStep = whichNextStep[roundIdx].wrongStep;
                //Debug.Log($"[AssignTochoiceButtons] Exercise {roundIdx} has opened and button next step is {whichNextStep[roundIdx].buttonStep.nextStep}");
                StepManager.Instance.currentStep = whichNextStep[roundIdx].buttonStep;

            }
        }
        roundIdx++;
    }
}

[Serializable]
public struct WhichNextStep {
    public Step buttonStep;
    public Step wrongStep;
    public Step rightStep;

}
