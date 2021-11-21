using UnityEngine.UI;
using UnityEngine;

public class UiTextAndExitButtonPosition : MonoBehaviour
{
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject bottomLeftText;
    [SerializeField] private GameObject bottomRightText;
    private Vector3 exitPosition = new Vector3(901f, 517f, 0f);
    private Vector3 bottomLeftPosition = new Vector3(-870f, -511f, 0f);
    private Vector3 bottomRightPosition = new Vector3(703f, -514f, 0f);
    void Start()
    {
        var aspect = Camera.main.aspect;
        if(aspect > 1.78f)
        {
            AnchorPreset(exitButton, exitPosition);
            AnchorPreset(bottomLeftText, bottomLeftPosition);
            AnchorPreset(bottomRightText, bottomRightPosition);
            Debug.Log("changed anchor aspect");
        }
    }

    void AnchorPreset(GameObject uiObject, Vector3 rectTransformPosition)
    {
        RectTransform uitransform = uiObject.GetComponent<RectTransform>();
        uitransform.anchorMin = new Vector2(0.5f, 0.5f);
        uitransform.anchorMax = new Vector2(0.5f, 0.5f);
        uitransform.pivot = new Vector2(0.5f, 0.5f);
        uitransform.localPosition = rectTransformPosition;
    }

}
