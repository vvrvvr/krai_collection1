using UnityEngine;
using krai_menu;

namespace krai_menu
{

    public class CustomCursorRight : MonoBehaviour
    {
        private Transform tr;
        public LayerMask iconLayer;
        [SerializeField] private MasterOfPopits masterPopit;

        void Start()
        {
            tr = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit rayHit;
                if (Physics.Raycast(tr.position, new Vector3(0, 0, 1), out rayHit, Mathf.Infinity, iconLayer))
                {
                    masterPopit.DeleteOnClick();
                }

            }
        }
    }
}
