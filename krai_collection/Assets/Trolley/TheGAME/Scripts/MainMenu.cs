using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private Fade _fade;
	[SerializeField] private GameObject mainWindow;
	[SerializeField] private Slider volume;
	[SerializeField] private MusicBox musicBox;

	private readonly int Act1Index = 1; 

	private void Start()
	{
		_fade.FadeIn();
		volume.value = PlayerPrefs.GetFloat("Volume", 1);		
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			mainWindow.SetActive(!mainWindow.activeSelf);
			Cursor.visible = mainWindow.activeSelf;
			if (mainWindow.activeSelf) Time.timeScale = 0;
			else Time.timeScale = 1;
		}

		if (Input.GetKeyDown(KeyCode.R))
			RestartLevel();
	}

	public void StartNewGame()
	{
		StartCoroutine(LoadSceneWhithFade("Act1_Testing"));
		Time.timeScale = 1;
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1;
	}

	public void Exit()
	{
		SceneManager.LoadScene("main menu");
	}

	public void ChangeVolume(float value)
	{
		musicBox.ChangeVolume(value);
	}

	public void LoadScene(string sceneName)
	{
		StartCoroutine(LoadSceneWhithFade(sceneName));
	}

	private IEnumerator LoadSceneWhithFade(string sceneName)
	{
		if (_fade != null)
		{
			_fade.gameObject.SetActive(true);
			_fade.FadeOut();
			yield return new WaitForSeconds(_fade.FadeOutTime);
		}
		SceneManager.LoadScene(sceneName);
	}
}
