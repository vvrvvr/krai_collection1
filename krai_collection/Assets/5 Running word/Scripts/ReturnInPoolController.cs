using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class ReturnInPoolController : MonoBehaviour
{
    private float destroyPos = 12;

    private void OnEnable()
    {
        destroyPos = InstantiateLevel.Instance.spawnPositionX * -1;
        StartCoroutine(ControlUpdateDP());
    }

    private void FixedUpdate()
    {
            if (destroyPos < 0 && gameObject.transform.position.x < destroyPos)
                gameObject.SetActive(false);
            else
                if (destroyPos > 0 && gameObject.transform.position.x > destroyPos)
                    gameObject.SetActive(false);
    }

    private IEnumerator ControlUpdateDP()
    {
        yield return new WaitForSeconds(1);
        destroyPos = InstantiateLevel.Instance.spawnPositionX * -1;
        StartCoroutine(ControlUpdateDP());
    }
}
