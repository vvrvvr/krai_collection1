using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetText : MonoBehaviour
{
    private static AssetText instance;
    public static AssetText Instance => instance;

    public string poetWord = "";
    public string playerWord = "";

    [System.Serializable]
    public struct writeCollection
    {
        public string originWrite;
        public string anonimWrite;
        public string[] translateWrite;
        public string[] anonimlateWrite;
    }

    [System.Serializable]
    public struct wordCollection
    {
        public writeCollection[] write;
    }

    public wordCollection[] word = new wordCollection[3];
    public wordCollection[] wordRussian = new wordCollection[3];
    public wordCollection[] wordEnglish = new wordCollection[3];

    private void Awake()
    {
        instance = this;
    }


    public void UpgradePlayerWord(string text)
    {
        playerWord = playerWord + " " + text;
    }

    public void UpgradePoetWord(string text)
    {
        poetWord = poetWord + " " + text;
    }
}
