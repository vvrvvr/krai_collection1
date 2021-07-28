using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunWriteController : MonoBehaviour
{
    private static RunWriteController instance;
    public static RunWriteController Instance => instance;

    public bool playGame = false;
    [SerializeField] private GameObject[] whiteMass = new GameObject[3];
    [SerializeField] private float angleMoveSpeed = 5.0f;
    [SerializeField] private float levelBorder = 3;
    [SerializeField] private float multiplayLastWhrite = 0.2f;

    [System.Serializable]
    private struct writeAndtype
    {
        public TextMesh write;
        public bool anonim;
    }

    private writeAndtype[] wordText;

    [SerializeField] private Color blueColors;
    [SerializeField] private Color orangeColors;
    [SerializeField] private Color redColors;

    private float angle = 0.0f;
    private int count = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levelBorder = Camera.main.orthographicSize;
        wordText = new writeAndtype[whiteMass.Length];

        count = Random.Range(0, AssetText.Instance.word.Length - 1);

        for (int i = 0; i < whiteMass.Length; i++)
        {
            wordText[i].write = whiteMass[i].GetComponentInChildren<TextMesh>();
            wordText[i].write.text = AssetText.Instance.word[count].write[i].originWrite;
            wordText[i].anonim = false;
        }
    }

    private void Update()
    {
        if (!playGame) return;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(1, gameObject);
            angle = Mathf.Clamp((angle + 0.3f), -8, 8);
        }
        else
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(-1, gameObject);
            angle = Mathf.Clamp((angle - 0.3f), -8, 8);
        }
        else
        {
            if (angle > 0.01f)
                angle -= 0.3f;
            else
                if (angle < 0.01f) angle += 0.3f;
        }

        transform.eulerAngles = new Vector3(0, 0, angle);
        LastWhriteMove();
    }

    private void Move(float vector, GameObject _go)
    {
        float _moveXAxis = (_go.transform.position.y + vector * angleMoveSpeed);
        _go.transform.position = new Vector3(_go.transform.position.x, (Mathf.Clamp(_moveXAxis, -levelBorder + _go.transform.localScale.y, levelBorder - _go.transform.localScale.y)),
        _go.transform.position.z);
    }




    private void ReturnPos(GameObject _go, GameObject target, float speed)
    {
        if (_go.transform.position.y > target.transform.position.y)
            Move(-1 + speed, _go);
        else
            if (_go.transform.position.y < target.transform.position.y)
                Move(1 - speed, _go);
    }

    private void LastWhriteMove()
    {
        for (int i = 0; i < whiteMass.Length; i++)
        {
            if (whiteMass.Length <= 1) break;

            if (i != 0)
            {
                Vector3 direction = whiteMass[i - 1].transform.position - whiteMass[i].transform.position;
                whiteMass[i].transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                whiteMass[i].transform.Rotate(0, 0, 90);

                if (Vector2.Distance(whiteMass[i].transform.position, new Vector2(whiteMass[i].transform.position.x, whiteMass[i - 1].transform.position.y)) > 0.05f)
                {
                    ReturnPos(whiteMass[i], whiteMass[i - 1], i * multiplayLastWhrite);
                }
                      
            }
        }
    }

    public void DeactiveWrite(string text)
    {
        for (int i = 0; i < whiteMass.Length; i++)
        {
            ImageSwap image = whiteMass[i].GetComponent<ImageSwap>();

            if (image != null && image.active)
            {
                image.Swap();
                image.active = false;
                if (whiteMass[i].GetComponent<SpriteRenderer>().color != redColors)
                    whiteMass[i].GetComponent<SpriteRenderer>().color = Color.white;
                AssetText.Instance.UpgradePlayerWord(text + " " + wordText[i].write.text);
                wordText[i].write.text = "";
                InstantiateLevel.Instance.SwapBackground(i);

                if (i == whiteMass.Length - 1)
                    GameController.Instance.ManualEndGame();
                else
                    return;
            }
        }

        if (GameController.Instance) GameController.Instance.ManualEndGame();
    }

    public void WriteBeAttack()
    {
        for (int i = 0; i < whiteMass.Length; i++)
        {
            if (whiteMass[i].GetComponent<SpriteRenderer>().color != redColors)
            {
                whiteMass[i].GetComponent<SpriteRenderer>().color = redColors;

                int countW = Random.Range(0, AssetText.Instance.word.Length);
                string text = AssetText.Instance.word[countW].write[0].originWrite + " " +
                                AssetText.Instance.word[countW].write[1].originWrite + " " +
                                    AssetText.Instance.word[countW].write[2].originWrite;
                AssetText.Instance.UpgradePoetWord(text + " " + wordText[i].write.text);

                if (i == whiteMass.Length - 1)
                {
                    if (GameController.Instance) GameController.Instance.ManualEndGame();
                }
                else
                    return;
            }
        }

        GameController.Instance.ManualEndGame();
    }

    public void YellowWrite()
    {
        for (int i = 0; i < whiteMass.Length; i++)
        {
            if (whiteMass[i].GetComponent<ImageSwap>().active)
            {
                if (!wordText[i].anonim)
                {
                    if (whiteMass[i].GetComponent<SpriteRenderer>().color != redColors)
                        whiteMass[i].GetComponent<SpriteRenderer>().color = orangeColors;

                    wordText[i].write.text = AssetText.Instance.word[count].write[i].anonimWrite;
                    wordText[i].anonim = true;
                }
                else
                {
                    if (whiteMass[i].GetComponent<SpriteRenderer>().color != redColors)
                        whiteMass[i].GetComponent<SpriteRenderer>().color = Color.white;

                    wordText[i].write.text = AssetText.Instance.word[count].write[i].originWrite;
                    wordText[i].anonim = false;
                }

            }
        }
    }

    public void BlueWrite()
    {
        int value = Random.Range(0, 1);
        for (int i = 0; i < whiteMass.Length; i++)
        {
            if (whiteMass[i].GetComponent<ImageSwap>().active)
            {
                if (!wordText[i].anonim)
                {
                    wordText[i].write.text = AssetText.Instance.word[count].write[i].translateWrite[value];
                    if (whiteMass[i].GetComponent<SpriteRenderer>().color != redColors)
                        whiteMass[i].GetComponent<SpriteRenderer>().color = blueColors;
                }
                else
                {
                    wordText[i].write.text = AssetText.Instance.word[count].write[i].anonimlateWrite[value];
                    if (whiteMass[i].GetComponent<SpriteRenderer>().color != redColors)
                        whiteMass[i].GetComponent<SpriteRenderer>().color = blueColors;
                }
            }
        }
    }
}
