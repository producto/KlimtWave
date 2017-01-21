using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
	public GameObject Waves;
	public GameObject WaveColumnPrefab;
	public Camera camera;

	private List<GameObject> waveColumns = new List<GameObject> ();
	private float waveColumnWidth;

	private float mousePos;

	public GameObject Cat;
	private Animator catAnimator;
	private Rigidbody2D catRigidbody2D;

	// Use this for initialization
	void Start ()
	{
		waveColumnWidth = WaveColumnPrefab.GetComponent<Renderer> ().bounds.size.x;
		mousePos = 0F;

		catAnimator = Cat.GetComponent<Animator> ();
		catRigidbody2D = Cat.GetComponent<Rigidbody2D> ();

		var startPosition = Waves.transform.position;
		startPosition.x -= 960F;
		startPosition.y -= 1350F;
		var startRotation = Waves.transform.rotation;

		for (int i = 0; i < Mathf.Ceil (camera.pixelWidth / waveColumnWidth) + 1; i++) {
			var newWave = GameObject.Instantiate (WaveColumnPrefab, startPosition, startRotation);

//			if (i != 0) {
//				newWave.GetComponent<SpringJoint2D> ().connectedBody = waveColumns [i - 1].GetComponent<Rigidbody2D> ();
//			}
			waveColumns.Add (newWave);

			startPosition.x += waveColumnWidth;
		}

//		waveColumns [0].GetComponent<SpringJoint2D> ().connectedAnchor = new Vector2 (0, -1080 - 540 / 2);
//		waveColumns [0].GetComponent<SpringJoint2D> ().distance = 0;
	}

	void FixedUpdate ()
	{
		HandleInput ();
		UpdateWaves ();

		//Debug.Log("Cat velocity: " + catRigidbody2D.velocity);
		catAnimator.SetInteger ("CatAnimationState", catRigidbody2D.velocity.y > -200 ? 0 : 1);
	}

	private void UpdateWaves ()
	{
		// If we are past the last wave, generate a new one
		if (camera.WorldToScreenPoint (waveColumns [0].GetComponent<Renderer> ().bounds.max).x < 0) {
			var first = waveColumns [0];
			for (int i = 0; i < waveColumns.Count - 1; i++) {
				waveColumns [i] = waveColumns [i + 1];
			}
			waveColumns [waveColumns.Count - 1] = first;
			waveColumns [waveColumns.Count - 1].transform.position = waveColumns [waveColumns.Count - 2].transform.position;
			waveColumns [waveColumns.Count - 1].transform.Translate (new Vector3 (waveColumnWidth, 0, 0));
		}

		// Update all columns
		for (int i = 0; i < waveColumns.Count; i++) {
			CalculateForceVector (i);
		}
	}


	private void CalculateForceVector (int i)
	{
		var balancingConstant = 1200F;
		var dampingConstant = -1900F;
		var balanceY = -1350F;
		var transmittanceConstant = 30000F;
		var controlConstant = 4000000F; 
		var previousPos = balanceY;

		if (i > 0) {
			previousPos = waveColumns [i - 1].GetComponent<Rigidbody2D> ().position.y;
			controlConstant = 0;
		} else {
			transmittanceConstant = 0;
		}
		var body = waveColumns [i].GetComponent<Rigidbody2D> ();
		body.AddForce (new Vector2 (0, (balanceY - body.position.y) * balancingConstant +
			
		(previousPos - body.position.y) * transmittanceConstant +
		body.velocity.y * dampingConstant +
		(mousePos) * controlConstant));
	}

	private void HandleInput ()
	{

//		if (Input.GetKey (KeyCode.Space)) {
//			waveColumns [0].GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 100000F));
//		}


		mousePos = Input.GetAxis ("Mouse Y");


		if (Input.GetKeyUp (KeyCode.Escape)) {
			Debug.Log ("Quitting game. Buh-BYE!!");
			Application.Quit ();
		}
	}

	private void CollidePlayer(GameObject collidedWave){
		
		var cindex = waveColumns.FindIndex (item => item.GetInstanceID() == collidedWave.GetInstanceID());

		var left = waveColumns[Mathf.Max(cindex-1,0)].transform.position.y;
		var mid = waveColumns[cindex].transform.position.y; 
		var right = waveColumns[Mathf.Min(cindex+1,waveColumns.Count-1)].transform.position.y; 

		var diff = (right - mid) + (mid - left); // positive is upslope
			
		var slopeForce = -35000;

		Cat.GetComponent<Rigidbody2D> ().AddForce (new Vector2((diff * slopeForce), 0));

		Debug.Log ("Player Collide with index:"+cindex);
	}
}
