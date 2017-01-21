using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("WaveTest");
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Quitting game. Buh-BYE!!");
            Application.Quit();
        }
    }
}
