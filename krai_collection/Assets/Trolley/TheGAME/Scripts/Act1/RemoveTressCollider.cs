using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTressCollider : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Bus") transform.parent.gameObject.SetActive(false);
	}
}

