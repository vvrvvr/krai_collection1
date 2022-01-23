using UnityEngine;

public class LanguageSettings : MonoBehaviour
{
    public static LanguageSettings Singleton;
    public bool isRussian;

    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
        DontDestroyOnLoad(gameObject);
    }

}
