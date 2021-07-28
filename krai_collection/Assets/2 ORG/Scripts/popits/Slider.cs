using UnityEngine;
using DG.Tweening;
using krai_shooter;

namespace krai_shooter
{
    public class Slider : MonoBehaviour
    {
        public GameObject handle;
        private bool isOnce = true;
        private Transform parent;
        //line
        private LineRenderer line;
        [SerializeField] Transform lineAncor;
        private Transform handleTransform;
        [SerializeField] private PopitGeneral popit;

        private void Start()
        {
            handleTransform = handle.transform;
            parent = transform.parent;
            line = GetComponent<LineRenderer>();
            line.positionCount = 2;
            line.SetPosition(0, lineAncor.position);
            line.SetPosition(1, handleTransform.position);
        }

        private void Update()
        {
            line.SetPosition(0, lineAncor.position);
            line.SetPosition(1, handleTransform.position);
        }

        public void StartSlider(Vector3 sliderPosition)
        {

            //Debug.Log("slider");
            if (isOnce)
            {
                var localHitPosition = gameObject.transform.InverseTransformPoint(sliderPosition);
                SoundManager.Singleton.PlayButtonSound();
                isOnce = false;
                var sliderPos = new Vector3(localHitPosition.x, handle.transform.localPosition.y, handle.transform.localPosition.z);
                handle.transform.DOLocalMove(sliderPos, 0.2f).OnComplete(() => StartPopit()).SetId(parent);
            }
        }

        private void StartPopit()
        {
            popit.StartButtonPopit();
        }

    }
}
