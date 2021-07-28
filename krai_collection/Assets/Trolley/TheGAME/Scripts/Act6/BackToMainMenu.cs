using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace krai_trol
{
	public class BackToMainMenu : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Player")
				SceneManager.LoadScene(0);
		}
	}
}
