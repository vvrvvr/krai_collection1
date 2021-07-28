using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using krai_shooter;

namespace krai_shooter
{
    public class Timer : MonoBehaviour
    {
        public TextMeshProUGUI timer;
        private bool isCountdown = true;
        private bool stopAtZero = true;
        private bool isStoped = true;
        float minutes = 0;
        float seconds = 0;
        float miliseconds = 0;

        void Update()
        {
            if (!isStoped)
                CountTime();

        }

        private void CountTime()
        {
            if (isCountdown)
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
                    else // to minus countdown
                    {
                        seconds = 0;
                        miliseconds = 0;
                        minutes = 0;
                        isCountdown = false;
                        if (stopAtZero) //if timer stops at zero choosen
                        {
                            isStoped = true;
                            timer.text = string.Format("0:00:00");

                            return;
                        }
                    }
                    if (minutes == 0 && seconds == 4)
                    {
                        ScaleEffect();
                    }
                }

                miliseconds -= Time.deltaTime * 100;
                timer.text = string.Format("{0}:{1}:{2}", minutes, seconds, (int)miliseconds);
            }
            else //minus countdown
            {
                if (miliseconds >= 100)
                {
                    if (seconds >= 59)
                    {
                        minutes++;
                        seconds = 0;
                    }
                    else if (seconds <= 59)
                    {
                        seconds++;
                    }

                    miliseconds = 0;
                }

                miliseconds += Time.deltaTime * 100;
                timer.text = string.Format("-{0}:{1}:{2}", minutes, seconds, (int)miliseconds);
            }
        }

        public void SetRandomTime(float sec)
        {
            stopAtZero = true;
            var probability = new[] { 0, 0, 1, 1, 0 }; //вероятность выпадения  - 40 проц
            var value = probability[Random.Range(0, probability.Length)];
            if (value == 1)
            {
                int secondsRandom = Random.Range(-5, 10);

                seconds = sec + secondsRandom;
                var probability2 = new[] { 0, 0, 1, 1, 0 }; //вероятность выпадения  - 40 проц
                var value2 = probability2[Random.Range(0, probability2.Length)];
                if (value2 == 1)
                {
                    stopAtZero = false;
                }
            }
            else
            {
                seconds = sec;
            }
            minutes = 0f;
            miliseconds = 0;
            isCountdown = true;
            isStoped = false;
        }
        public void StartCounter()
        {
            isStoped = false;
        }
        public void StopCounter()
        {
            isStoped = true;
        }
        private void ScaleEffect()
        {

            Sequence scale = DOTween.Sequence();
            scale.Append(timer.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f));
            scale.Append(timer.transform.DOScale(Vector3.one, 0.5f));
            scale.SetLoops(5);
        }
    }
}
