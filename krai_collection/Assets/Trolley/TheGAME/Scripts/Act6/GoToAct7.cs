using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace krai_trol
{
	public class GoToAct7 : MonoBehaviour
	{
		[SerializeField] protected MainMenu _mainMenu;
		[SerializeField] protected string _act;

		[Space]
		[SerializeField] private float _musicLength;
		[SerializeField] private float _timeAfterLeaveBus;

		private void Start()
		{
			StartCoroutine(Delay(_musicLength));
		}

		private IEnumerator Delay(float seconds)
		{
			yield return new WaitForSeconds(seconds);
			_mainMenu.LoadScene(_act);
		}

		public void PlayerExit()
		{
			StartCoroutine(Delay(_timeAfterLeaveBus));
		}

		private void OnDestroy()
		{
			StopAllCoroutines();
		}
	}
}