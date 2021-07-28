using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using krai_cvetok;

namespace krai_cvetok
{
    public class DialogueWorldTrigger : MonoBehaviour
    {
        [SerializeField] private Transform objectToLook;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private bool isForcedLook;
        [SerializeField] private bool isTakeControl;
        [SerializeField] private bool isOnce;
        [SerializeField] private bool isChangeMusic = false;

        public string[] s = new string[0];
        public string[] f = new string[0];
        //private bool isTriggerActivated;
        private string[] phrase;



        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player"))
            {
                if (Endings.Singleton.isRussian)
                    phrase = s;
                else
                    phrase = f;
                gameManager.InitiateDialogue(phrase, objectToLook.position, isForcedLook, isTakeControl);
                if (isChangeMusic)
                {
                    gameManager.isSwitchTrack = true;
                    //Debug.Log("changed music");
                }
                if (isOnce)
                    Destroy(gameObject);

            }
        }


    }
}
