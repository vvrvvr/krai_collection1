﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace krai_trol
{
	public class StopFollow : MonoBehaviour
	{
		private CameraAct5 mainCamera;

		private void Start()
		{
			mainCamera = FindObjectOfType<CameraAct5>();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == "Bus")
				mainCamera.StopFollow(collision.transform);
		}
	}
}
