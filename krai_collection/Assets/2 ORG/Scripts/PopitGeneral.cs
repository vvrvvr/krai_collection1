using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using UnityEngine.Audio;
using krai_shooter;

namespace krai_shooter
{
    public class PopitGeneral : MonoBehaviour
    {
        private GameObject parent;
        [Header("general settings")]
        [SerializeField] ParticleSystem explosionPrefab;
        [SerializeField] private float disappearSpeed = 0.5f;
        [SerializeField] private bool isShootable;

        [Header("genre settings")]
        [SerializeField] private bool isGenre;
        [SerializeField] private Text[] buttonTexts = new Text[0];

        [Header("links to art types")]
        [SerializeField] private bool isArt;
        [SerializeField] private GameObject image;
        [SerializeField] private GameObject video;
        [SerializeField] private GameObject text;//Text text;

        [Header("powerup")]
        [SerializeField] private bool isPowerUp;
        [SerializeField] private bool guntiling;
        [SerializeField] private bool abberation;
        [SerializeField] private bool bloom;
        [SerializeField] private bool lensdistortion;
        [SerializeField] private bool coloradj;
        [SerializeField] private bool money;

        [Header("popit type")]
        [SerializeField] private bool isUnwantedArt;
        [SerializeField] private bool isNotification;

        [Header("lifetime")]
        [SerializeField] private float lifetime = 10f; // либо потом брать из геймменеджера, где время менеджериться для всех
        private float currentTime = 0;
        private Vector3 currentScale;
        private bool isOnce = true;
        [HideInInspector] public bool canDisappear = true;
        private Tween one;
        private Tween two;

        //audio output
        [SerializeField] AudioMixerGroup audioMixer;
        private AudioSource _audio;


        private string from = "\n\nот АГЕНТА 10002";

        //intro
        [SerializeField] private bool isIntro;
        // Start is called before the first frame update
        void Start()
        {
            parent = transform.parent.gameObject;
            currentScale = transform.localScale;
            transform.localScale = Vector3.zero;

            if (isArt)
            {
                var type = Random.Range(0, 3);
                switch (type)
                {
                    case 0:
                        image.SetActive(true);
                        var currentImage = image.GetComponent<Image>();
                        currentImage.sprite = ArtSource.Singleton.GetRandomSprite();
                        break;
                    case 1:
                        var currentVideoPlayer = video.GetComponent<VideoPlayer>();
                        currentVideoPlayer.clip = ArtSource.Singleton.GetRandomVideo();
                        _audio = video.AddComponent<AudioSource>();
                        currentVideoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
                        currentVideoPlayer.controlledAudioTrackCount = 1;
                        currentVideoPlayer.EnableAudioTrack(0, true);
                        currentVideoPlayer.SetTargetAudioSource(0, _audio);
                        _audio.outputAudioMixerGroup = audioMixer;
                        _audio.volume = 1.0f;
                        video.SetActive(true);
                        //currentVideoPlayer.
                        break;
                    case 2:
                        text.SetActive(true);
                        var currentText = text.GetComponent<Text>();
                        currentText.text = ArtSource.Singleton.GetRandomText();
                        break;
                    default:
                        break;
                }
            }
            if (isGenre)
            {
                foreach (var item in buttonTexts)
                {
                    item.text = ArtSource.Singleton.GetRandomGenre();
                }
            }
            if (isNotification)
            {
                var currentText = text.GetComponent<Text>();
                currentText.text = ArtSource.Singleton.GetRandomNotification() + from;
            }

            if (lifetime > 0)
                Appear();
            else
            {
                if (one != null)
                    DOTween.Kill(one);
                if (two != null)
                    DOTween.Kill(two);
                DestroyPopit();
            }
        }

        public void StartPopit()
        {
            if (isShootable)
            {
                if (canDisappear) //проверка на срабатывание больше одного раза
                {
                    canDisappear = false;

                    if (isPowerUp) //логика длявсех поверапов
                    {
                        Powerup();
                        Disappear(disappearSpeed);
                    }
                    else if (isUnwantedArt)
                    {
                        CheckIfMoneyReward();
                        Disappear(0.2f);
                        var explosion = Instantiate(explosionPrefab);
                        SoundManager.Singleton.PlayExplosionSound();
                        explosion.transform.position = transform.position;
                        explosion.transform.parent = null;

                    }
                    else
                    {
                        Disappear(disappearSpeed); // если забыл выставить галки для попыта
                    }
                }
            }
        }
        public void StartButtonPopit() // попыты с кнопками
        {
            if (canDisappear) //проверка на срабатывание больше одного раза
            {
                canDisappear = false;
                CheckIfMoneyReward();
                Disappear(disappearSpeed);
                if (isIntro)
                {
                    GameManager.Singleton.isStart = true;
                    GameManager.Singleton.MovePlayer();
                    SoundManager.Singleton.StartMusic();
                }
            }
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= lifetime && isOnce) //из онсе - чтобы ичезновение в апдейте работало только один раз
            {
                Disappear(disappearSpeed);
                isOnce = false;

            }
        }
        private void Powerup()
        {
            if (guntiling)
            {
                ScreenVisuals.Singleton.ChangeGunTiling();
                SoundManager.Singleton.PowerUpSound();
            }
            if (abberation)
            {
                ScreenVisuals.Singleton.Abberation();
                SoundManager.Singleton.PowerUpSound();
            }
            if (bloom)
            {
                ScreenVisuals.Singleton.Bloom();
                SoundManager.Singleton.PowerUpSound();
            }
            if (lensdistortion)
            {
                ScreenVisuals.Singleton.LensDistortion();
            }
            if (coloradj)
            {
                ScreenVisuals.Singleton.ColorAdj();
                SoundManager.Singleton.PowerUpSound();
            }
            if (money)
            {
                GameManager.Singleton.MoneyPowerUp();
                SoundManager.Singleton.MoneySound();
            }
        }

        private void Appear()
        {
            one = transform.DOScale(currentScale, 1f);
        }
        public void Disappear(float speed)
        {

            if (isOnce && transform != null && !isUnwantedArt)
            {
                isOnce = false;
                two = transform.DOScale(Vector3.zero, speed).OnComplete(() => DestroyPopit());

            }
            else
            {
                if (one != null)
                    DOTween.Kill(one);
                if (two != null)
                    DOTween.Kill(two);

                DestroyPopit();
            }

        }
        public void DestroyPopit()
        {
            parent.transform.DOKill();
            if (one != null)
                DOTween.Kill(one);
            if (two != null)
                DOTween.Kill(two);
            Destroy(gameObject);
            Destroy(parent);
        }


        private void CheckIfMoneyReward()
        {
            var probability = new[] { 0, 0, 1 }; //вероятность получения денежного бонуса - 33 проц
            var value = probability[Random.Range(0, probability.Length)];
            //Debug.Log(value);
            if (value == 1)
            {
                GameManager.Singleton.UpdateMoney();
            }
        }
    }
}
