using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour, IWorldObject {

	public WorldController wCont;

	public Vector3 throwForce;

	public GameObject explosionPrefab;
	public Player player;
	//bomb skin
	public Sprite skin;
	//bomb explosion radius
	public float explosionRadius;
	//power of the throw
	public float throwPower;
	//bomb damage
	public float damage;
	//birth time of the bomb
	private float birthTime;
	//life time
	public float lifeTime = 10f;
	//list of all damagable objects
	public IDestroyableObject destroyableList;
	//player
	public bool playerFacingRight;

	private Rigidbody2D rb2D;

	void Start () {
		player = FindObjectOfType<Player> () as Player;
		foreach (var item in GameObject.FindObjectsOfType<Player>()) {
			if (item.gameObject.active == true) {
				player = item;
			}
		}
		wCont = GameObject.FindObjectOfType<WorldController> ();
		InitParameters ();
		birthTime = Time.time;
		rb2D = GetComponent<Rigidbody2D> ();
		playerFacingRight = player.facingRight;
		Debug.Log (transform.lossyScale.x / Mathf.Abs (transform.lossyScale.x));

		playerFacingRight = player.facingRight;

		if (playerFacingRight)
			rb2D.AddForce (new Vector3(350f,350f,0)*throwPower);
		else
			rb2D.AddForce (new Vector3(-350f,350f,0)*throwPower);
		
	}

	void FixedUpdate () {
		if (Time.time - birthTime > lifeTime) {
			Explode ();
		}

		rb2D.AddTorque (2f);
	}

	void OnCollisionEnter2D (Collision2D col) {
		if ((col.gameObject.tag == "Wall")||(col.gameObject.tag == "Enemy") || (col.gameObject.tag == "Ground")) {
			Explode ();
		}
	}

	void Explode () {
		this.enabled = false;
		Instantiate (explosionPrefab, this.transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}

	public void InitParameters (){
	}
}
