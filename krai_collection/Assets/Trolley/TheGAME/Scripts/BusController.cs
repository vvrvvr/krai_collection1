using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusController : MonoBehaviour
{
    [SerializeField] protected AxleInfo[] carAxis = new AxleInfo[2];
    [SerializeField] protected float carSpeed;
    [SerializeField] protected float steerAngle;

    protected float _horInput;
    protected float _vertInput;
    protected MusicBox _musicBox;

    protected readonly float _maxRPM = 32000;

	protected void Start()
	{
        _musicBox = GameObject.FindGameObjectWithTag("MusicBox").GetComponent<MusicBox>();
        StartCoroutine(TireSound());
    }

	protected void FixedUpdate()
	{
        _horInput = Input.GetAxis("Horizontal");
        _vertInput = Input.GetAxis("Vertical");

        Acclererate();
	}


    protected void Acclererate ()
	{
		foreach (AxleInfo axle in carAxis)
		{
            if (axle.steering)
			{
                axle.rightWheel.steerAngle = steerAngle * _horInput;
                axle.leftWheel.steerAngle = steerAngle * _horInput;
            }
            if (axle.motor)
			{
                axle.rightWheel.motorTorque = carSpeed * _vertInput;
                axle.leftWheel.motorTorque = carSpeed * _vertInput;
            }

            VisualEheelsToCollider(axle.rightWheel, axle.visRightWheel);
            VisualEheelsToCollider(axle.leftWheel, axle.visLeftWheel);
        }
    }

    protected void VisualEheelsToCollider (WheelCollider col, Transform visWhell)
	{
        Vector3 position;
        Quaternion rotation;

        col.GetWorldPose(out position, out rotation);

        visWhell.position = position;
        visWhell.rotation = rotation;
	}

    protected IEnumerator TireSound()
	{
		do
		{
            var rpm = Mathf.Max(carAxis[0].rightWheel.rpm, carAxis[0].leftWheel.rpm);
            _musicBox.PlayEngineSound(Mathf.Abs(rpm / _maxRPM));
            Debug.Log(rpm);
            yield return new WaitForFixedUpdate();
		} while (true);
	}
}

[System.Serializable]
public class AxleInfo 
{
    public WheelCollider rightWheel;
    public WheelCollider leftWheel;

    public Transform visRightWheel;
    public Transform visLeftWheel;

    public bool steering;
    public bool motor;
}
