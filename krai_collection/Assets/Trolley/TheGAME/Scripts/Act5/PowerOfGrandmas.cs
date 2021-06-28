using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOfGrandmas : MonoBehaviour
{
	public float power;
	private Rigidbody2D _rb2D;
	public float doNotFallPower;
	public float maxSpeed;

	private void Start()
	{
		_rb2D = gameObject.GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		_rb2D.AddForce(transform.rotation * Vector2.right * power * _rb2D.mass);
		_rb2D.AddForce(transform.rotation * Vector2.right * transform.rotation.z * power * _rb2D.mass * doNotFallPower); // Равновесие

		var velocityX = Mathf.Clamp(_rb2D.velocity.x , -maxSpeed, maxSpeed);
		_rb2D.velocity = new Vector2(velocityX, _rb2D.velocity.y);
	}

	public void PowerModule()
	{
		if (power<0) power = -1 * power;
	}


}
