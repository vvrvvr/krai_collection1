using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using krai_cvetok;

namespace krai_cvetok
{
    public class ObjectState : MonoBehaviour
    {
        [SerializeField] private Material[] stateMaterials;
        private int currentState = 0;
        private MeshRenderer meshR;

        void Start()
        {
            meshR = GetComponent<MeshRenderer>();
        }

        public void NextState()
        {
            if (currentState < stateMaterials.Length - 1)
            {
                currentState++;
                meshR.material = stateMaterials[currentState];
            }
        }
    }
}
