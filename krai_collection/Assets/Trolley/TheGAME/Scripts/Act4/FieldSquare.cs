using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FieldSquare : MonoBehaviour
{
	private FieldSquareMove fieldSquareMove;

	private void Start()
	{
		fieldSquareMove = transform.parent.gameObject.GetComponent<FieldSquareMove>();

		if (fieldSquareMove == null)
			throw new InvalidOperationException();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Bus")
			fieldSquareMove.SetSquareAsCentar(this.gameObject);
	}
}
