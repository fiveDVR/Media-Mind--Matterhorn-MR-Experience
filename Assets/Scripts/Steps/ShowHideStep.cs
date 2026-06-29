using UnityEngine;

public class ShowHideStep : Step {

    [SerializeField] GameObject[] gameObjects;

    [SerializeField] bool showOrHide;


    public override void Activate() {
        GameObjectVisibility(showOrHide);
        base.Activate();
    }



    public override void Reactivate() {
        GameObjectVisibility(showOrHide);
        base.Reactivate();
    }
    public override void Succeeded() {

        base.Succeeded();
    }
    public override void Failed() {
        GameObjectVisibility(!showOrHide);
        base.Failed();
    }

    void GameObjectVisibility(bool active) {
        foreach (var gameObject in gameObjects) {
            if (gameObject.activeSelf != active) {
                gameObject.SetActive(active);
            }


        }

        if (autoStep) {
            StepManager.Instance.OnStepSuccess();
        }

    }


}
