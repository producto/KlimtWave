

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	
	public GameObject cat;
	public GameObject block;
	public float dist_cat_cam;

	// Use this for initialization
	void Start ()
	{
		Screen.SetResolution (1920, 1080, true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (this.transform.position.x - cat.transform.position.x < dist_cat_cam) {
			transform.Translate (new Vector3 (cat.transform.position.x - this.transform.position.x + dist_cat_cam, 0F));
		}
		block.transform.position = new Vector3 (transform.position.x - 960, 0, 0);
	}
}

