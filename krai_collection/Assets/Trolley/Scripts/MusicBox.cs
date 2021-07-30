using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace krai_trol
{
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
		[SerializeField] protected float _engineVolume = 1;

		[Space]
		[SerializeField] private GameObject player;

		[Space]
		[FMODUnity.EventRef] [SerializeField] protected string _ambience;
		[FMODUnity.EventRef] [SerializeField] protected string _music;
		[FMODUnity.EventRef] [SerializeField] protected string _voices;
		[FMODUnity.EventRef] [SerializeField] protected string _scream;
		[FMODUnity.EventRef] [SerializeField] protected string _engine;
		[FMODUnity.EventRef] [SerializeField] protected string _oneShotEngineLeft;
		[FMODUnity.EventRef] [SerializeField] protected string _oneShotEngineRight;


		protected FMOD.Studio.EventInstance _ambienceEvent;
		protected FMOD.Studio.EventInstance _musicEvent;
		protected FMOD.Studio.EventInstance _voiceEvent;
		protected FMOD.Studio.EventInstance _screamEvent;
		protected FMOD.Studio.EventInstance _engineEvent;

		private void Start()
		{
			_masterVolume = PlayerPrefs.GetFloat("Volume");

			if (_ambienceOn)
				PlayAmbient();

			if (_musicOn)
				PlayMusic();

			if (_voicesOn)
				PlayVoices();

			//new 
			PlayEngineSound(true);

		}

		private void OnDestroy()
		{
			_ambienceEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			_musicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			_voiceEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			_screamEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			_engineEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
			//if (player != null)
			FMODUnity.RuntimeManager.AttachInstanceToGameObject(_voiceEvent, Camera.main.transform);
			//else
			//	_voiceEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
			_voiceEvent.setVolume(_voicesVolume * _masterVolume);
			_voiceEvent.start();
		}
		public void OffroadSetParameters(float distance)
		{
			_voiceEvent.setParameterByName("distance", distance);
			_engineEvent.setParameterByName("offroad", distance);
			//Debug.Log("distance " + distance);
		}

		public void ClimbingSetVoicesParameter(float param)
        {
			_voiceEvent.setParameterByName("climbing_distance", param);
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
			//Debug.Log("should play");
			if (player != null)
				_screamEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(player.transform));
			else
				_screamEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
			_screamEvent.setVolume(_voicesVolume * _masterVolume * volume);
			_screamEvent.start();
		}
		public void PlaySlippingSound(int condition)
        {
			if (_engine == "") return;

			if (!_engineEvent.isValid())
			{
				_engineEvent = FMODUnity.RuntimeManager.CreateInstance(_engine);
				if (player != null)
					FMODUnity.RuntimeManager.AttachInstanceToGameObject(_engineEvent, player.transform);
				else
					_engineEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
				_engineEvent.setVolume(_engineVolume * _masterVolume);
				
			}
			else
			{
				if (IsPlaying(_engineEvent))
				{
					_engineEvent.setParameterByName("mountain", condition); 
				}
				else
                {
					if (condition == 1)
					{
						_engineEvent.setParameterByName("mountain", 1); //pressed
						_engineEvent.start();
					}
					
				}
			}
		}

		public void PlayEngineSound(bool isPlay)
		{
			if (_engine == "") return;

			if (!_engineEvent.isValid())
			{
				_engineEvent = FMODUnity.RuntimeManager.CreateInstance(_engine);
				if (player != null)
					FMODUnity.RuntimeManager.AttachInstanceToGameObject(_engineEvent, player.transform);
				else
					_engineEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
				_engineEvent.setVolume(_engineVolume * _masterVolume);
				_engineEvent.start();
			}
			else
			{
				if (isPlay)
				{
					_engineEvent.setParameterByName("rpm", 0.11f);
					_engineEvent.setParameterByName("offroadrpm", 0.11f);
				}
				else
				{
					_engineEvent.setParameterByName("rpm", 0f);
					_engineEvent.setParameterByName("offroadrpm", 0f);
				}
			}

		}
		public float GetEngineEvenTRpm()
		{
			float val;
			float finalval;
			_engineEvent.getParameterByName("rpm", out val, out finalval);
			return finalval;
		}

		public void PlayEngineTurns(int num, bool isOffroad)
		{
			switch (num)
			{
				case -1:
					_engineEvent.setParameterByName("turn_left", 0.11f);
					//if (isOffroad)
					//	FMODUnity.RuntimeManager.PlayOneShotAttached(_oneShotEngineLeft, player);
					//Debug.Log("left sound");
					break;
				case 1:
					_engineEvent.setParameterByName("turn_right", 0.11f);
					//if (isOffroad)
					//	FMODUnity.RuntimeManager.PlayOneShotAttached(_oneShotEngineRight, player);
					//Debug.Log("right sound");
					break;
				case 0:
					_engineEvent.setParameterByName("turn_left", 0f);
					_engineEvent.setParameterByName("turn_right", 0f);
					break;
			}
		}

		public void StopAmbient()
		{
			if (_ambienceEvent.isValid())
				StartCoroutine(FadeOutSound(_ambienceEvent, 2));
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

		public void ChangeVolume(float value)
		{
			_masterVolume = value;
			PlayerPrefs.SetFloat("Volume", value);

			_ambienceEvent.setVolume(_ambienceVolume * _masterVolume);
			_musicEvent.setVolume(_musicVolume * _masterVolume);
			_voiceEvent.setVolume(_voicesVolume * _masterVolume);
			_screamEvent.setVolume(_voicesVolume * _masterVolume);
			_engineEvent.setVolume(_engineVolume * _masterVolume);
		}


		private bool IsPlaying(FMOD.Studio.EventInstance instance)
		{
			FMOD.Studio.PLAYBACK_STATE state;
			instance.getPlaybackState(out state);
			return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
		}
	}
}
