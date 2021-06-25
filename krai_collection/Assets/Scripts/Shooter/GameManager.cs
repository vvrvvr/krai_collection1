using UnityEngine;
using DG.Tweening;
using krai_shooter;

namespace krai_shooter
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Singleton;

        [SerializeField] Transform[] areas = new Transform[0];
        [SerializeField] Transform[] lookAtPoints = new Transform[0];
        [SerializeField] SpawnPoint[] spawnPoints = new SpawnPoint[0];
        [SerializeField] GameObject player;
        private Timer timer;
        private MoneyScript money;


        private int currentarea = -1;

        //timers 
        private float startTime = 30f;
        private float currentTime = 0;
        private float spawnRate = 3.5f;
        private float timeDecrease = 2;
        private float spawnDecrease = 0.5f;

        //start game
        public bool isStart;




        private void Start()
        {
            isStart = false;
            Singleton = this;
            money = GetComponent<MoneyScript>();
            timer = GetComponent<Timer>();
            player.GetComponent<PlayerController>().pointToLook = lookAtPoints[0];

            //MovePlayer();
            //testText.text = $"{testValue}";
        }
        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    MovePlayer();
            //}
            if (isStart)
                CountTime();

            //if (Input.GetKeyDown(KeyCode.J))
            //{
            //    // DOTween.To(UpdateText, testValue, 60000, 0.5f);
            //    //money.UpdateMoney();
            //    var str = "player <color = green>green</color> .GetComponent<Transform>().DOMove(areas[cu<b>absolutely <i>definitely</i>rrentarea].position, 1f).SetEase(Ease.OutBounce);player.GetComponent<PlayerController>().pointToLook = lookAtPoints[currentarea]; ";
            //    testText.DOText(str, 6f, true);

            //}
            //testText.text = $"{testValue}";
        }
        private void CountTime()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= startTime)
            {
                currentTime = 0;
                //timer.StopCounter();
                if (currentarea != -1)
                    spawnPoints[currentarea].StopSpawn();
                MovePlayer();
            }
        }

        public void MovePlayer()
        {
            currentarea++;
            if (currentarea >= areas.Length)
                currentarea = 0;
            player.GetComponent<Transform>().DOMove(areas[currentarea].position, 1f).SetEase(Ease.OutBounce);
            SoundManager.Singleton.MoveSound();
            player.GetComponent<PlayerController>().pointToLook = lookAtPoints[currentarea];
            //посчитать время
            CalculateTime();
            timer.SetRandomTime(startTime);
            spawnPoints[currentarea].StartSpawn(spawnRate);


        }
        private void StartGame()
        {

        }
        private void CalculateTime()
        {
            if (startTime >= 10)
                startTime -= timeDecrease;
            if (spawnRate >= 1)
                spawnRate -= spawnDecrease;
            else
            {
                if (spawnRate >= 0.8f)
                    spawnRate -= 0.1f;
            }
            //Debug.Log($"current time ={startTime}, spawn rate = {spawnRate}");
        }

        //всё по попытам
        public void MoneyPowerUp()
        {
            money.MoneyPowerup();
        }
        public void UpdateMoney()
        {
            money.UpdateMoney();
        }





    }
}
