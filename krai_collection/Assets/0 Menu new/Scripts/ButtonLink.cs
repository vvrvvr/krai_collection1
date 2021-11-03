using UnityEngine;

public class ButtonLink : MonoBehaviour
{
    [SerializeField] private string link;
    public void OpenLink()
    {
        Application.OpenURL(link);
    }
}
