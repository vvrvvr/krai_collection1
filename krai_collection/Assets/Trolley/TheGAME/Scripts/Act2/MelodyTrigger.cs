using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyTrigger : MonoBehaviour
{
    [SerializeField] private MusicBox _musicBox;
    [FMODUnity.EventRef] [SerializeField] protected string _melody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bus")
        {
            _musicBox.PlayScream(_melody, 1);
            Destroy(gameObject);
        }
    }
}
