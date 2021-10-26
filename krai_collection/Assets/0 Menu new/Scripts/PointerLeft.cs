using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using krai_menu;

namespace krai_menu
{

    public class PointerLeft : MonoBehaviour
    {
        [SerializeField] private GameObject pointerLeft;
        [SerializeField] private GameObject pointerRigth;
        [SerializeField] private Transform anchLeft;
        [SerializeField] private Transform ancRight;
        public float PointerScale = 2f;
        private Transform pointerLeftTransform;
        private Transform pointerRightTransform;
        [HideInInspector] public bool isCursorVisible;
        private Vector3 worldPosition = Vector3.zero;
        private Quaternion Rot;
        public float rotX = 0f;
        //private Vector3 cameraTopRight;
        //private Vector3 cameraBottomLeft;
        void Start()
        {
            //cameraTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
            //cameraBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));

            pointerLeftTransform = pointerLeft.GetComponent<Transform>();
            pointerRightTransform = pointerRigth.GetComponent<Transform>();
            //pointer.SetActive(false);

        }

        void Update()
        {

            if (isCursorVisible)
            {
                worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //позиция мыши в мире
                worldPosition.z = transform.position.z;
                if (isCursorVisible)
                    pointerLeftTransform.position = worldPosition;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PointerScale += 0.1f;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                PointerScale -= 0.1f;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                rotX += 1f;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                rotX -= 1f;
            }
            Rot = new Quaternion(0, 0, rotX, 0);
        }

        private void OnMouseOver()
        {
            isCursorVisible = true;
            //Debug.Log("here");
            Vector3 dirLeft = worldPosition - anchLeft.position;
            dirLeft = dirLeft / PointerScale; //масштаб
                                              //RotateVector(dirLeft, rotX);
            dirLeft = Quaternion.AngleAxis(rotX, Vector3.forward) * dirLeft; //угол
            pointerRightTransform.position = ancRight.position + dirLeft;

        }
        private void OnMouseEnter()
        {
            pointerLeft.SetActive(true);
            pointerRigth.SetActive(true);
            //isEnterArea = true;
            Cursor.visible = false;
            // Debug.Log($"enter {gameObject.name}");
        }
        private void OnMouseExit()
        {
            isCursorVisible = false;
            pointerLeft.SetActive(false);
            pointerRigth.SetActive(false);
            //isEnterArea = false;
            Cursor.visible = true;
            //  Debug.Log($"exit {gameObject.name}");
        }

        private Vector2 RotateVector(Vector2 v, float delta)
        {
            return new Vector2(
                v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
                v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
            );
        }
    }
}
