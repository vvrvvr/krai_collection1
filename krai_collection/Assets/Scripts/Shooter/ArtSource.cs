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

        private void Awake()
        {
            Singleton = this;
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
            var numb = Random.Range(0, texts.Length);
            return texts[numb];
        }
        public string GetRandomGenre()
        {
            var numb = Random.Range(0, genres.Length);
            return genres[numb];
        }
        public string GetRandomNotification()
        {
            var numb = Random.Range(0, notifications.Length);
            return notifications[numb];
        }

    }
}
