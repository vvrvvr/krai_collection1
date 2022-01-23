using UnityEngine.SceneManagement;
using UnityEngine;

public class Endings : MonoBehaviour
{
    public int Ending;
    public static Endings Singleton;
    public int[] openedEndings = new int[5];
     public bool isRussian = true;
    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
        if(LanguageSettings.Singleton != null)
            isRussian = LanguageSettings.Singleton.isRussian;
        DontDestroyOnLoad(gameObject);
    }
    public void WriteEnding(int i)
    {
        Ending = i;
        openedEndings[i] = 1;
    }
    //public void SetLanguageEnglishAndStart()
    //{
    //    isRussian = false;
    //    SceneManager.LoadScene("flower_game");
    //}
    //public void StartGame()
    //{
    //    isRussian = true;
    //    SceneManager.LoadScene("flower_game");
    //}
}
