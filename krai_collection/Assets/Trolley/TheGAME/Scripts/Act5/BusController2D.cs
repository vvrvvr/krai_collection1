﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusController2D : MonoBehaviour
{
    public float busSpeed;
	Rigidbody2D rb2D;

	public CircleCollider2D wheel;
	public Collider2D ground;
	private float inputTotal;
	[HideInInspector] public bool HasControl = true;



	private void Start()
	{
		rb2D = gameObject.GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (HasControl)
			MoveBus();
	}
	

	private void MoveBus()
    {
		var inputHorizontal = Input.GetAxis("Horizontal");
		var inputVertical = Input.GetAxis("Vertical");
		inputTotal = inputHorizontal;
		if (inputHorizontal == 0)
		{
			inputTotal = inputVertical;
		}
		if (wheel.IsTouching(ground))
			rb2D.AddForce(gameObject.transform.rotation * Vector2.right * busSpeed * inputTotal * rb2D.mass);
	}
}

