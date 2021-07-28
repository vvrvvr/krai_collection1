using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedIO : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<RunWriteController>())
        {
            collision.GetComponent<RunWriteController>().YellowWrite();
            if (GetComponentInParent<Transform>())
                GetComponentInParent<Transform>().gameObject.SetActive(false);

            HandController.Instance.PlaySound(clip);
        }
    }
}
