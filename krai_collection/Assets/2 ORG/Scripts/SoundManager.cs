using DG.Tweening;
using UnityEngine;
using krai_shooter;

namespace krai_shooter
{
    public class SoundManager : MonoBehaviour
    {
        [Header("sounds")]
        [SerializeField] private AudioClip shootSound;
        [SerializeField] private AudioClip reloadSound;
        [SerializeField] private AudioClip explosionSound;
        [SerializeField] private AudioClip buttonShatterSound;
        [SerializeField] private AudioClip powerupSound;
        [SerializeField] private AudioClip moneySound;
        [SerializeField] private AudioClip moveSound;
        [SerializeField] private AudioClip switchSound;
        [SerializeField] private AudioClip gunPowerup;

        [Header("music")]
        [SerializeField] private AudioClip mainTheme;

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _shootSource;
        [SerializeField] private AudioSource _moveSource;

        //pause
        private Tween one;
        private Tween two;


        public static SoundManager Singleton;

        private void Awake()
        {
            Singleton = this;
        }

        void Start()
        {
            //_audioSource = gameObject.AddComponent<AudioSource>();
            //_musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.clip = mainTheme;
            _musicSource.Stop();
            _musicSource.loop = true;

        }

        //music
        public void StartMusic()
        {
            _musicSource.Play();
        }

        //effects
        public void PlayReloadSound()
        {
            _audioSource.PlayOneShot(reloadSound);
        }


        public void PlayButtonSound()
        {
            _audioSource.pitch = Random.Range(0.7f, 1.3f);
            _audioSource.PlayOneShot(buttonShatterSound);
            //_audioSource.pitch = 1f;
        }

        public void PowerUpSound()
        {
            _audioSource.PlayOneShot(powerupSound);
        }
        public void MoneySound()
        {
            _audioSource.PlayOneShot(moneySound);
        }
        public void MoveSound()
        {

            _moveSource.PlayOneShot(moveSound);
        }
        public void SwitchSound()
        {
            _audioSource.PlayOneShot(switchSound);
        }
        public void GunPowerupSound()
        {
            _audioSource.PlayOneShot(gunPowerup);
        }

        //shooting
        public void PlayExplosionSound()
        {
            _shootSource.pitch = Random.Range(0.7f, 1.3f);
            _shootSource.PlayOneShot(explosionSound);
        }
        public void PlayShootSound()
        {
            _shootSource.PlayOneShot(shootSound);
        }

        public void PauseGame()
        {
            _musicSource.pitch = 0.55f;
            _musicSource.volume = 0.6f;

            // DOTween.To(() => _moveSource.pitch, x => bloom.intensity.value = x, 2, 0.3f);

        }
        public void ResumeGame()
        {
            _musicSource.pitch = 1f;
            _musicSource.volume = 1f;

        }
    }
}
