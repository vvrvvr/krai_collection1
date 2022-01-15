using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace krai_trol
{
	public class Titles : MonoBehaviour
	{
		[SerializeField] private Fade _fade;
		[SerializeField] private int _act;
		[Space]
		[SerializeField] private float _titelShowTime;

		private Text[] _titles;

		private void Start()
		{
			_titles = gameObject.GetComponentsInChildren<Text>();
			foreach (var title in _titles) title.gameObject.SetActive(false);
			StartCoroutine(ShowTitles());
		}


		private IEnumerator ShowTitles()
		{
			foreach (var title in _titles)
			{
				title.gameObject.SetActive(true);

				_fade.FadeIn();
				yield return new WaitForSeconds(_fade.FadeInTime);

				yield return new WaitForSeconds(_titelShowTime);

				_fade.FadeOut();
				yield return new WaitForSeconds(_fade.FadeOutTime);

				title.gameObject.SetActive(false);
			}
			SceneManager.LoadScene(_act);
		}
	}
}
