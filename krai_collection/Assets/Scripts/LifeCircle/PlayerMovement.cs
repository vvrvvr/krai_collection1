using UnityEngine;

namespace krai_room
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private Transform playerCamera;

        [SerializeField]
        private AudioSource steps;

        private CharacterController controller;

        private Transform playerTransform;



        void Start()
        {
            controller = GetComponent<CharacterController>();
            playerTransform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            float forwardMovement = Input.GetAxisRaw("Vertical");
            float rightMovement = Input.GetAxisRaw("Horizontal");
            if (forwardMovement != 0 || rightMovement != 0)
            {
                Vector3 newForwardVector = new Vector3(playerCamera.forward.x, 0, playerCamera.forward.z) * forwardMovement;
                Vector3 newRightVector = new Vector3(playerCamera.right.x, 0, playerCamera.right.z) * rightMovement;
                controller.Move((newForwardVector + newRightVector).normalized * speed * Time.deltaTime);
                if (!steps.isPlaying)
                    steps.Play();
            }
            else
            {
                steps.Stop();
            }
        }
    }
}
