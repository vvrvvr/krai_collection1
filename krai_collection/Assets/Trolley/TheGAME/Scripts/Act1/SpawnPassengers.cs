using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPassengers : MonoBehaviour
{
	[SerializeField] private float spawnRate;

	[SerializeField] private GameObject[] passengers;

    private Busstop[] _busstops;

	private void Start()
	{
		_busstops = GetComponentsInChildren<Busstop>();
		StartCoroutine(Spawn());
	}

	private IEnumerator Spawn()
	{
		do
		{
			_busstops[Random.Range(0, _busstops.Length)].SetPassenger(passengers[Random.Range(0, passengers.Length)]);
			yield return new WaitForSeconds(1 / spawnRate);
		} while (true);
	}
}
