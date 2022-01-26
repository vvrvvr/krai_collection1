using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip videoRus;
    [SerializeField] private VideoClip videoEng;
    [SerializeField] private float videoPlayTime;

    [SerializeField] private GameObject menuButton;
    private Coroutine playingVideoCoroutine;
    private bool isVideoPlaying;
    private void Awake()
    {
        if (!LanguageSettings.Singleton.isRussian)
        {
            videoPlayer.clip = videoEng;
        }
    }
    private void Start()
    {
        if (videoBool.Singleton.isVideoPlaying)
        {
            videoPlayTime = (float)videoPlayer.length;
            menuButton.SetActive(false);
            videoPlayer.gameObject.SetActive(true);
            playingVideoCoroutine = StartCoroutine(VideoPlay());
        }

        //if (PlayerPrefs.HasKey("video"))
        //{
        //    if (PlayerPrefs.GetInt("video") == 0)
        //    {
        //        menuButton.SetActive(false);
        //        video.gameObject.SetActive(true);
        //        StartCoroutine(VideoPlay());
        //        PlayerPrefs.SetInt("video", 1);
        //    }
        //    else
        //    {
        //        menuButton.SetActive(true);
        //        video.gameObject.SetActive(false);
        //    }
        //}
        //else
        //{
        //    menuButton.SetActive(false);
        //    video.gameObject.SetActive(true);
        //    StartCoroutine(VideoPlay());
        //    PlayerPrefs.SetInt("video", 1);
        //}

    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && video.gameObject.activeInHierarchy)
        //{
        //    video.gameObject.SetActive(true);
        //    menuButton.SetActive(false);
        //    StartCoroutine(VideoPlay());
        //    PlayerPrefs.SetInt("video", 1);
        //    Debug.Log("pressed");
        //}
        if (Input.anyKeyDown && videoBool.Singleton.isVideoPlaying)
        {
            if (playingVideoCoroutine != null)
                StopCoroutine(playingVideoCoroutine);
            videoBool.Singleton.isVideoPlaying = false;
            menuButton.SetActive(true);
            videoPlayer.gameObject.SetActive(false);
            Debug.Log("pressed");
        }
    }

    private IEnumerator VideoPlay()
    {
        yield return new WaitForSeconds(videoPlayTime);

        videoBool.Singleton.isVideoPlaying = false;
        menuButton.SetActive(true);
        videoPlayer.gameObject.SetActive(false);
    }

}
