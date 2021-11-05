using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using krai_menu;

namespace krai_menu
{

    public class MenuMusic : MonoBehaviour
    {
        [Space]
        [SerializeField] protected bool _musicOn;
        [SerializeField] protected bool _ambienceOn;

        [Space]
        [SerializeField] protected float _masterVolume = 1;
        [SerializeField] protected float _musicVolume = 1;
        [SerializeField] protected float _typingVolume = 1;
        [SerializeField] protected float _messageVolume = 1;
        [SerializeField] protected float _ambienceVolume = 1;

        [Space]
        //[FMODUnity.EventRef] [SerializeField] protected string _typing;
        [FMODUnity.EventRef] [SerializeField] protected string _music;
        [FMODUnity.EventRef] [SerializeField] protected string _typing;
        [FMODUnity.EventRef] [SerializeField] protected string _ambience;
        [FMODUnity.EventRef] [SerializeField] protected string _messageOneShot;

        protected FMOD.Studio.EventInstance _musicEvent;
        protected FMOD.Studio.EventInstance _typingEvent;
        protected FMOD.Studio.EventInstance _ambienceEvent;


        void Start()
        {
            if (_musicOn)
                PlayMusic();
            if (_ambienceOn)
                PlayAmbience();

            StartTypingEvent();

        }

        private void OnDestroy()
        {
            _typingEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _musicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _ambienceEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        FadeMusic(true);
        //    }
        //    if (Input.GetKeyDown(KeyCode.V))
        //    {
        //        FadeMusic(false);
        //    }
        //}


        public void PlayMusic()
        {
            if (_music == "") return;

            _musicEvent = FMODUnity.RuntimeManager.CreateInstance(_music);
            _musicEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            _musicEvent.setVolume(_musicVolume * _masterVolume);
            _musicEvent.start();
            _musicEvent.setParameterByName("music_fade", 1f);
        }

        public void PlayAmbience()
        {
            if (_ambience == "") return;

            _ambienceEvent = FMODUnity.RuntimeManager.CreateInstance(_ambience);
            _ambienceEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            _ambienceEvent.setVolume(_ambienceVolume * _masterVolume);
            _ambienceEvent.start();
        }

        public void StartTypingEvent()
        {
            if (_typing == "") return;

            _typingEvent = FMODUnity.RuntimeManager.CreateInstance(_typing);
            _typingEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            _typingEvent.setVolume(_typingVolume * _masterVolume);
            _typingEvent.start();
            _typingEvent.setPaused(true);
        }

        public void PlayTypingSound(bool isPlay)
        {
            if (isPlay)
                _typingEvent.setPaused(false);
            else
                _typingEvent.setPaused(true);
        }

        public void FadeMusic(bool isFade)
        {
            if (isFade)
                _musicEvent.setParameterByName("music_fade", 0f);
            else
                _musicEvent.setParameterByName("music_fade", 1f);

        }

        public void PlayMessageSound()
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(_messageOneShot, this.gameObject);
        }
    }
}
