using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmasRotat : MonoBehaviour
{
	public BoxCollider2D boxCollider;
	public SpriteRenderer render;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "GrandmasRP")
		{
			render.flipX = false;
			boxCollider.enabled = true;
			GetComponent<PowerOfGrandmas>().PowerModule();
		}
	}
}
