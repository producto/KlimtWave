using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag ("cat");	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (10, 0, 0));
	}
}
