using UnityEngine;

public class ButtonLink : MonoBehaviour
{
    [SerializeField] private string link;
    public Texture2D cursor1;
    public void OpenLink()
    {
        if (link == "") 
            return;
        Application.OpenURL(link);
    }
    public void OnMouseOver()
    {
        Cursor.SetCursor(cursor1, Vector2.zero, CursorMode.Auto);
    }
    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
             Application.Quit();
#endif
    }
}
