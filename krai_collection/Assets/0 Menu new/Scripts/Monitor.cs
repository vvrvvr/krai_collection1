using UnityEngine;
using krai_menu;

namespace krai_menu
{

    public class Monitor : MonoBehaviour
    {
        [SerializeField] private GameObject pointerMain;
        [SerializeField] private GameObject pointerSecondary;
        [SerializeField] private Transform anchMain;
        [SerializeField] private Transform ancSecondary;
        public float PointerScale = 5.9f;
        private Transform pointerMainTransform;
        private Transform pointerSecondaryTransform;
        [HideInInspector] public bool isCursorVisible;
        private Vector3 worldPosition = Vector3.zero;
        public float rotX = 14f;

        void Start()
        {
            pointerMainTransform = pointerMain.GetComponent<Transform>();
            pointerSecondaryTransform = pointerSecondary.GetComponent<Transform>();
        }

        void Update()
        {
            if (isCursorVisible)
            {
                worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //позиция мыши в мире
                worldPosition.z = transform.position.z;
                if (isCursorVisible)
                    pointerMainTransform.position = worldPosition;
            }
        }
        private void OnMouseOver()
        {
            isCursorVisible = true;
            Vector3 dirLeft = worldPosition - anchMain.position;
            dirLeft = dirLeft * PointerScale; //масштаб
            dirLeft = Quaternion.AngleAxis(rotX, Vector3.forward) * dirLeft; //угол
            pointerSecondaryTransform.position = ancSecondary.position + dirLeft;

        }
        private void OnMouseEnter()
        {
            pointerMain.SetActive(true);
            pointerSecondary.SetActive(true);
            Cursor.visible = false;
        }
        private void OnMouseExit()
        {
            isCursorVisible = false;
            pointerMain.SetActive(false);
            pointerSecondary.SetActive(false);
            Cursor.visible = true;
        }
    }
}
