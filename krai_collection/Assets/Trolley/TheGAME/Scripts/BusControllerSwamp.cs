using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusControllerSwamp : BusController
{
    [SerializeField] private float reactDelay;
    private float[] horInputs;
    private int inputIndex;
    private int inputIndexMax;

    [SerializeField] private float enginDownDelayMin;
    [SerializeField] private float enginDownDelayMax;
    private bool engineDown;
    private float enginTimeToEnginDown;

    private new void Start()
	{
        inputIndexMax = (int)(reactDelay / Time.fixedDeltaTime);
        horInputs = new float[inputIndexMax];
    }

    private new void FixedUpdate()
    {
        horInputs[inputIndex] =  Input.GetAxis("Horizontal");
        inputIndex = inputIndex == inputIndexMax - 1 ? 0 : inputIndex+1;
        _horInput = horInputs[inputIndex];

        _vertInput = engineDown? 0 : Input.GetAxis("Vertical");

        Acclererate();
        EngineMalfunctions();
    }

    private void EngineMalfunctions()
	{
        if (!engineDown && enginTimeToEnginDown == 0)
            enginTimeToEnginDown = (int)Random.Range(enginDownDelayMin * 50, enginDownDelayMax * 50);

        if (!engineDown && enginTimeToEnginDown  > 0)
            enginTimeToEnginDown--;

        if (!engineDown && enginTimeToEnginDown == 0)
            engineDown = true;

        if (engineDown && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))   ||
            engineDown && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) ||
            engineDown && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) ||
            engineDown && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            )
            engineDown = false;
    }
}
