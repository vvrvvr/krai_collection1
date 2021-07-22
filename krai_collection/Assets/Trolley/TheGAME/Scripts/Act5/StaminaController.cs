using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private BusController2D busController;
    [SerializeField] private float staminaMaxValue;
    [SerializeField] private float decreaseSpeed;
    [SerializeField] private float increaseSpeed;
    [SerializeField] private float sliderRecoveryIncreaseSpeed;
    private bool hasControl = true;
    void Start()
    {
        if (staminaMaxValue <= 0)
            staminaMaxValue = 1f;
        slider.maxValue = staminaMaxValue;
        slider.value = staminaMaxValue;
    }

    void Update()
    {
        if(hasControl)
        {
            SliderControl();
        }
        else
        {
            SliderRecovery();
        }
    }

    private void SliderControl()
    {
        //input
        var inputHorizontal = Input.GetAxisRaw("Horizontal");
        var inputVertical = Input.GetAxisRaw("Vertical");
        var inputTotal = inputHorizontal;
        if (inputHorizontal == 0)
        {
            inputTotal = inputVertical;
        }

        if (inputTotal != 0)
        {
            if (slider.value > 0)
                slider.value -= Time.deltaTime * decreaseSpeed;
            else
            {
                hasControl = false;
                busController.HasControl = false;
            }
        }
        else
        {
            if (slider.value < staminaMaxValue)
                slider.value += Time.deltaTime * increaseSpeed;
            else
            {
                slider.value = staminaMaxValue;
            }
        }
    }

    private void SliderRecovery()
    {
        slider.value += Time.deltaTime * sliderRecoveryIncreaseSpeed;
        if(slider.value >= staminaMaxValue)
        {
            slider.value = staminaMaxValue;
            hasControl = true;
            busController.HasControl = true;
            Debug.Log("recover");
        }
    }
}
