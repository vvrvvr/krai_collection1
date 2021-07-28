using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using krai_cvetok;

namespace krai_cvetok
{
    public class Cloud : MonoBehaviour
    {
        private int _band;
        private float startScale = 10f;
        private float multiplayer = 1.5f;
        private float speed = 0.05f;
        private MeshRenderer meshR;
        private void Start()
        {
            //meshR = GetComponent<MeshRenderer>();
            _band = Random.Range(0, SoundManager.samples8.Length);
        }

        void Update()
        {
            var soundFloat = SoundManager.samples8[_band] * multiplayer + startScale;
            transform.localScale = new Vector3(soundFloat, transform.localScale.y, soundFloat);
            // meshR.material.color = new Color(soundFloat + _band, soundFloat, soundFloat + _band);

        }
        private void FixedUpdate()
        {
            transform.position += Vector3.right * speed;
        }
    }
}
