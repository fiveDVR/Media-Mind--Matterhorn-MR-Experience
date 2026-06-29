using UnityEngine;
using UnityEngine.UI;

public class ButtonStep : Step {
   [SerializeField] Button[] buttons;
    public override void Activate() {
        ButtonActivation(true);
        base.Activate();
    }

  

    public override void Reactivate() {
        ButtonActivation(true);
        base.Reactivate();
    }
    public override void Succeeded() {

        base.Succeeded();
    }
    public override void Failed() {
        ButtonActivation(false);
        base.Failed();
    }

    private void ButtonActivation(bool active) {
        foreach (var button in buttons) {
            button.gameObject.SetActive(true);
            button.interactable = active;

        }
    }

}
