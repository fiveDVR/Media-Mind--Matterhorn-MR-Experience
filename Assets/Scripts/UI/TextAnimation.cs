using DG.Tweening;
using UnityEngine;

public class TextAnimation : MonoBehaviour {


    #region Variables

    private Vector3 _originalScale;
    private Vector3 _scaleTo;
    [SerializeField] float scaleFactor = 2f;
    [SerializeField] float delayFactor = 2f;
    [SerializeField] float durationFactor = 2f;



    #endregion


    void Start() {
        _originalScale = transform.localScale;
        _scaleTo = _originalScale * scaleFactor;

    }


    public void OnScale() {
        transform.DOScale(_scaleTo, durationFactor)
            .SetEase(Ease.InOutSine)
            .SetDelay(delayFactor)
            //.OnComplete(() => {
            //})
            ;

    }
}

