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
		[SerializeField] private float spawnDelay = 0f;
		private MusicBox _musicBox;
		private bool isOnce;
		private Coroutine spawnCoroutine;

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
				spawnCoroutine = StartCoroutine(Spawn());
			}
		}
		private IEnumerator Spawn()
		{
			yield return new WaitForSeconds(spawnDelay);
			spawner.Spawn();
		}
        private void OnDestroy()
        {
			if(spawnCoroutine != null)
				StopCoroutine(spawnCoroutine);
        }
    }
}
