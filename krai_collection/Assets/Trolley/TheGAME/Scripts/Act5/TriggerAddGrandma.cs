using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace krai_trol
{
	public class TriggerAddGrandma : MonoBehaviour
	{
		[SerializeField] private GrandmasSpawner spawner;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == "Bus")
				spawner.Spawn();
		}
	}
}
