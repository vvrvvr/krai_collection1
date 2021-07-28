using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCS : MonoBehaviour {

	public  float CloudsSpeed;
		
	void Update () {
		transform.Rotate(0,Time.deltaTime*CloudsSpeed ,0); 
	}
}
