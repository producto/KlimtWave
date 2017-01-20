using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public GameObject Waves;
    public GameObject WaveColumnPrefab;
    private List<GameObject> waveColumns = new List<GameObject>();

	// Use this for initialization
	void Start ()
	{
        var startPosition = Waves.transform.position;
        startPosition.x -= 960F;
        startPosition.y -= 540F;
        var startRotation = Waves.transform.rotation;

        for (int i = 0; i < 15; i++)
	    {
	        waveColumns.Add(GameObject.Instantiate(WaveColumnPrefab, startPosition, startRotation));
	        startPosition.x += 128F;
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
