using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaveController : MonoBehaviour
{
	public GameObject Waves;
	public GameObject WaveColumnPrefab;
	private List<GameObject> waveColumns = new List<GameObject> ();

	private float mousePos;

	// Use this for initialization
	void Start ()
	{
		mousePos = 0F;
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

//		waveColumns [0].GetComponent<SpringJoint2D> ().connectedAnchor = new Vector2 (0, -1080 - 540 / 2);
//		waveColumns [0].GetComponent<SpringJoint2D> ().distance = 0;
	}

	void FixedUpdate ()
	{
		HandleInput ();
		UpdateWaves ();
	}

	private void UpdateWaves ()
	{
		for (int i = 0; i < waveColumns.Count; i++) {
			CalculateForceVector (i);
		}
	}


	private void CalculateForceVector(int i){
		var balancingConstant = 1000F;
		var dampingConstant = -100F;
		var balanceY = -1350F;
		var transmittanceConstant = 8000F;
		var controlConstant = 2000000F; 
		var previousPos = balanceY;

		if (i > 0) {
			previousPos = waveColumns [i - 1].GetComponent<Rigidbody2D> ().position.y;
			controlConstant = controlConstant/(i+1);
		} else {
			transmittanceConstant = 0;
		}
		var body = waveColumns [i].GetComponent<Rigidbody2D> ();

		body.AddForce (new Vector2 (0, (balanceY - body.position.y) * balancingConstant + 
			
			(previousPos - body.position.y) * transmittanceConstant + 
			body.velocity.y * dampingConstant +
			(mousePos)*controlConstant ));
	}

	private void HandleInput ()
	{
		
//		if (Input.GetKey (KeyCode.Space)) {
//			waveColumns [0].GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 100000F));
//		}


		mousePos = Input.GetAxis("Mouse Y");

	}
}
