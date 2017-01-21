using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCameraController : MonoBehaviour
{
    public Image Cat;
    public Image PressEnterText;

    private bool finishedAnimatingCamera;
    private bool finishedFadingInElements;

    // Use this for initialization
    void Start ()
	{
        Debug.Log(Camera.main.orthographicSize);
        Camera.main.orthographicSize = 0F;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (finishedFadingInElements) return;

        // If camera is finished zooming out, fade in the other elements
        if (finishedAnimatingCamera)
        {
            var pressEnterColor = PressEnterText.color;
            if (PressEnterText.color.a < 0.99F)
            {
                PressEnterText.color = new Color(pressEnterColor.r, pressEnterColor.g, 
                                                 pressEnterColor.b, pressEnterColor.a + 0.01F);
            }
            else
            {
                finishedFadingInElements = true;
            }
        }
        // Otherwise zoom out camera
        else
        {
            if (Camera.main.orthographicSize < 4.94F)
            {
                Camera.main.orthographicSize += 0.05F;
            }
            else
            {
                Camera.main.orthographicSize = 5F;
                finishedAnimatingCamera = true;
            }
        }
    }
}
