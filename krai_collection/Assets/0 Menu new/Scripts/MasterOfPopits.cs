using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using krai_menu;

namespace krai_menu
{

    public class MasterOfPopits : MonoBehaviour
    {
        [SerializeField] private GameObject popitPrafab;
        [SerializeField] private Transform[] anchors;
        public List<GameObject> popitsList = new List<GameObject>();
        private GameObject currentPopit;
        //таймеры фейда попытов
        private float timer = 0f;
        private float maxtime = 3f;

        private float popitSpeed = 0.7f;
        private float popitFadeSpeed = 1f;
        private int popitsTotalInt; //для дерьмового решения
        private int PopitsIndexInt; //тож для дерьмового решения. но работает!
        private bool isFading;
        //спавн попытов таймеры
        private float popitSpawnTimer = 0;
        private float popitSpawnCurrentMaxTime = 2f;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CreatePopit();
            }
            SpawnPopits();
            PopitController();
        }
        private void CreatePopit()
        {
            var newPopit = Instantiate(popitPrafab, anchors[0].position, Quaternion.identity);
            popitsList.Add(newPopit);
            timer = 0f;
            isFading = false;
            CancelFading();
            RepositionPopits();
        }
        private void RepositionPopits()
        {
            int popitsTotal = popitsList.Count;
            int popitPositionIndex = popitsTotal; //индекс нулевого попыта будет равен индексу анкора
            popitsTotalInt = popitsTotal;
            PopitsIndexInt = popitPositionIndex;

            if (popitsTotal >= 4)
            {
                GameObject popit = popitsList[0];
                currentPopit = popit;
                var pointSprite = popit.GetComponent<SpriteRenderer>();
                pointSprite.DOFade(0f, popitFadeSpeed / 2).OnComplete(PartReposition); // крайне дерьмовое решение из-за того, что нет времени разбираться
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
        public void DeleteOnClick() //вызываем по клику мышки из скрипта поинтер райт
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
        private void PopitController() //контроль фейда попытов и удаления со временем
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
                        pointSprite.DOFade(0f, popitFadeSpeed).SetId(popitsList[i].GetInstanceID()).OnComplete(DestroyPopitsInList); //удаляем по времени фейда последнего чтоб корутины не того
                }
            }
        }
        private void CancelFading() //сбросить фейд если новый попыт появился 
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
    }
}
