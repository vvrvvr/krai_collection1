using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using krai_shooter;

namespace krai_shooter
{
    public class ArtSource : MonoBehaviour
    {
        public static ArtSource Singleton;
        [SerializeField] private Sprite[] images = new Sprite[0];
        [SerializeField] private VideoClip[] videoclips = new VideoClip[0];
        [SerializeField] private string[] texts = new string[0];
        [SerializeField] private string[] genres = new string[0];
        [SerializeField] private string[] notifications = new string[0];
        [SerializeField] private string[] textsEnglish = new string[0];
        [SerializeField] private string[] genresEnglish = new string[0];
        [SerializeField] private string[] notificationsEnglish = new string[0];
        private bool isRussian = true;
        private string from = "\n\nÓÚ ¿√≈Õ“¿ 10002";
        private string fromEnglish = "\n\nfrom AGENT 10002";

        private void Awake()
        {
            Singleton = this;
        }
        private void Start()
        {
            if(LanguageSettings.Singleton != null)
                isRussian = LanguageSettings.Singleton.isRussian;
        }
        public Sprite GetRandomSprite()
        {
            var numb = Random.Range(0, images.Length);
            return images[numb];

        }
        public VideoClip GetRandomVideo()
        {
            var numb = Random.Range(0, videoclips.Length);
            return videoclips[numb];
        }
        public string GetRandomText()
        {
            if(isRussian)
            {
                var numb = Random.Range(0, texts.Length);
                return texts[numb];
            }
            else
            {
                var numb = Random.Range(0, textsEnglish.Length);
                return textsEnglish[numb];
            }
        }
        public string GetRandomGenre()
        {
            if (isRussian)
            {
                var numb = Random.Range(0, genres.Length);
                return genres[numb];
            }
            else
            {
                var numb = Random.Range(0, genresEnglish.Length);
                return genresEnglish[numb];
            }
        }
        public string GetRandomNotification()
        {
            if (isRussian)
            {
                var numb = Random.Range(0, notifications.Length);
                return notifications[numb] + from;
            }
            else
            {
                var numb = Random.Range(0, notificationsEnglish.Length);
                return notificationsEnglish[numb] + fromEnglish;
            }
        }

    }
}
