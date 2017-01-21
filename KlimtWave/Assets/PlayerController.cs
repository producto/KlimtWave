using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		//GetComponent<Rigidbody2D> ().AddForce (new Vector2 (1000F, 0));
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.collider.gameObject.tag == "Wave Particle") {
			Debug.Log ("collision with wave");
			var waves = GameObject.FindGameObjectWithTag ("Wave Control");
			waves.SendMessage ("CollidePlayer");
		}
	}

    public void Death()
    {
        Debug.Log("Died");
        GetComponent<CircleCollider2D>().enabled = false;

        var particleSystem = transform.FindChild("Blood");
        Debug.Log(particleSystem != null);

        particleSystem.gameObject.SetActive(true);
    }
}
