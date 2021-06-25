using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using krai_cvetok;

namespace krai_cvetok
{
    public class PlayerInteraction : MonoBehaviour
    {
        private GameObject raycastedObj;
        [SerializeField] private int rayLength = 10;
        [SerializeField] private LayerMask layerMaskInteract;
        [SerializeField] private Sprite crosshair;
        [SerializeField] private Sprite hand;
        [SerializeField] private Sprite enterDoor;
        [SerializeField] private Transform camTransform;
        private FirstPersonAIO player;
        //[SerializeField] private Sprite eye;
        [SerializeField] private GameManager gameManager;
        private SoundManager sounds;

        private void Start()
        {
            player = GetComponent<FirstPersonAIO>();
            sounds = SoundManager.Singleton;

        }


        void Update()
        {
            RaycastHit hit;
            Vector3 fwd = camTransform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(camTransform.position, fwd, out hit, rayLength, layerMaskInteract.value))
            {
                //Debug.Log("interactable");
                if (hit.collider.CompareTag("flower"))
                {
                    raycastedObj = hit.collider.gameObject;
                    player.crossHairImage.sprite = hand;
                    //Debug.Log("flower hit");
                    if (Input.GetMouseButtonDown(0))
                    {
                        Destroy(raycastedObj);
                        raycastedObj = null;
                        //sound
                        sounds.PlayFlowerPickup();
                        gameManager.UpdateFlowers();
                    }
                }
                if (hit.collider.CompareTag("note"))
                {
                    raycastedObj = hit.collider.gameObject;
                    player.crossHairImage.sprite = hand;
                    //Debug.Log("note hit");
                    if (Input.GetMouseButtonDown(0))
                    {
                        gameManager.ChangeWorldState();
                        Destroy(raycastedObj);
                        raycastedObj = null;
                        gameManager.ShowNote();
                        //sound
                        sounds.PlayNotePickup();
                        sounds.SwitchTrack();
                    }
                }
                if (hit.collider.CompareTag("doorClosed"))
                {
                    raycastedObj = hit.collider.gameObject;
                    player.crossHairImage.sprite = enterDoor;
                    if (Input.GetMouseButtonDown(0))
                    {
                        raycastedObj = null;
                        gameManager.EnterDoor();
                    }
                }
            }
            else
            {
                player.crossHairImage.sprite = crosshair;
            }


        }
    }
}
