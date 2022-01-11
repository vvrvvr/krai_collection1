using UnityEngine;

public class videoBool : MonoBehaviour
{
    public static videoBool Singleton;
    public bool isVideoPlaying = true;
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
