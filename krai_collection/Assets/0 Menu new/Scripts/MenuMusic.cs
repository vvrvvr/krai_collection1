using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [Space]
    [SerializeField] protected bool _musicOn;

    [Space]
    [SerializeField] protected float _masterVolume = 1;
    [SerializeField] protected float _musicVolume = 1;
    [SerializeField] protected float _typingVolume = 1;
    [SerializeField] protected float _messageVolume = 1;

    [Space]
    //[FMODUnity.EventRef] [SerializeField] protected string _typing;
    [FMODUnity.EventRef] [SerializeField] protected string _music;
    [FMODUnity.EventRef] [SerializeField] protected string _messageOneShot;

    protected FMOD.Studio.EventInstance _musicEvent;
    protected FMOD.Studio.EventInstance _typingEvent;


    void Start()
    {
        if (_musicOn)
            PlayMusic();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FadeMusic(true);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            FadeMusic(false);
        }
    }


    public void PlayMusic()
    {
        if (_music == "") return;

        _musicEvent = FMODUnity.RuntimeManager.CreateInstance(_music);
        _musicEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        _musicEvent.setVolume(_musicVolume * _masterVolume);
        _musicEvent.start();
        _musicEvent.setParameterByName("music_fade", 1f);
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

    }
}
