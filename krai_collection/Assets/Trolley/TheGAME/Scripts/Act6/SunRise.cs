using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRise : MonoBehaviour
{
	[Space]
	[SerializeField] private float speed;
	[Space]
	[SerializeField] private Light directionalLight;
	[Space]
	[SerializeField] private CloudGenerator cloudGenerator;

	private readonly float _directionalLightRotationStartY = -27;

	private float _directionalLightRotation;
	private readonly float _directionalLightRotationMinX = -15;
	private readonly float _directionalLightRotationMaxX = 10;

	//private readonly float _cloudLightRate = 0.28f;
	//private readonly float _cloudLightMin= 0.2f;
	//private readonly float _cloudLightMax = 6;

	//private readonly float _ambientIntensityRate = 0.04f;
	//private readonly float _ambientIntensityMin = 0.5f;
	//private readonly float _ambientIntensityMax = 1.5f;

	private readonly float _ambientGroundColorIntensityRate = 0.04f;
	private readonly Color _ambientGroundColorIntensityMin = Color.black;
	private readonly float _ambientGroundColorIntensityMax = 1.5f;

	private void Start()
	{
		_directionalLightRotation = _directionalLightRotationMinX;

		//cloudGenerator.cloudIntensity = _cloudLightMin;

		//RenderSettings.ambientIntensity = _ambientIntensityMin;

		RenderSettings.ambientSkyColor = _ambientGroundColorIntensityMin;
	}

	private void FixedUpdate()
	{
		if (_directionalLightRotation < _directionalLightRotationMaxX)
		{
			_directionalLightRotation += speed * Time.fixedDeltaTime;
			directionalLight.transform.eulerAngles = new Vector3(1, 0, 0) * _directionalLightRotation + new Vector3(0, 1, 0) * _directionalLightRotationStartY;
		}

		//if (cloudGenerator.cloudIntensity < _cloudLightMax)
		//	cloudGenerator.cloudIntensity += speed * Time.fixedDeltaTime * _cloudLightRate;


		//if (RenderSettings.ambientIntensity < _ambientIntensityMax)
		//	RenderSettings.ambientIntensity += speed * Time.fixedDeltaTime * _ambientIntensityRate;
			
		if (RenderSettings.ambientSkyColor.a < _ambientGroundColorIntensityMax)
			RenderSettings.ambientSkyColor += Color.white * speed * Time.fixedDeltaTime * _ambientGroundColorIntensityRate;

	}
}
