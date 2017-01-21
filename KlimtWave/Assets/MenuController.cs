using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private IntroCameraController cameraController;

	// Use this for initialization
	void Start ()
	{
	    cameraController = GameObject.FindObjectOfType<IntroCameraController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (!cameraController.FinishedAnimatingCamera) return;

        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter) 
            || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
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
