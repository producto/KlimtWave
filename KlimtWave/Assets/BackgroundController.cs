using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundLayer
{

	private readonly GameObject[] images;
	private readonly Camera camera;
	private readonly float speed;

	private int currentindex;
	private float imageWidth;


	public BackgroundLayer (float speed, Camera camera, GameObject image)
	{
		this.speed = speed;
		this.camera = camera;
		this.imageWidth = image.GetComponent<SpriteRenderer> ().bounds.size.x;
		this.currentindex = 0;

		this.images = new GameObject[2];
		this.images [0] = image;
		this.images [1] = GameObject.Instantiate (image);


		this.images [1].transform.Translate (new Vector3 (this.imageWidth, 0));
	}

	public void update ()
	{

		for (int i = 0; i < 2; i++) {
			images [i].transform.Translate (speed * camera.velocity * Time.fixedDeltaTime);
		}


		if (camera.transform.position.x > images [1 - currentindex].transform.position.x) {
			shuffle ();
		}

	}

	public void shuffle ()
	{

		var otherIndex = 1 - this.currentindex;

		this.images [currentindex].transform.Translate (new Vector3 (2 * this.imageWidth, 0, 0));

		this.currentindex = otherIndex;
	}

}

public class AmbientLayer
{

	private readonly GameObject[] images;
	private readonly Camera camera;
	private readonly float speed;

	private int rearMostIndex;
	private float spacing;
	private float randomspace;

	public AmbientLayer (float speed, Camera camera, string objectTag, float spacing, float randomness)
	{
		this.speed = speed;
		this.camera = camera;
		this.rearMostIndex = 0;
		this.spacing = spacing;

		this.images = GameObject.FindGameObjectsWithTag (objectTag);


		this.randomspace = randomness;

		//initial spacing

		for (int i = 0; i < images.Length; i++) {
			this.images [i].transform.Translate (new Vector2 (camera.transform.position.x - camera.orthographicSize + i * spacing, 0));
		}

	}
		
	public void update ()
	{

	
		//move everything by the speed
		for (int i = 0; i < images.Length; i++) {
			images [i].transform.Translate (speed * camera.velocity * Time.fixedDeltaTime);
		}


		if (camera.WorldToScreenPoint(images[this.rearMostIndex].transform.position).x < -spacing) {
			placeNext ();
		}

	}

	public void placeNext(){

		var foremostIndex = (rearMostIndex - 1) % images.Length;
		var rear = images [rearMostIndex];
		var fore = images [foremostIndex];
		var advance = spacing * (1 + randomspace * Random.value);

		rear.transform.position.Set (fore.transform.position.x + advance, rear.transform.position.y , 0);
		//rear.transform.localScale.Set (1, 1, 1);

		this.rearMostIndex++;

	}

}

public class BackgroundController : MonoBehaviour
{

	public Camera camera;
	public GameObject clouds;
	public GameObject trees;
	public GameObject land;
	public GameObject mountain;

	private BackgroundLayer cloudLayer;
	private BackgroundLayer treeslayer;
	private BackgroundLayer grasslayer;
	private AmbientLayer mountainLayer;

	// Use this for initialization
	void Start ()
	{

		//lower is "closer"
		this.cloudLayer = new BackgroundLayer (0.5F, camera, clouds);
		this.treeslayer = new BackgroundLayer (0.1F, camera, trees);
		this.grasslayer = new BackgroundLayer (0.1F, camera, land);

		this.mountainLayer = new AmbientLayer (0.05F, camera, "mountain", 700f, 0.5f);
	}

	// Update is called once per frame
	void Update ()
	{
		//update layer transforms
		cloudLayer.update ();
		treeslayer.update ();
		grasslayer.update ();
		mountainLayer.update ();

		//spawn new images

	}
}
