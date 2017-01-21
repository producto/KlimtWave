using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
	public Camera camera;
	public GameObject obstaclePrefab;
	public float avgAmountOnScreen;

	private readonly List<GameObject> obstacles = new List<GameObject> ();

	private int obstaclesSpawned;

	void Start ()
	{
		obstaclesSpawned = 0;
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
			obstacles.Remove (obstacles [0]);
		}
	}

	void PossiblySpawnNewObstacles ()
	{
		var needForNewObstacle = camera.transform.position.x / camera.pixelWidth - obstaclesSpawned / avgAmountOnScreen;
		Debug.Log ("need: " + needForNewObstacle + ", pos/w = " + camera.transform.position.x / camera.pixelWidth + ", spawned/avg = " + obstaclesSpawned / avgAmountOnScreen);
		if (Random.value < needForNewObstacle) {
			SpawnNewObstacle ();
		}
	}

	void SpawnNewObstacle ()
	{
		var newObstacle = GameObject.Instantiate (obstaclePrefab);
		var randomY = Random.value;
		newObstacle.transform.position.Set (0, 0, 0);
		newObstacle.transform.position = camera.ViewportToWorldPoint (new Vector3 (1, randomY, 10));
		obstacles.Add (newObstacle);
		obstaclesSpawned++;
	}
}
