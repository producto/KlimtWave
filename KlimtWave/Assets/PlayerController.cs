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

    public void Death()
    {
        Debug.Log("Died");
        GetComponent<CircleCollider2D>().enabled = false;

        var particleSystem = transform.FindChild("Blood");
        Debug.Log(particleSystem != null);

        particleSystem.gameObject.SetActive(true);
    }
}
