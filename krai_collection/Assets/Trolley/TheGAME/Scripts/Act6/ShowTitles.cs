using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowTitles : MonoBehaviour
{
	[SerializeField] private float startDelay = 4;
	[SerializeField] private float stay = 3;
	[SerializeField] private float fadeIn = 1;
	[SerializeField] private float fadeOut = 1;
	[SerializeField] private TextMeshProUGUI [] titles;

	private void Start()
	{
		//titles[0].color = new Color();
		StartCoroutine(ShowTitel());
	}

	private IEnumerator ShowTitel()
	{
		yield return new WaitForSeconds(startDelay);

		for (int i = 0; i < titles.Length; i++)
		{
			StartCoroutine(FadeIn(titles[i], fadeIn));
			yield return new WaitForSeconds(fadeIn);

			yield return new WaitForSeconds(stay);

			StartCoroutine(FadeOut(titles[i], fadeOut));
			yield return new WaitForSeconds(fadeOut);
		}
	}

	private IEnumerator FadeIn(TextMeshProUGUI titel, float seconds)
	{
		var color = titel.color;
		color.a = 0;
		titel.color = color;

		var shift = seconds * Time.fixedDeltaTime;

		do
		{
			color.a += shift;
			titel.color = color;

			seconds -= Time.fixedDeltaTime;

			yield return new WaitForFixedUpdate();
		} while (seconds > 0);
	}

	private IEnumerator FadeOut(TextMeshProUGUI titel, float seconds)
	{
		var color = titel.color;
		color.a = 1;
		titel.color = color;

		var shift = seconds * Time.fixedDeltaTime;

		do
		{
			color.a -= shift;
			titel.color = color;

			seconds -= Time.fixedDeltaTime;

			yield return new WaitForFixedUpdate();
		} while (seconds > 0);
	}
}
