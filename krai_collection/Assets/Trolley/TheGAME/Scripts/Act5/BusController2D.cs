using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusController2D : MonoBehaviour
{
    public float busSpeed;
	Rigidbody2D rb2D;

	public CircleCollider2D wheel;
	public Collider2D ground;


	private void Start()
	{
		rb2D = gameObject.GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (wheel.IsTouching(ground))
			rb2D.AddForce( gameObject.transform.rotation * Vector2.right * busSpeed * Input.GetAxis("Horizontal") * rb2D.mass);
	}
	


}

