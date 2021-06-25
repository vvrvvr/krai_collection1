using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace krai_room
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource sound;
        [SerializeField]
        private AudioSource music;

        [SerializeField]
        private List<Sounds> gameSounds;

        private int currentSound;
        private int currentMusic;

        private int currentStage;
        void Start()
        {
            currentSound = 0;
            currentMusic = 0;
            currentStage = 0;
            if (sound != null)
            {
                sound.volume = SettingsMenu.GetSoundsSettings();
                PlaySoundOnStage(currentStage);
            }
            if (music != null)
                music.volume = SettingsMenu.GetMusicSettings();

            // PlayMusicOnStage(currentStage);
            ImagesManager.OnNextStage.AddListener(() => NextStage());
        }


        public void UpdateAudioSettings()
        {
            if (sound != null)
            {
                sound.volume = SettingsMenu.GetSoundsSettings();
            }
            if (music != null)
                music.volume = SettingsMenu.GetMusicSettings();
        }
        public void PlaySoundOnStage(int stage)
        {
            currentSound = 0;
            currentStage = stage;
            sound.clip = gameSounds[stage].Sound[currentSound];
            StartCoroutine(FadeIn(sound));
        }

        public void NextSound(int stage)
        {
            if (gameSounds[currentStage].Sound.Count > currentSound)
            {
                currentSound++;
            }
            else currentSound = 0;
            sound.clip = gameSounds[stage].Sound[currentSound];
            StartCoroutine(FadeIn(sound));
        }

        public void PlayMusicOnStage(int stage)
        {
            currentMusic = 0;
            currentStage = stage;
            music.clip = gameSounds[stage].Music[currentMusic];
            StartCoroutine(FadeIn(music));
        }

        public void NextMusic(int stage)
        {
            if (gameSounds[currentStage].Music.Count > currentMusic)
            {
                currentMusic++;
            }
            else currentMusic = 0;
            music.clip = gameSounds[stage].Music[currentSound];
            FadeIn(music);
        }

        private void NextStage()
        {
            currentStage++;
            //PlayMusicOnStage(currentStage);
            PlaySoundOnStage(currentStage);
        }
        private IEnumerator FadeIn(AudioSource source)
        {
            source.Play();
            source.volume = 0;
            for (float i = 0; i < 1; i += 0.002f)
            {
                source.volume = i;
                yield return null;
            }
        }


    }

    [System.Serializable]
    class Sounds
    {
        public int Stage;
        public List<AudioClip> Music;
        public List<AudioClip> Sound;
    }
}
