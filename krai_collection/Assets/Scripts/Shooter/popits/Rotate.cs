using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using krai_shooter;

namespace krai_shooter
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float speed = 100f;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * speed, 0);
        }
    }
}
