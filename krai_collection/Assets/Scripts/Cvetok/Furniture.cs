using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using krai_cvetok;

namespace krai_cvetok
{
    public class Furniture : MonoBehaviour
    {
        [SerializeField] private Material stateMaterial;
        private MeshRenderer meshR;

        void Start()
        {
            meshR = GetComponent<MeshRenderer>();
        }

        public void FullArtHouse()
        {
            meshR.material = stateMaterial;
        }
    }
}
