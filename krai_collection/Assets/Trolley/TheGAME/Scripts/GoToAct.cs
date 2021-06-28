using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToAct : MonoBehaviour
{
	[SerializeField] protected MainMenu _mainMenu;
	[SerializeField] protected int _act;
	private void OnTriggerEnter(Collider other)
	{
		_mainMenu.LoadScene(_act);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		_mainMenu.LoadScene(_act);
	}
}
