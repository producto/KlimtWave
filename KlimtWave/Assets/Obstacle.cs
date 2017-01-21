using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    private GameObject player;
    private Animator obstacleAnimator;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        obstacleAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("hit player");
            obstacleAnimator.SetInteger("FishAnimationState", 1);
        }
        /*if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.SendMessage("Death");
        }*/
    }
}
