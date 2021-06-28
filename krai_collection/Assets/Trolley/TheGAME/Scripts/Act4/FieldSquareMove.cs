using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSquareMove : MonoBehaviour
{
	private GameObject[,] _fieldSquares;

	private int _fieldSide;
	private int _squareSide;
	private Vector2Int _matrixCenter;
	private Vector3 _fieldCenter;
	private readonly int _planeSide = 20;
	private readonly Vector3 _startOffset = new Vector3(-10,0,-10);

	private void Start()
	{
		SetupField();
	}

	public void SetSquareAsCentar (GameObject square)
	{
		var positionInMatrix = FindSquarePositionInMatrix(square);

		if (positionInMatrix != _matrixCenter)
			ShiftField(_matrixCenter - positionInMatrix);
	}

	private void SetupField ()
	{
		var squares = GetComponentsInChildren<FieldSquare>();
		_fieldSide = (int)Mathf.Sqrt(squares.Length);
		_squareSide = (int)squares[0].transform.localScale.x * _planeSide;
		_matrixCenter = Vector2Int.one *( _fieldSide / 2);


		var offset = new Vector3(-1, 0, 1) * (_fieldSide - 1) * _squareSide / 2 + _startOffset;
		int count = 0;

		_fieldSquares = new GameObject[_fieldSide, _fieldSide];
		for (int z = 0; z < _fieldSide; z++)
			for (int x = 0; x < _fieldSide; x++)
			{
				_fieldSquares[z,x] = squares[count].gameObject;
				_fieldSquares[z,x].transform.position = offset + new Vector3(x, 0, -z) * _squareSide;
				count++;
			}
		

		_fieldCenter = _fieldSquares[_matrixCenter.y,_matrixCenter.x].transform.position + _startOffset;
	}

	private Vector2Int FindSquarePositionInMatrix(GameObject square)
	{
		for (int z = 0; z < _fieldSide; z++)
			for (int x = 0; x < _fieldSide; x++)
				if (_fieldSquares[z,x] == square) return new Vector2Int(x, z);

		Debug.Log("Can't find square in fieldSquares");
		return Vector2Int.zero;
	}

	private void ShiftField(Vector2Int shift)
	{
		GameObject saveSquare;

		if (shift.y == 1)
		{
			for (int x = 0; x < _fieldSide; x++)
			{
				saveSquare = _fieldSquares[_fieldSide - 1, x];

				saveSquare.transform.position += Vector3.forward * _fieldSide * _squareSide; // Двиггаем плошадку

				for (int z = _fieldSide - 1; z > 0; z--)
					_fieldSquares[z, x] = _fieldSquares[z - 1, x];

				_fieldSquares[0, x] = saveSquare;
			}
		}
		else if (shift.y == -1)
		{
			for (int x = 0; x < _fieldSide; x++)
			{
				saveSquare = _fieldSquares[0, x];

				saveSquare.transform.position += Vector3.back * _fieldSide * _squareSide;

				for (int z = 0; z < _fieldSide - 1; z++)
					_fieldSquares[z, x] = _fieldSquares[z + 1, x];

				_fieldSquares[_fieldSide - 1, x] = saveSquare;
			}
		}

		if (shift.x == 1)
		{
			for (int z = 0; z < _fieldSide; z++)
			{
				saveSquare = _fieldSquares[z, _fieldSide - 1];

				saveSquare.transform.position += Vector3.left * _fieldSide * _squareSide;

				for (int x = _fieldSide - 1; x > 0; x--)
					_fieldSquares[z, x] = _fieldSquares[z, x - 1];

				_fieldSquares[z, 0] = saveSquare;
			}
		}
		else if (shift.x == -1)
		{
			for (int z = 0; z < _fieldSide; z++)
			{
				saveSquare = _fieldSquares[z, 0];

				saveSquare.transform.position += Vector3.right * _fieldSide * _squareSide;

				for (int x = 0; x < _fieldSide - 1; x++)
					_fieldSquares[z, x] = _fieldSquares[z, x +1];

				_fieldSquares[z, _fieldSide - 1] = saveSquare;
			}
		}

		_fieldCenter = _fieldSquares[_matrixCenter.y, _matrixCenter.x].transform.position + _startOffset;
	}

	private GameObject[][] Clone (GameObject[][] from)
	{
		GameObject[][] to = new GameObject[from.Length][];		
		for (int i = 0; i < from.Length; i++)
		{
			for (int j = 0; j < from[j].Length; j++)
			{

			}
		}

		return to;
	}
}
