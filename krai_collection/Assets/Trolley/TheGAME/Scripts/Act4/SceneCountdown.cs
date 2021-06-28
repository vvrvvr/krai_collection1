using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCountdown : MonoBehaviour
{
	[SerializeField] private MusicBoxAct4 _musicBox;
	[SerializeField] private KeepDistance _mountain;
	[SerializeField] private GameObject _bus;

	[SerializeField] private float _musicPlaySeconds;
	[SerializeField] private float _minMountainDistance;
	[SerializeField] private string _loadAct;

	[SerializeField] private MainMenu _mainMenu;

	private void Start()
	{
		StartCoroutine(MusicStopAtTime());
	}

	private IEnumerator MusicStopAtTime()
	{
		yield return new WaitForSeconds(_musicPlaySeconds);
		_musicBox.StopMusic();
		MoutainCanMove();
	}

	private void MoutainCanMove()
	{
		_mountain.YouCanComeToTargetZ();
		StartCoroutine(TrakeMountainPlayerDistance());
	}

	private IEnumerator TrakeMountainPlayerDistance()
	{
		do
		{
			if (Mathf.Abs ( _bus.transform.position.z - _mountain.transform.position.z) < _minMountainDistance)
				_mainMenu.LoadScene(_loadAct);

			yield return new WaitForSeconds(1f);
		} while (true);
	}
}
