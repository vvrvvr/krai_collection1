using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveRoadTrigger : MonoBehaviour
{
    private MusicBox musicBox;

	private void Start()
	{
        musicBox = GameObject.FindGameObjectWithTag("MusicBox").GetComponent<MusicBox>();
	}
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Road") 
            musicBox.PlayVoices();
        Debug.Log("Offroad");
    }

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Road") 
            musicBox.StopVoices();
    }

}
