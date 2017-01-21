using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundLayer {

	private GameObject[] images;
	private Camera camera;
	private float speed;
	private int currentindex; 
	private float imageWidth;


	public BackgroundLayer(float speed, Camera camera, GameObject image){
		this.speed = speed;
		this.camera = camera;
		this.imageWidth = image.GetComponent<SpriteRenderer> ().bounds.size.x;
		this.currentindex = 0;

		this.images = new GameObject[2];
		this.images [0] = image;
		this.images [1] = GameObject.Instantiate (image);


		this.images [1].transform.Translate(new Vector3(this.imageWidth, 0)); 
	}

	public void update(){

		for (int i = 0; i < 2; i++) {
			images [i].transform.position.Set(camera.transform.position.x * speed, 0, 0);
		}


		if (camera.transform.position.x > images [1-currentindex].transform.position.x) {
			shuffle ();
		}
		
	}

	public void shuffle(){

		var otherIndex = 1 - this.currentindex;

		this.images [currentindex].transform.Translate (new Vector3 (2*this.imageWidth, 0, 0));

		this.currentindex = otherIndex;
	}

}

public class BackgroundController : MonoBehaviour {

	public Camera camera;
	public GameObject clouds; 
	public GameObject trees;
	public GameObject land; 
	public GameObject mountain;

	private BackgroundLayer cloudLayer;
	private BackgroundLayer treeslayer;
	private BackgroundLayer grasslayer;
	private BackgroundLayer mountainLayer;

	// Use this for initialization
	void Start () {

		//lower is "closer"
		this.cloudLayer = new BackgroundLayer (0.01F, camera, clouds);
		this.treeslayer = new BackgroundLayer (0.1F, camera, trees);
		this.grasslayer = new BackgroundLayer (0.1F, camera, land);
		this.mountainLayer = new BackgroundLayer (0.05F, camera, mountain);
	}
	
	// Update is called once per frame
	void Update () {
		//update layer transforms
		cloudLayer.update();
		treeslayer.update();
		grasslayer.update();

		//spawn new images

	}
}
