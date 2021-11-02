using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using krai_menu;
using System.Collections;

namespace krai_menu
{

    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject titles;
        [SerializeField] private RectTransform titlesRect;
        private bool isTitlesShown;
        private bool canHideTitles;
        [SerializeField] PolygonCollider2D col1;
        [SerializeField] PolygonCollider2D col2;
        [SerializeField] PolygonCollider2D col3;
        [SerializeField] private IdeWindow ideWindow;
        [SerializeField] private MasterOfPopits masterOfPopits;

        void Start()
        {
            titles.SetActive(false);
        }

        void Update()
        {
            
        }
        private void LateUpdate()
        {
            if (Input.anyKey && canHideTitles)
            {
                StartCoroutine(WaitTime());
                
            }
        }
        private  IEnumerator WaitTime()
        {
            yield return new WaitForSeconds(0.15f);
            if (isTitlesShown)
                HideTitles();
        }
        public void ShowTitles()
        {
            StopAll();
            titles.SetActive(true);
            isTitlesShown = true;
            titlesRect.DOAnchorPos(Vector2.zero, 0.3f).OnComplete(AfterShowTitles);
        }

        private void HideTitles()
        {
            canHideTitles = false;
            titlesRect.DOAnchorPos(new Vector2(2000, 1000f), 0.3f).OnComplete(AfterTitles);
        }
        private void AfterTitles()
        {
            //titlesRect.position = new Vector2(2000f, 1000f);
            titles.SetActive(false);
            isTitlesShown = false;
            col1.enabled = true;
            col2.enabled = true;
            col3.enabled = true;
            ideWindow.PlayCoding();
            masterOfPopits.IsPause = false;

        }
        private void StopAll()
        {
            col1.enabled = false;
            col2.enabled = false;
            col3.enabled = false;
            ideWindow.PauseCoding();
            masterOfPopits.IsPause = true;

        }
        private void AfterShowTitles()
        {
            canHideTitles = true;
        }
    }
}
