using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    private GameObject player;
    private Animator obstacleAnimator;
    private ObstaclesController controller;

    private bool isEvil;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        obstacleAnimator = GetComponent<Animator>();

        controller = GameObject.FindObjectOfType<ObstaclesController>();
        Debug.Log("Obstacles Spawned: " + controller.ObstaclesSpawned);

        var chance = controller.ObstaclesSpawned/100f < 0.7f ? controller.ObstaclesSpawned/100f : 0.7f;
        if (UnityEngine.Random.Range(0f, 1f) < chance)
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
            collider.gameObject.SendMessage(isEvil ? "Death" : "IncreaseScore");
        }
    }
}
