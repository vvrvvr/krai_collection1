using UnityEngine;

namespace krai_room
{
    [RequireComponent(typeof(Camera))]
    public class Camera : MonoBehaviour
    {
        [SerializeField]
        private float sensivity;

        private Transform camera;
        float mouseXDir;
        float mouseYDir;

        void Start()
        {
            camera = transform;
            sensivity = SettingsMenu.GetSensivitySettings();
        }

        public void UpdateCameraSettings()
        {
            sensivity = SettingsMenu.GetSensivitySettings();
        }

        // Update is called once per frame
        void Update()
        {
            if (CameraSettings.isPause)
                return;
            mouseXDir += Input.GetAxis("Mouse X") * sensivity;
            mouseYDir += Input.GetAxis("Mouse Y") * sensivity;
            mouseYDir = Mathf.Clamp(mouseYDir, -90, 90);

            Vector3 cameraRot = new Vector3(-mouseYDir, mouseXDir, 0);

            //gameObject.transform.localEulerAngles = new Vector3(0, cameraRot.y, 0);
            camera.eulerAngles = cameraRot;
        }
    }
}
