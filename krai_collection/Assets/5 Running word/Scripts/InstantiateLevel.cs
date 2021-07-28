using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InstantiateLevel : MonoBehaviour
{
    private static InstantiateLevel instance;
    public static InstantiateLevel Instance => instance;

      //типы и слои объектов из которых строится уровень
      private enum ObjType { InterObj1, InterObj2, InterObj3 , background }
      private enum LayerInGame { interactiveObj, level_1 }

      [System.Serializable]
      private struct MaxMinValue
      {
          [Range(0, 100)] public float MinValue;
          [Range(0, 100)] public float MaxValue;
      }

      [SerializeField] private MaxMinValue _IO1SpawnChance;
      [SerializeField] private MaxMinValue _IO2SpawnChance;
      [SerializeField] private MaxMinValue _IO3SpawnChance;

      private float distance = 0.0f;
      [SerializeField] private float spawnIntDistance = 1.0f;


      [HideInInspector] public float moveLevelINTER = 0;
      [HideInInspector] public float moveLevelFIRST = 0;


      [System.Serializable]
      private struct objAtScene
      {
          public GameObject objPrefab;
          public ObjType objType;
          public LayerInGame layer;
      }

      [System.Serializable]
      private struct objInLayer
      {
          public GameObject objPrefab;
          public ObjType objType;

          public objInLayer(GameObject obj, ObjType type)
          {
              objPrefab = obj;
              objType = type;
          }
      }

      private List<objInLayer> interactiveLevelObj = new List<objInLayer>();//создаем общий список по структуре в которой указываем ссылку на ранее созданный объект, а так же его тип и слой
      [SerializeField] private float _InteractiveLSpeedValue = 1;
      [SerializeField] private float _ILSpawnValue = 1; //переменные которые влияют на то, какая будет задержка спавна для каждого уровня
      private List<objInLayer> firstLevelObj = new List<objInLayer>();
      [SerializeField] private float _FirstLSpeedValue = 0.80f;
      [SerializeField] private float _FLSpawnValue = 0.9f;

      [SerializeField] private float levelObjSpeed = 1;

      [SerializeField] private objAtScene[] assemblyObj = new objAtScene[0];
      [SerializeField] private int cloneValue = 5;
      private float OrthSize = 5;

      [HideInInspector] public float moveSpeed = 1.0f;

      public float spawnPosition = 5.0f;
      [HideInInspector] public float spawnPositionX = 12;

      private void Awake()
      {
          instance = this;
          OrthSize = Camera.main.orthographicSize;
          LevelInitialization();
      }

      private void Start()//задаем скорость уровню, причем важно, 0.02f это Time.deltaTime, но почему-то при перезапуске уровня значение меняется
      {
          moveLevelINTER = (levelObjSpeed * _InteractiveLSpeedValue) * 0.02f * -2.5f;
          moveLevelFIRST = (levelObjSpeed * _FirstLSpeedValue) * 0.02f * -2.5f;
      }

      private float IO1Size;
      private float IO2Size;
      private float IO3Size;
      private float backgroundSize;

    private void LevelInitialization()
      {
          for (int i = 0; i < assemblyObj.Length; i++)
          {
              if (assemblyObj[i].objPrefab != null)
              {
                  GameObject GO = assemblyObj[i].objPrefab;
                  float goSizeY = 0;
                  if (GO.GetComponent<SpriteRenderer>()) goSizeY = GO.GetComponent<SpriteRenderer>().size.y / 2;//половина размера объекта


                  switch (assemblyObj[i].objType)
                  {

                      case ObjType.InterObj1:
                          for (int j = 0; j <= cloneValue; j++)
                          {
                              GameObject _go = Instantiate(GO, new Vector3(-spawnPositionX, 0), Quaternion.identity);

                              switch (assemblyObj[i].layer)
                              {
                                  case LayerInGame.interactiveObj:
                                      interactiveLevelObj.Add(new objInLayer(_go, assemblyObj[i].objType));
                                      _go.SetActive(false);
                                      break;
                                  case LayerInGame.level_1:
                                      firstLevelObj.Add(new objInLayer(_go, assemblyObj[i].objType));
                                      _go.SetActive(false);
                                      break;
                                  default:
                                      Debug.LogError(GO.name + " слой объекта не определен");
                                      break;
                              }
                          }

                          IO1Size = goSizeY;
                          break;

                      case ObjType.InterObj2:
                          for (int j = 0; j <= cloneValue; j++)
                          {
                              GameObject _go = Instantiate(GO, new Vector3(-spawnPositionX, 0), Quaternion.identity);

                              switch (assemblyObj[i].layer)
                              {
                                  case LayerInGame.interactiveObj:
                                      interactiveLevelObj.Add(new objInLayer(_go, assemblyObj[i].objType));
                                      _go.SetActive(false);
                                      break;
                                  case LayerInGame.level_1:
                                      firstLevelObj.Add(new objInLayer(_go, assemblyObj[i].objType));
                                      _go.SetActive(false);
                                      break;
                                  default:
                                      Debug.LogError(GO.name + " слой объекта не определен");
                                      break;
                              }
                          }

                          IO2Size = goSizeY;
                          break;

                      case ObjType.InterObj3:
                          for (int j = 0; j <= cloneValue; j++)
                          {
                              GameObject _go = Instantiate(GO, new Vector3(-spawnPositionX, 0), Quaternion.identity);

                              switch (assemblyObj[i].layer)
                              {
                                  case LayerInGame.interactiveObj:
                                      interactiveLevelObj.Add(new objInLayer(_go, assemblyObj[i].objType));
                                      _go.SetActive(false);
                                      break;
                                  case LayerInGame.level_1:
                                      firstLevelObj.Add(new objInLayer(_go, assemblyObj[i].objType));
                                      _go.SetActive(false);
                                      break;
                                  default:
                                      Debug.LogError(GO.name + " слой объекта не определен");
                                      break;
                              }
                          }

                          IO3Size = goSizeY;
                          break;

                    case ObjType.background:
                        if (GO.GetComponent<SpriteRenderer>()) backgroundSize = GO.GetComponent<SpriteRenderer>().size.x;
                        for (int j = 0; j <= cloneValue; j++)
                        {
                            GameObject _go = Instantiate(GO, new Vector3(-spawnPositionX, 0), Quaternion.identity);

                            switch (assemblyObj[i].layer)
                            {
                                case LayerInGame.interactiveObj:
                                    interactiveLevelObj.Add(new objInLayer(_go, assemblyObj[i].objType));
                                    if (j == 0) _go.transform.position = new Vector2(0, 0);
                                    else
                                        if (j == 1) _go.transform.position = new Vector2(backgroundSize, 0);
                                    else
                                       _go.SetActive(false);
                                    break;
                                case LayerInGame.level_1:
                                    firstLevelObj.Add(new objInLayer(_go, assemblyObj[i].objType));
                                    if (j == 0) _go.transform.position = new Vector2(0, 0);
                                    else
                                       if (j == 1) _go.transform.position = new Vector2(backgroundSize, 0);
                                    else
                                        _go.SetActive(false);
                                    break;
                                default:
                                    Debug.LogError(GO.name + " слой объекта не определен");
                                    break;
                            }
                        }

                        
                        break;

                    default:
                          Debug.LogError(GO.name + " тип объекта не определен");
                          break;
                  }
              }
          }

          SpawnInteractiveLayerObj(ObjType.InterObj1);
          System.GC.Collect();//очищаем временную память перед запуском основной игры
      }//создаем пул объектов для уровня

      private GameObject GetFreeObj(List<objInLayer> ObjList, ObjType type)//ищем свободный объект в пуле, который не активен, после чего активируем его
      {
          List<GameObject> _go = new List<GameObject>();
          for (int i = 0; i < ObjList.Count; i++)
          {
              if (!ObjList[i].objPrefab.activeInHierarchy && ObjList[i].objType == type)
              {
                  _go.Add(ObjList[i].objPrefab);
              }
          }
          if (_go.Count != 0)
              return _go[Random.Range(0, _go.Count)];
          else
              return null;
      }


      private void SpawnInteractiveLayerObj(ObjType lastType)
      {

          //реализация шанса выдачи конкретного объекта
          float _chanceSpawn = Random.Range(1, 100);
          if (lastType != ObjType.InterObj1 && (_chanceSpawn >= _IO1SpawnChance.MinValue && _chanceSpawn <= _IO1SpawnChance.MaxValue))
          {
              GameObject _go = GetFreeObj(interactiveLevelObj, ObjType.InterObj1);

              if (_go == null)
              {
                  SpawnInteractiveLayerObj(lastType);
              }
              else
                  StartCoroutine(CreateIO1Interactive(_go));
          }
          else
          if (lastType != ObjType.InterObj2 && (_chanceSpawn >= _IO2SpawnChance.MinValue && _chanceSpawn <= _IO2SpawnChance.MaxValue))
          {
              GameObject _go = GetFreeObj(interactiveLevelObj, ObjType.InterObj2);

              if (_go == null)
              {
                  SpawnInteractiveLayerObj(lastType);
              }
              else
                  StartCoroutine(CreateIO2Interactive(_go));
          }
          else
          if (lastType != ObjType.InterObj3 && (_chanceSpawn >= _IO3SpawnChance.MinValue && _chanceSpawn <= _IO3SpawnChance.MaxValue))
          {
              GameObject _go = GetFreeObj(interactiveLevelObj, ObjType.InterObj3);

              if (_go == null)
              {
                  SpawnInteractiveLayerObj(lastType);
              }
              else
                  StartCoroutine(CreateIO3Interactive(_go));
          }
          else//если кубик не попал ни в один из интервалов указанных пользователем, то функция запускает создание еще одной платформы
          {
              GameObject _go = GetFreeObj(interactiveLevelObj, ObjType.InterObj1);

              if (_go == null)
              {
                  SpawnInteractiveLayerObj(lastType);
              }
              else
                  StartCoroutine(CreateIO1Interactive(_go));
          }
      }

      //Корутины по генерации уровня
      private IEnumerator CreateIO1Interactive(GameObject _go)
      {
          distance = 0.0f;
          while (distance < spawnIntDistance)
          {
              distance += moveSpeed * 0.01f;
              yield return new WaitForSeconds(0.01f);
          }

          _go.SetActive(true);


              float yPos = Random.Range(-OrthSize + IO1Size, OrthSize - IO1Size);//позиция платформы
              _go.transform.position = new Vector2(spawnPositionX, yPos);//axisValue должна быть отрицательной, если мы бежим вперед.


          SpawnInteractiveLayerObj(ObjType.InterObj1);//внимание, надо заменить на null ObjType
      }

    private IEnumerator CreateIO2Interactive(GameObject _go)
    {
        distance = 0.0f;
        while (distance < spawnIntDistance)
        {
            distance += moveSpeed * 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        _go.SetActive(true);

        float _dP = Random.Range(1, 100);//кидаем кубик, так сказать
        if (_dP < 50)
        {
            float yPos = Random.Range(-OrthSize + IO2Size, OrthSize - IO2Size);//позиция платформы
            _go.transform.position = new Vector2(spawnPositionX, yPos);//axisValue должна быть отрицательной, если мы бежим вперед.
        }

        SpawnInteractiveLayerObj(ObjType.InterObj2);
    }

    private IEnumerator CreateIO3Interactive(GameObject _go)
    {
        distance = 0.0f;
        while (distance < spawnIntDistance)
        {
            distance += moveSpeed * 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        _go.SetActive(true);

        float _dP = Random.Range(1, 100);//кидаем кубик, так сказать
        if (_dP < 50)
        {
            float yPos = Random.Range(-OrthSize + IO3Size, OrthSize - IO3Size);//позиция платформы
            _go.transform.position = new Vector2(spawnPositionX, yPos);//axisValue должна быть отрицательной, если мы бежим вперед.
        }

        SpawnInteractiveLayerObj(ObjType.InterObj3);
    }

    private void CreateBackground(GameObject obj)
    {
        if (obj.transform.position.x < -(backgroundSize))
        {
            GameObject _go = GetFreeObj(firstLevelObj, ObjType.background);
            _go.SetActive(true);
            _go.transform.position = new Vector2(backgroundSize, 0);
            obj.SetActive(false);
        } 
    }

    private void MoveLevel()
    {

        //test
        for (int i = 0; i < interactiveLevelObj.Count; i++)
        {
            if (interactiveLevelObj[i].objPrefab.activeInHierarchy)
                interactiveLevelObj[i].objPrefab.transform.position =
               new Vector2((interactiveLevelObj[i].objPrefab.transform.position.x + moveSpeed * moveLevelINTER), interactiveLevelObj[i].objPrefab.transform.position.y);
        }

        for (int i = 0; i < firstLevelObj.Count; i++)
        {
            if (firstLevelObj[i].objPrefab.activeInHierarchy)
                firstLevelObj[i].objPrefab.transform.position =
               new Vector2((firstLevelObj[i].objPrefab.transform.position.x + moveSpeed * moveLevelFIRST), firstLevelObj[i].objPrefab.transform.position.y);

            if (firstLevelObj[i].objType == ObjType.background)
                CreateBackground(firstLevelObj[i].objPrefab.gameObject);
        }
    }

    private float time = 0;
    [SerializeField] float timeForSpeed = 5;

    private void FixedUpdate()
      {
        if (!RunWriteController.Instance.playGame) return;

      /*  if (time < timeForSpeed)
            time += Time.deltaTime;
        else
            moveSpeed = moveSpeed + 1.3f;
            */
        MoveLevel();
      }

    [SerializeField] private Sprite[] backgroundTexture = new Sprite[3];
    public void SwapBackground(int nomber)
    {
        moveSpeed = moveSpeed * 1.5f;
        for (int i = 0; i < firstLevelObj.Count; i++)
        {
            if (firstLevelObj[i].objType == ObjType.background)
                firstLevelObj[i].objPrefab.gameObject.GetComponent<SpriteRenderer>().sprite = backgroundTexture[nomber];
        }
    }
}
