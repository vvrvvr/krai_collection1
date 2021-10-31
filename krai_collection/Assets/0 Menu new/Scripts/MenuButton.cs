using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private VideoPlayer video;
    private bool isOverObject = false;
    // Start is called before the first frame update
    void Start()
    {
        video.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            var obj = EventSystem.current;
            Debug.Log($"object = {obj.gameObject.name}");
            if (!isOverObject && obj.gameObject.name == gameObject.name)
            {
                Debug.Log("over");
                isOverObject = true;
                video.Play();
            }
        }
        else
        {
            
            if (isOverObject)
            {
                Debug.Log("exit");
                isOverObject = false;
                video.Stop();
            }
        }
    }

}
