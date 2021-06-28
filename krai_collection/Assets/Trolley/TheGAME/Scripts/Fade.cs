using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
	[SerializeField] private Image _image;
	[Space]
	public float FadeInTime = 2;
	public float FadeOutTime = 2;

	private void Start()
	{
		_image.color = Color.black;
	}

	private IEnumerator FadeInCoroutine(float seconds)
	{
		//_image.gameObject.SetActive(true);

		var delfaColorA = (1 / seconds) * Time.fixedDeltaTime;
		_image.color = Color.black;
		do
		{
			_image.color -= new Color(0, 0, 0, 1) * delfaColorA;

			seconds -= Time.fixedDeltaTime;
			yield return new WaitForFixedUpdate();
		} while (seconds > 0);

		_image.gameObject.SetActive(false);
	}

	private IEnumerator FadeOutCoroutine(float seconds)
	{
		_image.gameObject.SetActive(true);
		_image.color = new Color(0, 0, 0, 0);

		var delfaColorA = (1 / seconds) * Time.fixedDeltaTime;
		do
		{
			_image.color += new Color(0, 0, 0, 1) * delfaColorA;

			seconds -= Time.fixedDeltaTime;
			yield return new WaitForFixedUpdate();
		} while (seconds > 0);

		//_image.gameObject.SetActive(false);
	}

	public void FadeIn()
	{
		StartCoroutine(FadeInCoroutine(FadeInTime));
	}

	public void FadeOut()
	{
		StartCoroutine(FadeOutCoroutine(FadeOutTime));
	}

	public void FadeIn(float seconds)
	{
		StartCoroutine(FadeInCoroutine(seconds));
	}

	public void FadeOut(float seconds)
	{
		StartCoroutine(FadeOutCoroutine(seconds));
	}
}
