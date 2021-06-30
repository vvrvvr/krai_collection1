using UnityEngine;

public class CollectionMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        var endSingletone = GameObject.FindGameObjectsWithTag("delete");
        if (endSingletone != null)
        {
            foreach (var item in endSingletone)
            {
                Destroy(item);
            }
        }
    }
}
