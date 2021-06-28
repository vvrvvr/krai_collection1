using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmasSpawner : MonoBehaviour
{
	public float spawnDealay;
	public float maxGrandmasCount;
	public GameObject[] grandmas;
	public GameObject[] spawnPoints;


	private void Start()
	{
		//StartCoroutine(SpawnCoroutine());
	}


	private IEnumerator SpawnCoroutine()
	{
		do
		{
			yield return new WaitForSeconds(spawnDealay);
		
			var spawn = spawnPoints[Random.Range(0,spawnPoints.Length-1)];
			var grandma = grandmas[Random.Range(0, grandmas.Length-1)];

			Instantiate(grandma,spawn.transform.position, new Quaternion(), transform);

		} while (transform.childCount < maxGrandmasCount);
	}

	public void Spawn()
	{
		var spawn = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
		var grandma = grandmas[Random.Range(0, grandmas.Length - 1)];

		Instantiate(grandma, spawn.transform.position, new Quaternion(), transform);
	}
}

