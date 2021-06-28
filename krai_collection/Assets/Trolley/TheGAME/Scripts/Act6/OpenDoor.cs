using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
	[SerializeField] private GameObject trlleybusClosedDoor;
	[SerializeField] private GameObject trlleybusOpenDoor;

	[SerializeField] private float delay;

	private void Start()
	{
		StartCoroutine(OpenWithDelay());
	}

	private IEnumerator OpenWithDelay()
	{
		yield return new WaitForSeconds(delay);
		trlleybusOpenDoor.SetActive(true);
		trlleybusClosedDoor.SetActive(false);
	}

}
