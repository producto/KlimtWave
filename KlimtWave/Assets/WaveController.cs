﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
	public GameObject Waves;
	public GameObject WaveColumnPrefab;
	private List<GameObject> waveColumns = new List<GameObject> ();

    public GameObject Cat;
    private Animator catAnimator;
    private Rigidbody2D catRigidbody2D;

    // Use this for initialization
    void Start ()
    {
        catAnimator = Cat.GetComponent<Animator>();
        catRigidbody2D = Cat.GetComponent<Rigidbody2D>();

		var startPosition = Waves.transform.position;
		startPosition.x -= 960F;
		startPosition.y -= 1350F;
		var startRotation = Waves.transform.rotation;

		for (int i = 0; i < 15; i++) {
			var newWave = GameObject.Instantiate (WaveColumnPrefab, startPosition, startRotation);
//			if (i != 0) {
//				newWave.GetComponent<SpringJoint2D> ().connectedBody = waveColumns [i - 1].GetComponent<Rigidbody2D> ();
//			}
			waveColumns.Add (newWave);

			startPosition.x += 128F;
		}

		waveColumns [0].GetComponent<SpringJoint2D> ().connectedAnchor = new Vector2 (0, -1080 - 540 / 2);
		waveColumns [0].GetComponent<SpringJoint2D> ().distance = 500;
	}

	void FixedUpdate ()
	{
		HandleInput ();
		UpdateWaves ();

        //Debug.Log("Cat velocity: " + catRigidbody2D.velocity);
        catAnimator.SetInteger("CatAnimationState", catRigidbody2D.velocity.y > -200 ? 0 : 1);
    }

	private void UpdateWaves ()
	{
		for (int i = waveColumns.Count - 1; i > 0; i--) {
			waveColumns[i].transform.position = new Vector3(waveColumns[i].transform.position.x,
                                                            waveColumns[i - 1].transform.position.y,
                                                            waveColumns[i].transform.position.z);
		}
	}

	private void HandleInput ()
	{
	    if (Input.GetKey (KeyCode.Space)) {
			waveColumns [0].GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 100000F));
		}

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Quitting game. Buh-BYE!!");
            Application.Quit();
	    }
	}
}
