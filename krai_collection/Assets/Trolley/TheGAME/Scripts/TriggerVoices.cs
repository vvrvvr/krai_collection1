using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVoices : MonoBehaviour
{
	[FMODUnity.EventRef] [SerializeField] protected string _scream;
	[SerializeField] protected float _screamVolume = 0.5f;
	private MusicBox _musicBox;

	private void Start()
	{
		_musicBox = GameObject.FindGameObjectWithTag("MusicBox").GetComponent<MusicBox>();
	}

	private void OnTriggerEnter(Collider other)
	{
		_musicBox.PlayScream(_scream, _screamVolume);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		_musicBox.PlayScream(_scream, _screamVolume);
	}
}
