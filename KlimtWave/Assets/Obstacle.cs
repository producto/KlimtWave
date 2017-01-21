using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    private GameObject player;
    private Animator obstacleAnimator;

    private bool isEvil;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        obstacleAnimator = GetComponent<Animator>();

        if (UnityEngine.Random.Range(0f, 1f) > 0.8f)
        {
            isEvil = true;
            var sprite = GetComponent<SpriteRenderer>();
            var color = new Color(1f, 0.42f, 0.42f);

            sprite.color = color;
        }
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
            collider.gameObject.SendMessage("IncreaseScore");
            if (isEvil) collider.gameObject.SendMessage("Death");
        }
        /*if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.SendMessage("Death");
        }*/
    }
}
