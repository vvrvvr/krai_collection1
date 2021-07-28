using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HandController : MonoBehaviour
{
    private static HandController instance;
    public static HandController Instance => instance;
    private AudioSource source;

    [SerializeField] private float basePos = 8.0f;
    [SerializeField] private float waitPos = 5.0f;
    [SerializeField] private float attackPos = 2.0f;

    [SerializeField] private float attackSpeed = 2.0f;

    [SerializeField] private Sprite openHand;
    [SerializeField] private Sprite closeHand;
    private SpriteRenderer handVisual;

    [SerializeField] private AudioClip[] clip = new AudioClip[3];

    private float handPos = 0.0f;

    private int vector = 1;

    [SerializeField] private int nextAttackTime;

    private BoxCollider2D col;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<RunWriteController>())
        {
            PlaySound(clip[Random.Range(0, clip.Length - 1)]);

            RunWriteController.Instance.WriteBeAttack();
            col.enabled = false;
        }
    }

    public void PlaySound(AudioClip _clip)
    {
        source.PlayOneShot(_clip);
    }

    private void Start()
    {
        instance = this;
        source = GetComponent<AudioSource>();
        handVisual = GetComponent<SpriteRenderer>();
        gameObject.transform.position = new Vector2(-4, basePos);
        handPos = basePos;
        col = GetComponent<BoxCollider2D>();
        Invoke("StartAttack", 3);
    }

    private void StartAttack()
    {
        StartCoroutine(HandAttack());
    }

    private void FixedUpdate()
    {
        if (!RunWriteController.Instance.playGame) return;

        Move();
    }

    private void Move()
    {
        gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, new Vector2(gameObject.transform.position.x, handPos), attackSpeed);
    }

    private IEnumerator HandAttack()
    {
        int startVector = vector;

        yield return new WaitForSeconds(nextAttackTime);

        handPos = waitPos;

        yield return new WaitForSeconds(1.5f);

        handPos = attackPos;

        if (vector == -1)
        while (gameObject.transform.position.y < handPos - 0.2f)
        {
            yield return new WaitForEndOfFrame();
        }
        else
            while (gameObject.transform.position.y > handPos + 0.2f)
            {
                yield return new WaitForEndOfFrame();
            }

        handVisual.sprite = closeHand;
        handPos = basePos;

        yield return new WaitForSeconds(1);
        vector = Random.Range(-1, 1);
        if (vector == 0) vector = 1;

        if (vector != startVector)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y * -1, gameObject.transform.localScale.z);
            basePos *= -1;
            handPos = basePos;
            waitPos *= -1;
            attackPos *= -1;
            gameObject.transform.position = new Vector3(Random.Range(-7f, -3f), handPos, gameObject.transform.position.z);
        }
        else
            gameObject.transform.position = new Vector3(Random.Range(-7f, -3f), handPos, gameObject.transform.position.z);

        col.enabled = true;
        handVisual.sprite = openHand;
        StartCoroutine(HandAttack());
    }
}
