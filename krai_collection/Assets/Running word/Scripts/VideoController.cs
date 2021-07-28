using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private VideoPlayer video;
    [SerializeField] private float videoPlayTime;

    [SerializeField] private GameObject menuButton;

    private void Start()
    {
        videoPlayTime = (float)video.length;

        if (PlayerPrefs.HasKey("video"))
        {
            if (PlayerPrefs.GetInt("video") == 0)
            {
                menuButton.SetActive(false);
                video.gameObject.SetActive(true);
                StartCoroutine(VideoPlay());
                PlayerPrefs.SetInt("video", 1);
            }
            else
            {
                menuButton.SetActive(true);
                video.gameObject.SetActive(false);
            }
        }
        else
        {
            menuButton.SetActive(false);
            video.gameObject.SetActive(true);
            StartCoroutine(VideoPlay());
            PlayerPrefs.SetInt("video", 1);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && video.gameObject.activeInHierarchy)
        {
            video.gameObject.SetActive(true);
            StartCoroutine(VideoPlay());
            PlayerPrefs.SetInt("video", 1);
        }
    }

    private IEnumerator VideoPlay()
    {
        yield return new WaitForSeconds(videoPlayTime);

        menuButton.SetActive(true);
        video.gameObject.SetActive(false);
    }

}
