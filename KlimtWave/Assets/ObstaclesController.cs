using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
	public Camera camera;
	public GameObject obstaclePrefab;
	public float avgAmountOnScreen;

	private readonly List<GameObject> obstacles = new List<GameObject> ();

    [HideInInspector]
	public int ObstaclesSpawned;

	void Start ()
	{
		ObstaclesSpawned = 0;
	}

	void FixedUpdate ()
	{
		RemovePastObstacles ();
		if (camera.velocity.x > 0) {
			PossiblySpawnNewObstacles ();
		}
	}

	void RemovePastObstacles ()
	{
		// If the obstacles is off the screen, remove it from the list
		// Once the first obstacles in the list is /on/ the screen, the rest will probably be too.
		while (obstacles.Count > 0 && camera.WorldToViewportPoint (obstacles [0].GetComponent<Renderer> ().bounds.max).x < 0) {
			Destroy (obstacles [0]);
			obstacles.RemoveAt (0);
		}
	}

	void PossiblySpawnNewObstacles ()
	{
		var needForNewObstacle = camera.transform.position.x / camera.pixelWidth - ObstaclesSpawned / avgAmountOnScreen;
		var deviation = Random.value - 0.5F;

//		Debug.Log ("need: " + needForNewObstacle
//		+ ", deviatedNeed = " + (needForNewObstacle + deviation)
//		+ ", pos/w = " + camera.transform.position.x / camera.pixelWidth
//		+ ", spawned/avg = " + ObstaclesSpawned / avgAmountOnScreen);

		if (Random.value < needForNewObstacle + deviation) {
			SpawnNewObstacle ();
		}
	}

	void SpawnNewObstacle ()
	{
		var newObstacle = GameObject.Instantiate (obstaclePrefab);
		var randomY = .1F + Random.value / .6F;  // Random between 0.1 and 0.7
		newObstacle.transform.position.Set (0, 0, 0);
		newObstacle.transform.position = camera.ViewportToWorldPoint (new Vector3 (1, randomY, 10));
		var newBody = newObstacle.GetComponent<Rigidbody2D> ();

		var maxDVelocity = new Vector2 (400F, 700F);
		var deviation = new Vector2 (-Random.value, Random.value);
		var dVelocity = Vector2.Scale (maxDVelocity, deviation);
		newBody.AddForce (newBody.mass * dVelocity / Time.fixedDeltaTime);

		obstacles.Add (newObstacle);
		ObstaclesSpawned++;
	}
}
