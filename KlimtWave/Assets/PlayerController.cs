
﻿using System;
﻿using System.Collections;
using System.Collections.Generic;
﻿using System.Linq;
﻿using UnityEngine;
﻿using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text ScoreText;
    private Animation scoreAnimation;

    private AudioSource[] audioSources;
    private Rigidbody2D rigidBody;

    private AudioSource meowAudio;
    private AudioSource meowDistressAudio;
    private AudioSource bigSplashAudio;
    private AudioSource waveAudio;
    private AudioSource fishCollect;

    private int score;

	// Use this for initialization
	void Start ()
    {
        audioSources = GameObject.FindObjectsOfType<AudioSource>();
	    rigidBody = gameObject.GetComponent<Rigidbody2D>();

        meowAudio = audioSources.First(a => a.name == "Meow");
        meowDistressAudio = audioSources.First(a => a.name == "Dying");
        bigSplashAudio = audioSources.First(a => a.name == "BigSplash");
        waveAudio = audioSources.First(a => a.name == "Wave");
        fishCollect = audioSources.First(a => a.name == "FishCollect");

        score = 0;
	    scoreAnimation = ScoreText.GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		//GetComponent<Rigidbody2D> ().AddForce (new Vector2 (1000F, 0));
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.collider.gameObject.tag == "Wave Particle") {
			var waves = GameObject.FindGameObjectWithTag ("Wave Control");
			waves.SendMessage ("CollidePlayer",collision.collider.gameObject);

            //Debug.Log("Player velocity:" + rigidBody.velocity);
		    if (Math.Abs(rigidBody.velocity.y) > rigidBody.velocity.x)
		    {
		        bigSplashAudio.Play(0);
		    }
		    else
		    {
                if (waveAudio.isPlaying) return;

                //bool shouldPlaySound = UnityEngine.Random.Range(0f, 1f) > 0.7f;
                waveAudio.Play();
            }

            bool shouldMeow = UnityEngine.Random.Range(0f, 1f) > 0.3f;
		    if (shouldMeow)
		    {
		        if (UnityEngine.Random.Range(0f, 1f) > 0.5f) meowDistressAudio.Play();
		        else meowAudio.Play();
            }
        }
	}

    public void Death()
    {
        Debug.Log("Died");
        GetComponent<CircleCollider2D>().enabled = false;

        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;

        Camera.main.orthographicSize = 400;
    }

    public void IncreaseScore()
    {
        Debug.Log("IncreaseScore");
        fishCollect.Play();
        scoreAnimation.Play();
        score += 1;
        ScoreText.text = score.ToString();
    }
}
