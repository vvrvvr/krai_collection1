using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAct5 : MonoBehaviour
{
	private Vector3 _offset;
	private Transform _target;
	private Coroutine _followCoroutine;
	
	public float speed;

	public void StartFollow (Transform target)
	{
		if (_followCoroutine == null)
		{
			_target = target;
			_offset = transform.position - _target.position;
			_followCoroutine = StartCoroutine(Follow());
		}
	}

	private IEnumerator Follow()
	{
		do
		{
			transform.position = Vector3.Lerp(transform.position, _target.position + _offset, speed * Time.fixedDeltaTime);
			yield return new WaitForFixedUpdate();
		} while (true);
	}

	public void  StopFollow (Transform target)
	{
		if (_followCoroutine!=null)
		{
			StopCoroutine(_followCoroutine);
			_followCoroutine = null;
		}
	}

}
