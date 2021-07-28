using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwap : MonoBehaviour
{
    [SerializeField] private Sprite imageActive;
    [SerializeField] private Sprite imageDeactive;
    public bool active = true;

    public void Swap()
    {
        active = false;

        if (gameObject.GetComponent<SpriteRenderer>().sprite.name == imageActive.name)
        {
            if (imageDeactive != null)
                gameObject.GetComponent<SpriteRenderer>().sprite = imageDeactive;
        }
        else
            if (imageActive != null)
                gameObject.GetComponent<SpriteRenderer>().sprite = imageActive;

    }
}
