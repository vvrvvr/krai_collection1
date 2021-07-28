using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteIO : MonoBehaviour
{
    public TextMesh write;
    [SerializeField] private AudioClip clip;

    private void OnEnable()
    {
        if (!AssetText.Instance) return;

        int count = Random.Range(0, AssetText.Instance.word.Length);


        write.text = AssetText.Instance.word[count].write[0].originWrite + " " +
                        AssetText.Instance.word[count].write[1].originWrite + " " +
                            AssetText.Instance.word[count].write[2].originWrite;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<RunWriteController>())
        {
            collision.GetComponent<RunWriteController>().DeactiveWrite(write.text);
            if (GetComponentInParent<Transform>())
                GetComponentInParent<Transform>().gameObject.SetActive(false);

            HandController.Instance.PlaySound(clip);
        }
    }
}
