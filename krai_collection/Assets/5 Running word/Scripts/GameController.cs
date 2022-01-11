using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance => instance;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endMenu;
    [SerializeField] private Text playerText;
    [SerializeField] private Text poetText;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InstantiateLevel.Instance.moveSpeed = 0;
        RunWriteController.Instance.playGame = false;
        pauseMenu.SetActive(false);
    }

    public void ManualStart()
    {
        InstantiateLevel.Instance.moveSpeed = 1;
        RunWriteController.Instance.playGame = true;
    }

    public void ManualRestart()
    {
        MainMenu.Instance.LoadGame();
    }

    public void ManualEndGame()
    {
        InstantiateLevel.Instance.moveSpeed = 0;
        RunWriteController.Instance.playGame = false;
        endMenu.SetActive(true);
        playerText.text = AssetText.Instance.playerWord;
        poetText.text = AssetText.Instance.poetWord;
    }

    public void ExitGame()
    {
        MainMenu.Instance.ExitGame();
    }

    public void ExitMainMenu()
    {
        MainMenu.Instance.LoadMainMenu();
    }

    public void PauseGame()
    {
        if (!pauseMenu.activeInHierarchy)
            pauseMenu.SetActive(true);
        else
            pauseMenu.SetActive(false);

        MainMenu.Instance.Pause();
    }

    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }
}
