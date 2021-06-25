using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using krai_shooter;

namespace krai_shooter
{
    public class PopitButton : MonoBehaviour
    {
        public TextMesh text;
        [Header("popit script link")]
        [SerializeField] private PopitGeneral popit;
        [SerializeField] private Rigidbody[] _childRigidbody;
        [SerializeField] private Transform[] _childTransform;
        private Collider _collider;
        //[HideInInspector] bool isReady;

        private void Start()
        {
            //_childRigidbody = GetComponentsInChildren<Rigidbody>();
            // _childTransform = GetComponentsInChildren<Transform>();
            _collider = GetComponent<Collider>();
            //isReady = true;
        }

        //private void OnMouseDown()
        //{
        //    if (popit.canDisappear)
        //    {
        //        Destroy();
        //        SoundManager.Singleton.PlayButtonSound();
        //        popit.StartButtonPopit();
        //    }

        //}
        public void StartButton()
        {
            if (popit.canDisappear)
            {
                Destroy();
                SoundManager.Singleton.PlayButtonSound();
                popit.StartButtonPopit();
            }
        }

        public void Destroy()
        {
            //transform.SetParent(null);
            if (text != null) text.gameObject.SetActive(false);
            foreach (var rb in _childRigidbody) rb.isKinematic = false;
            _collider.enabled = false;
            foreach (var item in _childTransform)
            {
                item.parent = null;
                Destroy(item.gameObject, Random.Range(1f, 4f));
            }
            //isReady = false;
            // StartCoroutine(Reload());
        }

        //private IEnumerator Reload()
        //{
        //    yield return new WaitForSeconds(2f);
        //    if (text != null) text.gameObject.SetActive(true);
        //    foreach (var rb in _childRigidbody) rb.isKinematic = true;
        //    for(int i = 0; i < _childTransform.Length; i++)
        //    {
        //        //if (i == 0)
        //        //    continue;
        //        _childTransform[i].localPosition = Vector3.zero;
        //        _childTransform[i].localRotation = Quaternion.Euler(Vector3.zero);
        //    }
        //    _collider.enabled = true;
        //    //isReady = true;
        //}
    }
}

