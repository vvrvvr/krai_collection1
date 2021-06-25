using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class NoInPutTimer : MonoBehaviour
{
    [SerializeField] GameObject timerScreen;
    private Text timerText;
    private float currentTime = 0f;
    private float maxTime = 40f;
    Vector3 lastMousePosition;

    //visuals
    float seconds = 10f;
    float miliseconds = 0f;
    float minutes = 0f;

    private void Start()
    {
        timerScreen.SetActive(false);
        timerText = timerScreen.GetComponent<Text>();
    }

    void Update()
    {

        if (!Input.anyKey && Input.mousePosition == lastMousePosition) //not pressed
        {
            currentTime += Time.unscaledDeltaTime;
            if (currentTime >= 5f)
            {
                timerScreen.SetActive(true);
                CountTime();
            }
        }
        else //pressed
        {
            timerScreen.SetActive(false);
            seconds = 10f;
            miliseconds = 0f;
            lastMousePosition = Input.mousePosition;
            currentTime = 0f;
        }
    }


    private void CountTime()
    {
        if (miliseconds <= 0)
        {
            if (seconds <= 0)
            {
                minutes--;

                seconds = 59;
            }
            else if (seconds >= 0)
            {
                seconds--;
            }
            if (minutes >= 0)
            {
                miliseconds = 100;
            }
            else // reach zero
            {
                seconds = 0;
                miliseconds = 0;
                minutes = 0;


                timerText.text = string.Format("выход в меню через: {0}:{1}:{2}", minutes, seconds, (int)miliseconds);

                return;
            }
        }
        if (minutes == 0 && seconds == 5)
        {
            ScaleEffect();
        }
        miliseconds -= Time.unscaledDeltaTime * 100;
        timerText.text = string.Format("выход в меню через: {0}:{1}:{2}", minutes, seconds, (int)miliseconds);
    }
    private void ScaleEffect()
    {

        Sequence scale = DOTween.Sequence();
        scale.Append(timerText.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f));
        scale.Append(timerText.transform.DOScale(Vector3.one, 0.5f));
        scale.SetLoops(5);
    }
}




