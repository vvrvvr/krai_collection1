using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
	[SerializeField] private GoToAct7 _goToAct7;
	private void OnTriggerEnter(Collider other)
	{
		_goToAct7.PlayerExit();
	}
}
