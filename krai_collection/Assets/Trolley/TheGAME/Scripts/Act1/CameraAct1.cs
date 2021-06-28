using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAct1 : MonoBehaviour
{

	[SerializeField] private Transform target;
	private Vector3 offset;
	private Quaternion offsetRotation;
	private Vector3 targetEulerAnglesInMoment;

	[SerializeField] private float sensitivity = 2;

	[SerializeField] private float zoomSensitivity = 6;
	[SerializeField] private float zoomMax = 15;
	[SerializeField] private float zoomMin = 5;
	private float zoomCurrent;

	[SerializeField] private float yRotationMax = 80;
	[SerializeField] private float yRotationMin = 10; 

	float X, Y;

	void Start()
	{
		sensitivity *= 100;
		zoomSensitivity *= 100;

		offset = transform.position - target.position ;

		offsetRotation = Quaternion.Euler(- transform.eulerAngles);

		targetEulerAnglesInMoment = target.eulerAngles;

		zoomCurrent = offset.z / offset.normalized.z;

	}


	private void FixedUpdate()
	{
		
		Y = transform.eulerAngles.y + Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime + target.eulerAngles.y - targetEulerAnglesInMoment.y;

		X = transform.eulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime;
		X = Mathf.Clamp(X, yRotationMin, yRotationMax);

		transform.eulerAngles = new Vector3(X, Y, 0);

		transform.position = target.position + transform.rotation * offsetRotation * offset;
	
		targetEulerAnglesInMoment = target.eulerAngles;


		zoomCurrent -= Input.GetAxis("Mouse ScrollWheel") * Time.fixedDeltaTime * zoomSensitivity;
		zoomCurrent = Mathf.Clamp(zoomCurrent, zoomMin, zoomMax);
		offset = offset.normalized * zoomCurrent;

	}

}