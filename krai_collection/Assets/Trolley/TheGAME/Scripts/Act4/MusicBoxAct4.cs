using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace krai_trol
{
	public class MusicBoxAct4 : MusicBox
	{
		[Space]
		public float seconds;

		private void Start()
		{
			StartCoroutine(Delay());
		}

		private IEnumerator Delay()
		{
			yield return new WaitForSeconds(seconds);
			PlayMusic();
		}

	}
}
