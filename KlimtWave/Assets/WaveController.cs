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
        startPosition.y -= 1350F;
        var startRotation = Waves.transform.rotation;

        for (int i = 0; i < 15; i++)
        {
            var newWave = GameObject.Instantiate(WaveColumnPrefab, startPosition, startRotation);
            //if (i == 0) newWave.GetComponent<Rigidbody2D>().isKinematic = true;
            if (i != 0)
            {
                newWave.GetComponent<SpringJoint2D>().connectedBody = waveColumns[i - 1].GetComponent<Rigidbody2D>();
            }
            waveColumns.Add(newWave);

	        startPosition.x += 128F;
	    }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // Animate...EVERYTHING
        /*foreach (var waveColumn in waveColumns)
	    {
	        
	    }*/

        HandleInput();
    }



    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            waveColumns[0].GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000F));
        }
    }
}
