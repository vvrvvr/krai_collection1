using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAddGrandma : MonoBehaviour
{
	[SerializeField] private GrandmasSpawner spawner; 

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Bus")
			spawner.Spawn();
	}
}
