using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepDistance : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 _offset;
    private Coroutine changePosition;

    void Start()
    {
        _offset = target.position - transform.position;
        changePosition = StartCoroutine(ChangePosition());  
    }

    public void YouCanComeToTargetZ ()
	{
        StopCoroutine(changePosition);
        StartCoroutine(GetCloserZ());
	}

    private IEnumerator ChangePosition()
	{
        float x,y,z;
		do
		{
            x = target.position.x - _offset.x;
            y = transform.position.y;
            z = target.position.z - _offset.z;

            transform.position = new Vector3(x,y,z);
            
            yield return new WaitForFixedUpdate();
		} while (true);
	}

    private IEnumerator GetCloserZ()
	{
        float x, y, z;
        do
        {

            if (Mathf.Abs(target.position.z - transform.position.z) < Mathf.Abs(_offset.z))
                _offset.z = target.position.z - transform.position.z;

            x = target.position.x - _offset.x;
            y = transform.position.y;
            z = target.position.z - _offset.z;
            transform.position = new Vector3(x, y, z);

            yield return new WaitForFixedUpdate();
        } while (true);
    }
}
