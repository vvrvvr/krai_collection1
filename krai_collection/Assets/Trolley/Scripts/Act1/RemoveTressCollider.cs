using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace krai_trol
{
	public class RemoveTressCollider : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Bus") transform.parent.gameObject.SetActive(false);
		}
	}
}
