using UnityEngine;

public class ButtonLink : MonoBehaviour
{
    [SerializeField] private string link;
    public Texture2D cursor1;
    public void OpenLink()
    {
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
}
