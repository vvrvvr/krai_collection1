using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using krai_shooter;

namespace krai_shooter
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject[] popitPrefabs = new GameObject[0];
        [SerializeField] private GameObject[] powerupPrefabs = new GameObject[0];
        public List<PopitGeneral> currentPopitsOnPoint = new List<PopitGeneral>();
        [SerializeField] private Transform directionToLook;
        public bool isStart = false;
        private float spawnRate = 1f;
        private float currentTime;
        [Header("constrains")]
        [SerializeField] private float xLocal;
        [SerializeField] private float zLocal;
        [SerializeField] private float yMaxLocal;
        [SerializeField] private float yMinLocal;
        private GameObject popit;
        private bool isFirst = true;

        private int gunPowerupCounter = 0;




        void Update()
        {
            if (isStart)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= spawnRate || isFirst)
                {
                    isFirst = false;
                    currentTime = 0;
                    var probability = new[] { 0, 0, 0, 1 }; //вероятность выпадения префаба - 25 проц
                    var value = probability[Random.Range(0, probability.Length)];

                    if (value == 1)
                    {
                        var val = Random.Range(0, powerupPrefabs.Length - 1);
                        gunPowerupCounter++;
                        if (gunPowerupCounter >= 5)
                        {
                            gunPowerupCounter = 0;
                            val = powerupPrefabs.Length - 1;
                        }

                        popit = Instantiate(powerupPrefabs[val]);

                    }
                    else
                    {
                        var val = Random.Range(0, popitPrefabs.Length);
                        popit = Instantiate(popitPrefabs[val]);
                    }


                    currentPopitsOnPoint.Add(popit.GetComponentInChildren<PopitGeneral>());
                    popit.transform.parent = transform;
                    var x = Random.Range(-xLocal, xLocal);
                    var y = Random.Range(yMinLocal, yMaxLocal);
                    var z = Random.Range(-zLocal, zLocal);
                    //popit.transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z);
                    popit.transform.localPosition = new Vector3(x, y, z);
                    var direction = (popit.transform.position - directionToLook.position).normalized;
                    var lookRotation = Quaternion.LookRotation(direction);
                    popit.transform.rotation = lookRotation;


                    //popit.transform.LookAt()
                    //var lookPos = new Vector3(0f, directionToLook.position.y +90f, popit.transform.position.z);
                    //popit.transform.rotation(lookPos);
                    //Debug.Log("time");
                }
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                StopSpawn();
            }
        }

        public void StartSpawn(float spawnR)
        {
            spawnRate = spawnR;
            isStart = true;
        }
        public void StopSpawn()
        {

            isStart = false;
            foreach (var item in currentPopitsOnPoint)
            {
                if (item != null)
                {
                    item.Disappear(0.2f);
                }

            }
            currentPopitsOnPoint.Clear();
        }
    }
}
