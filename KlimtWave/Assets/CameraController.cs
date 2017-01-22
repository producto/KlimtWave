
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	
	public GameObject cat;
	private Camera cam;
	public GameObject block;

	// Use this for initialization
	void Start ()
	{
		cam = this.GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		block.transform.position = new Vector3 (transform.position.x - 960, 0, 0);

		if (this.transform.position.x < cat.transform.position.x) {

			transform.Translate (new Vector3 ((cat.transform.position.x - this.transform.position.x), 0F));
		}
	}
}
