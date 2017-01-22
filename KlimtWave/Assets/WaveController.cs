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

	private float mouseVelocity;
	private float spaceDown;

	public GameObject Cat;
	private Animator catAnimator;
	private Rigidbody2D catRigidbody2D;

	// Use this for initialization
	void Start ()
	{
		waveColumnWidth = WaveColumnPrefab.GetComponent<Renderer> ().bounds.size.x;
		mouseVelocity = 0F;

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
			newWave.GetComponent<SpringJoint2D> ().enabled = false;
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

		float balanceY = Mathf.Max(100, Mathf.Min(Input.mousePosition.y, 800)) - 1620F;
		float balancingConstant;
		float dampingConstant;
		float transmittanceConstant;
		float controlConstant;
		float previousPos;

		if (i > 0) {
			balancingConstant = 0.01F;
			dampingConstant = 0;
			transmittanceConstant = 0.5F;
			controlConstant = 0;
			previousPos = waveColumns [i - 1].GetComponent<Rigidbody2D> ().position.y;
		} else {
			balancingConstant = 1F;
			dampingConstant = -0.05F;
			transmittanceConstant = 0;
			controlConstant = 10000F; 
			previousPos = balanceY;
		}

		var body = waveColumns [i].GetComponent<Rigidbody2D> ();

		var balancingVelocity = (balanceY - body.position.y) * balancingConstant / Time.fixedDeltaTime;
		var transmittanceVelocity = (previousPos - body.position.y) * transmittanceConstant / Time.fixedDeltaTime;
		var dampingVelocity = body.velocity.y * dampingConstant;
		var naturalVelocity = balancingVelocity + transmittanceVelocity + dampingVelocity;


		body.AddForce (new Vector2 (0, body.mass * (naturalVelocity - body.velocity.y) / Time.fixedDeltaTime));
	}

	private void HandleInput ()
	{
//		if (Input.GetKey (KeyCode.Space)) {
//			spaceDown = 1F;
//		} else {
//			spaceDown = 0f;
//		}
//
//		mouseVelocity = Input.GetAxis ("Mouse Y");

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
			
		var slopeForce = -55000;

		Cat.GetComponent<Rigidbody2D> ().AddForce (new Vector2((diff * slopeForce), 0));

	}
}
