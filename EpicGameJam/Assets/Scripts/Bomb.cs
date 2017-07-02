using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

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
		birthTime = Time.time;
		rb2D = GetComponent<Rigidbody2D> ();
		playerFacingRight = FindObjectOfType<Player>().facingRight;
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
}
