﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
	[Space]
	[SerializeField] protected bool _ambienceOn;
	[SerializeField] protected bool _musicOn;
	[SerializeField] protected bool _voicesOn;

	[Space]
	[SerializeField] protected float _masterVolume = 1;
	[SerializeField] protected float _ambienceVolume = 1;
	[SerializeField] protected float _musicVolume = 1;
	[SerializeField] protected float _voicesVolume = 1;
	[SerializeField] protected float _tireVolume = 1;

	[Space]
	[FMODUnity.EventRef] [SerializeField] protected string _ambience;
	[FMODUnity.EventRef] [SerializeField] protected string _music;
	[FMODUnity.EventRef] [SerializeField] protected string _voices;
	[FMODUnity.EventRef] [SerializeField] protected string _scream;
	[FMODUnity.EventRef] [SerializeField] protected string _tire;

	protected FMOD.Studio.EventInstance _ambienceEvent;
	protected FMOD.Studio.EventInstance _musicEvent;
	protected FMOD.Studio.EventInstance _voiceEvent;
	protected FMOD.Studio.EventInstance _screamEvent;
	protected FMOD.Studio.EventInstance _tireEvent;

	private void Start()
	{
		_masterVolume = PlayerPrefs.GetFloat("Volume");

		if (_ambienceOn)
			PlayAmbient();

		if (_musicOn)
			PlayMusic();

		if (_voicesOn)
			PlayVoices();
	}

	private void OnDestroy()
	{
		_ambienceEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		_musicEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		_voiceEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		_screamEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		_tireEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
	}

	public void PlayAmbient()
	{
		if (_ambience == "") return;

		_ambienceEvent = FMODUnity.RuntimeManager.CreateInstance(_ambience);
		_ambienceEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
		_ambienceEvent.setVolume(_ambienceVolume * _masterVolume);
		_ambienceEvent.start();
	}

	public void PlayMusic()
	{
		if (_music == "") return;

		_musicEvent = FMODUnity.RuntimeManager.CreateInstance(_music);
		_musicEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
		_musicEvent.setVolume(_musicVolume * _masterVolume);
		_musicEvent.start();
	}

	public void PlayVoices()
	{
		if (_voices == "") return;

		_voiceEvent = FMODUnity.RuntimeManager.CreateInstance(_voices);
		_voiceEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
		_voiceEvent.setVolume(_voicesVolume * _masterVolume);
		_voiceEvent.start();
	}

	public void PlayScream()
	{
		if (_scream == "") return;

		_screamEvent = FMODUnity.RuntimeManager.CreateInstance(_scream);
		_screamEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
		_screamEvent.setVolume(_voicesVolume * _masterVolume);
		_screamEvent.start();
	}

	public void PlayScream(string scream, float volume)
	{
		if (_scream == "") return;

		_screamEvent = FMODUnity.RuntimeManager.CreateInstance(scream);
		_screamEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
		_screamEvent.setVolume(_voicesVolume * _masterVolume * volume);
		_screamEvent.start();
	}

	public void PlayTireSound(float intensity)
	{
		if (_tire == "") return;

		if (!_tireEvent.isValid())
		{
			_tireEvent = FMODUnity.RuntimeManager.CreateInstance(_tire);
			_tireEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
			_tireEvent.setVolume(_tireVolume * _masterVolume);
			_tireEvent.start();
		}
		else
		{
			_tireEvent.setVolume(_tireVolume * intensity * _masterVolume);
		}

	}

	public void StopAmbient()
	{
		if (_ambienceEvent.isValid())
			StartCoroutine(FadeOutSound ( _ambienceEvent, 2));
	}

	public void StopMusic()
	{
		if (_musicEvent.isValid())
			StartCoroutine(FadeOutSound(_musicEvent, 2));
	}

	public void StopVoices()
	{
		if (_voiceEvent.isValid())
			StartCoroutine(FadeOutSound(_voiceEvent, 2));
	}

	private IEnumerator FadeOutSound(FMOD.Studio.EventInstance sound, float time)
	{
		var timeLeft = time;
		sound.getVolume(out var volume);

		do
		{
			sound.setVolume(volume * (timeLeft / time));

			timeLeft -= Time.fixedDeltaTime;

			yield return new WaitForFixedUpdate();

		} while (timeLeft > 0);

		sound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
	}

	public void ChangeVolume (float value)
	{
		_masterVolume = value;
		PlayerPrefs.SetFloat("Volume", value);

		_ambienceEvent.setVolume(_ambienceVolume*_masterVolume);
		_musicEvent.setVolume(_musicVolume * _masterVolume);
		_voiceEvent.setVolume(_voicesVolume * _masterVolume);
		_screamEvent.setVolume(_voicesVolume * _masterVolume);
		_tireEvent.setVolume(_tireVolume * _masterVolume);
	}
}