using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace krai_trol
{
	public class TriggerAddGrandma : MonoBehaviour
	{
		[SerializeField] private GrandmasSpawner spawner;
		[SerializeField] private float climbingVoicesNumber = 1f;
		[SerializeField] private bool changeVoices = false;
		private MusicBox _musicBox;
		private bool isOnce;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == "Bus" && !isOnce)
			{
				if (changeVoices)
				{
					_musicBox = GameObject.FindGameObjectWithTag("MusicBox").GetComponent<MusicBox>();
					_musicBox.ClimbingSetVoicesParameter(climbingVoicesNumber);
					isOnce = true;
				}
				spawner.Spawn();
				
			}
		}
	}
}
