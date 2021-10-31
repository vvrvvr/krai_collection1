using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using krai_menu;
using System;
using Random = UnityEngine.Random;

namespace krai_menu
{

    public class MasterOfPopits : MonoBehaviour
    {
        [SerializeField] private GameObject popitPrafab;
        [SerializeField] private Transform[] anchors;
        public List<GameObject> popitsList = new List<GameObject>();
        private GameObject currentPopit;
        //������� ����� �������
        private float timer = 0f;
        private float maxtime = 3f;

        private float popitSpeed = 0.7f;
        private float popitFadeSpeed = 1f;
        private int popitsTotalInt; //��� ���������� �������
        private int PopitsIndexInt; //��� ��� ���������� �������. �� ��������!
        private bool isFading;
        //����� ������� �������
        private float popitSpawnTimer = 0;
        private float popitSpawnCurrentMaxTime = 2f;
        public bool IsPause;

        [SerializeField] private string[] phrases; //����� ��������
        private int indexOfPhrases = -1;
        [SerializeField] private Sprite[] avatars; //����� �������� � �������
        private void Start()
        {
            RandomizeArray(phrases);

        }

        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    CreatePopit();
            //}
            if (!IsPause)
            {
                SpawnPopits();
                PopitController();
            }
        }
        private void CreatePopit()
        {
            var newPopit = Instantiate(popitPrafab, anchors[0].position, Quaternion.identity);
            indexOfPhrases++;
            if (indexOfPhrases >= phrases.Length)
            {
                RandomizeArray(phrases);
                //foreach (var item in phrases)
                //{
                //    Debug.Log(item);
                //}
                indexOfPhrases = 0;
            }
            var message = phrases[indexOfPhrases];
            var avatarIndex = Random.Range(0, avatars.Length);
            popitPrafab.GetComponent<Popit>().SetpopitGuts(message, avatars[avatarIndex]);
            popitsList.Add(newPopit);
            timer = 0f;
            isFading = false;
            CancelFading();
            RepositionPopits();
        }
        private void RepositionPopits()
        {
            int popitsTotal = popitsList.Count;
            int popitPositionIndex = popitsTotal; //������ �������� ������ ����� ����� ������� ������
            popitsTotalInt = popitsTotal;
            PopitsIndexInt = popitPositionIndex;

            if (popitsTotal >= 4)
            {
                GameObject popit = popitsList[0];
                currentPopit = popit;
                var pointSprite = popit.GetComponent<SpriteRenderer>();
                pointSprite.DOFade(0f, popitFadeSpeed / 2).OnComplete(PartReposition); // ������ ��������� ������� ��-�� ����, ��� ��� ������� �����������
            }
            else
            {
                for (int i = 0; i < popitsTotal; i++)
                {
                    GameObject popit = popitsList[i];
                    if (i + 1 == popitsTotal)
                    {
                        var pointSprite = popit.GetComponent<SpriteRenderer>();
                        pointSprite.DOFade(0f, 0f);
                        pointSprite.DOFade(1f, popitFadeSpeed);
                    }
                    popit.transform.DOMove(anchors[popitPositionIndex].position, popitSpeed).SetId(popit.GetInstanceID());
                    popitPositionIndex--;
                }
            }
        }
        private void PartReposition()
        {
            Destroy(currentPopit);
            popitsList.RemoveAt(0);
            popitsTotalInt--;
            PopitsIndexInt--;
            for (int i = 0; i < popitsTotalInt; i++)
            {
                GameObject popit = popitsList[i];
                if (i + 1 == popitsTotalInt)
                {
                    var pointSprite = popit.GetComponent<SpriteRenderer>();
                    pointSprite.DOFade(0f, 0f);
                    pointSprite.DOFade(1f, popitFadeSpeed).SetId(popit.GetInstanceID());
                }
                popit.transform.DOMove(anchors[PopitsIndexInt].position, popitSpeed);
                PopitsIndexInt--;
            }
        }
        public void DeleteOnClick() //�������� �� ����� ����� �� ������� ������� ����
        {
            var totalPopits = popitsList.Count;
            for (int i = 0; i < totalPopits; i++)
            {
                GameObject popit = popitsList[i];
                var pointSprite = popit.GetComponent<SpriteRenderer>();
                if (i + 1 != totalPopits)
                    pointSprite.DOFade(0f, popitFadeSpeed / 4);
                else
                    pointSprite.DOFade(0f, popitFadeSpeed / 4).OnComplete(DestroyPopitsInList);
            }

        }
        private void DestroyPopitsInList()
        {
            foreach (var item in popitsList)
            {
                DOTween.Kill(item.GetInstanceID());
                Destroy(item);
            }
            popitsList.Clear();
            isFading = false;
        }
        private void PopitController() //�������� ����� ������� � �������� �� ��������
        {
            timer += Time.deltaTime;
            if (timer >= maxtime && !isFading)
            {
                isFading = true;

                for (int i = 0; i < popitsList.Count; i++)
                {
                    var pointSprite = popitsList[i].GetComponent<SpriteRenderer>();
                    if (i + 1 != popitsList.Count)
                        pointSprite.DOFade(0f, popitFadeSpeed).SetId(popitsList[i].GetInstanceID());
                    else
                        pointSprite.DOFade(0f, popitFadeSpeed).SetId(popitsList[i].GetInstanceID()).OnComplete(DestroyPopitsInList); //������� �� ������� ����� ���������� ���� �������� �� ����
                }
            }
        }
        private void CancelFading() //�������� ���� ���� ����� ����� �������� 
        {
            foreach (var item in popitsList)
            {
                DOTween.Kill(item.GetInstanceID());
                var pointSprite = item.GetComponent<SpriteRenderer>();
                pointSprite.DOFade(1f, popitFadeSpeed / 4);
            }
        }

        private void SpawnPopits()
        {
            popitSpawnTimer += Time.deltaTime;
            if (popitSpawnTimer >= popitSpawnCurrentMaxTime)
            {
                CreatePopit();
                popitSpawnTimer = 0f;
                var valuesToChoose = new[] { 1.4f, 1f, 1.7f, 4, 7, 5, 8, 5, 6 };
                var choosenValue = valuesToChoose[Random.Range(0, valuesToChoose.Length)];
                //Debug.Log(choosenValue);
                popitSpawnCurrentMaxTime = choosenValue;

            }
        }

        static void RandomizeArray(string[] arr)
        {
            for (var i = arr.Length - 1; i > 0; i--)
            {
                var r = Random.Range(0, i);
                var tmp = arr[i];
                arr[i] = arr[r];
                arr[r] = tmp;
            }
        }
    }

}

