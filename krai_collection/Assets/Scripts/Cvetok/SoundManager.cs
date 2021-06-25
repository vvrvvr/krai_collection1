using System.Collections;
using UnityEngine;
using krai_cvetok;

namespace krai_cvetok
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [Header("sounds")]
        [SerializeField] private AudioClip[] notePickup;
        [SerializeField] private AudioClip[] flowerPickup;
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip endingSound;
        [Header("music")]
        [SerializeField] private float musicVolume;
        //[SerializeField] private AudioClip music1;
        //[SerializeField] private AudioClip music2;
        [SerializeField] private AudioClip[] musicLoops;
        private AudioSource _audioSource;
        private AudioSource _musicSource1;
        private AudioSource _musicSource2;
        [SerializeField] private AudioSource _speechSource;
        private int loopNumber = 0;
        [Header("audio visualisation")]
        private float[] samples = new float[512];
        public static float[] samples8 = new float[8];
        public static SoundManager Singleton;
        [SerializeField] AudioClip[] selfTalks;
        private int selfindex;


        private void Awake()
        {
            Singleton = this;
        }

        void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();

            _musicSource1 = gameObject.AddComponent<AudioSource>();
            _musicSource2 = gameObject.AddComponent<AudioSource>();
            _musicSource1.clip = musicLoops[0];
            _musicSource1.loop = true;
            _musicSource2.loop = true;
            _musicSource1.Play();
            _musicSource1.volume = musicVolume;
            _musicSource2.volume = 0.0f;
        }

        private void Update()
        {
            Get8Samples();
        }

        public IEnumerator CrossFade(AudioSource a, AudioSource b, float seconds)
        {
            float step_interval = seconds / 20.0f;
            float vol_interval = musicVolume / 20.0f;

            var currentTrackTime = a.time;
            b.time = currentTrackTime;
            b.Play();

            for (int i = 0; i < 20; i++)
            {
                a.volume -= vol_interval;
                b.volume += vol_interval;
                yield return new WaitForSeconds(step_interval);
            }
            a.Stop();
        }

        private void Get8Samples()
        {
            if (_musicSource1.volume < 0.3f)
            {
                _musicSource2.GetSpectrumData(samples, 0, FFTWindow.Blackman);
                //Debug.Log("s2");
            }
            else
            {
                _musicSource1.GetSpectrumData(samples, 0, FFTWindow.Blackman);
                //Debug.Log("s1");
            }
            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                float average = 0;
                int sampleCount = (int)Mathf.Pow(2, i) * 2;
                if (i == 7)
                {
                    sampleCount += 2;
                }
                for (int j = 0; j < sampleCount; j++)
                {
                    average += samples[count] * (count + 1);
                    count++;
                }
                average /= count;
                samples8[i] = average * 10;
            }
        }

        public void SwitchTrack()
        {
            if (loopNumber < musicLoops.Length - 1)
            {
                loopNumber++;
                bool play_1 = true;
                if (_musicSource1.volume == 0.0f)
                    play_1 = false;
                if (play_1)
                {
                    _musicSource2.clip = musicLoops[loopNumber];
                    StartCoroutine(CrossFade(_musicSource1, _musicSource2, 2.0f));
                }
                else
                {
                    _musicSource1.clip = musicLoops[loopNumber];
                    StartCoroutine(CrossFade(_musicSource2, _musicSource1, 2.0f));
                }
            }
        }

        public void PlayNotePickup()
        {
            int i = Random.Range(0, notePickup.Length);
            _audioSource.PlayOneShot(notePickup[i]);
        }
        public void PlayFlowerPickup()
        {
            int i = Random.Range(0, flowerPickup.Length);
            _audioSource.PlayOneShot(flowerPickup[i]);
        }
        public void PlayJumpSound()
        {
            _audioSource.PlayOneShot(jumpSound, 0.2f);
        }
        public void PlaySpeech()
        {
            var index = Random.Range(0, selfTalks.Length);
            if (index == selfindex)
            {
                while (index == selfindex)
                {
                    index = Random.Range(0, selfTalks.Length);
                }
            }
            selfindex = index;
            _speechSource.clip = selfTalks[selfindex];
            _speechSource.Play();
        }
        public void StopPlayingSpeech()
        {
            _speechSource.Stop();
        }

        public void NewEndingSound()
        {
            _audioSource.PlayOneShot(endingSound);
        }

    }
}
