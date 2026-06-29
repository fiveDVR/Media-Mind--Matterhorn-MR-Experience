using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PlacementCheckerManager : MonoBehaviour {
    public float distance = 0.0254f;
    public float angle = 3.0f;
    public bool isPlaced, isRotated, isInRightPlace , isGrabbed;
    public XRGrabInteractable grabInteractable;
    

    [Header("Positions and Rotations")]
    [SerializeField] TextMeshProUGUI actualDistanceText;
    [SerializeField] TextMeshProUGUI actualRotationText;
    [SerializeField] TextMeshProUGUI rightDistanceText;
    [SerializeField] TextMeshProUGUI rightRotationText;


    private Renderer outlineRenderer;
    private Renderer childRenderer;


    GameObject otherGameObject;

    private static readonly int EnableGlowID = Shader.PropertyToID("_EnableGlow");
    private static readonly int EnablePulseID = Shader.PropertyToID("_EnablePulse");

    Material[] mats;



    

    private void Start() {
        //isPlaced = false;
        //isRotated = false;
        //grabInteractable.selectEntered.AddListener(OnSelectEntered);
        //grabInteractable.selectExited.AddListener(OnSelectExited);

        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);

        //DebugPositionAndRotation();

    }

    void OnEnable() {
        GetMaterials();
        isPlaced = false;
        isRotated = false;
        

        DebugPositionAndRotation();
    }

    private void DebugPositionAndRotation() {
        rightDistanceText.text = string.Empty;
        rightRotationText.text = string.Empty;
        actualDistanceText.text = string.Empty;
        actualRotationText.text = string.Empty;
    }

    void GetMaterials() {
        outlineRenderer = GetComponent<Renderer>();
        mats = outlineRenderer.materials;
        Debug.Log($"the outline material is {mats[1].name}");
    }

    void OnSelectEntered(SelectEnterEventArgs args) {
        Debug.Log("Select Entered");
        if (/*!isPlaced &&*/ !isGrabbed) {
            successandFailureAction.successAction?.Invoke();
            isGrabbed = true;
        }
    }

    void OnSelectExited(SelectExitEventArgs args) {
        Debug.Log("Select Exited");
        if (!isPlaced) {
            //successandFailureAction.failureAction?.Invoke();
        }
        if (isInRightPlace) {
            SetPulse(1);
            Debug.Log("It is in a pulse");
            successandFailureAction.successAction?.Invoke();
            Destroy(otherGameObject);

        }

    }

    public void SetGlow(float value) {
        mats[1].SetFloat(EnableGlowID, value);
        Debug.Log($"the Glow value is {mats[1].GetFloat(EnableGlowID)}");

        outlineRenderer.materials = mats;
    }

    public void SetPulse(float value) {
        mats[1].SetFloat(EnablePulseID, value);
        Debug.Log($"the Pulse value is {mats[1].GetFloat(EnablePulseID)}");

        outlineRenderer.materials = mats;
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Grabbable Object")) {
            #region Debugging Distance and rotation

            actualDistanceText.text = "Actual Distance " + Vector3.Distance(transform.position, other.transform.position).ToString();
            actualRotationText.text = "Actual Rotation " + Quaternion.Angle(transform.rotation, other.transform.rotation).ToString();
            rightDistanceText.text = "Right Distance " + distance.ToString();
            rightRotationText.text = "Right  Rotation " + angle.ToString();
            #endregion

       //     Debug.Log($"The distance between the object and the placement object is {Vector3.Distance(transform.position, other.transform.position)}");
       //     Debug.Log($"The rotation of the object in respect with the placement object is {Quaternion.Angle(transform.rotation, other.transform.rotation)}");
            CheckDistance(other);
        }

    }

    private void CheckDistance(Collider other) {
        if (Vector3.Distance(transform.position, other.transform.position) < distance && gameObject.activeSelf && other.gameObject.activeSelf) {


            if (!isPlaced) {
                successandFailureAction.successAction?.Invoke();
                Debug.Log($"[PlacementCheckerManager] distance is right");
                Debug.Log($"[PlacementCheckerManager] other collider name {other.name}");

                isPlaced = true;
            }

            if (Quaternion.Angle(transform.rotation, other.transform.rotation) < angle) {
                if (!isRotated) {
                    successandFailureAction.successAction?.Invoke();
                    Debug.Log($"[PlacementCheckerManager] right rotation");

                    isInRightPlace = true;
                    isRotated = true;

                    grabInteractable.enabled = false;
                    
                    if(isPlaced && isRotated) {
                        otherGameObject = other.gameObject;
                        SnapToObject(other);

                    }
                }
            }
            else {
                //When it is in the wrong rotation after it was in the right rotation
                if (isRotated) {
                    //successandFailureAction.failureAction?.Invoke();
                    isRotated = false;
                }

            }
        }
        else {
            //When it is outside after it was inside the right place
            if (isPlaced) {
                if (isRotated) {
                    //successandFailureAction.failureAction?.Invoke();
                    isRotated = false;
                }
                //successandFailureAction.failureAction?.Invoke();
                //isPlaced = false;
            }
        }
    }

    private void SnapToObject(Collider other) {
        if (outlineRenderer.enabled) {
            successandFailureAction.placedSuccessfully?.Invoke();
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;

            outlineRenderer.enabled = false;
            if (transform.childCount > 0) {
                childRenderer = transform.GetChild(0).gameObject.GetComponent<Renderer>();
                childRenderer.enabled = false;
            }

        }
        this.enabled = false;

    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Grabbable Object")) {
            if (isPlaced) {
                //successandFailureAction.failureAction?.Invoke();
                Debug.Log("On Trigger exit");
                //isPlaced = false;
                isRotated = false;
            }
        }
    }

    private void OnDestroy() {
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }
}