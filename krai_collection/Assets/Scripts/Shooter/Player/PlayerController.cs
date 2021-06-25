using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using krai_shooter;

namespace krai_shooter
{


    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Singleton;

        [SerializeField] private Transform playerBody;
        [SerializeField] private float mouseSensitivity = 50f;
        public Transform pointToLook;
        [SerializeField] private float SmoothLookSpeed;
        private float xRotation = 0f;
        private float yRotation = 0f;
        public bool controllerPauseState = false;
        private bool lockAndHideCursor = true;
        //private bool isSmoothLook = true;

        //recoil
        [SerializeField] private float recoilX = 1f;
        [SerializeField] private float recoilY = 0f;
        [SerializeField] private float recoilDuration = 0.05f;
        private float recoilRifleDuration = 0.015f;

        [Header("constrains")]
        [SerializeField] private float xAxisConst;
        [SerializeField] private float yAxisConst;
        private float recoilTime;

        public static System.Action OnPauseEvent;

        private void Awake()
        {
            Singleton = this;
        }
        void Start()
        {
            // Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        void Update()
        {
            if (!controllerPauseState)
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -xAxisConst, xAxisConst);

                yRotation -= mouseX;
                yRotation = Mathf.Clamp(yRotation, -yAxisConst, yAxisConst);

                //recoil
                if (Time.time <= recoilTime)
                {
                    xRotation -= recoilX;
                    yRotation -= recoilY;
                }


                Camera.main.transform.localRotation = Quaternion.Euler(xRotation, -yRotation, 0f);

            }
            SmoothLook(pointToLook.position);
            if (Input.GetButtonDown("Cancel")) //game pause
            {
                OnPauseEvent?.Invoke();
                ControllerPause();
                //event in menu manager
            }
        }

        void SmoothLook(Vector3 newDirection)
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((newDirection - transform.position).normalized), Time.deltaTime * SmoothLookSpeed);

        }
        public void ControllerPause()
        {
            controllerPauseState = !controllerPauseState;
            if (lockAndHideCursor)
            {
                Cursor.lockState = controllerPauseState ? CursorLockMode.None : CursorLockMode.Locked;
                Cursor.visible = controllerPauseState;
            }
        }
        public void Recoil()
        {
            recoilTime = Time.time + recoilDuration;
            recoilY = Random.Range(-0.5f, 0.5f);
        }
        public void RecoilRifle()
        {
            recoilTime = Time.time + recoilRifleDuration;
            recoilY = Random.Range(-0.3f, 0.3f);
        }

    }
}
