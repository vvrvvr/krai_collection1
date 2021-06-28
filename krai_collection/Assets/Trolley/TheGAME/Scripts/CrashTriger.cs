using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashTriger : MonoBehaviour
{
    private MusicBox _musicBox;
    private bool _notTree;

    private void Start()
    {
        _musicBox = GameObject.FindGameObjectWithTag("MusicBox").GetComponent<MusicBox>();
        _notTree = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tree" && _notTree)
            _notTree = false;
        else if (other.gameObject.tag == "Tree" 
            || other.gameObject.tag == "Bilding" 
            || other.gameObject.tag == "Busstop")
            _musicBox.PlayScream();
    }
}
