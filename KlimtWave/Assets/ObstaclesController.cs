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
		var randomY = Random.value;
		newObstacle.transform.position.Set (0, 0, 0);
		newObstacle.transform.position = camera.ViewportToWorldPoint (new Vector3 (1, randomY, 10));
		obstacles.Add (newObstacle);
		ObstaclesSpawned++;
	}
}
