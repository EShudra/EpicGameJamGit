using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//player speed
	public float speed = 10f;
	//jump power modifier
	public float jumpHeight = 1f;
	//player sprite
	public Sprite skin;
	//current and max hp
	public float currentHp;
	public float maximumHp;
	//max bomb quantity
	public int bombMaxCount;
	//bomb accuracy
	public float bombAccuracy;
	//number of bombs to instantiate per throw
	public int bombsPerThrow;
	//bomb accuracy angle
	public float bombAngle;

	[HideInInspector] public float jumpForce = 1000f;
	public Transform groundCheck;
	public float invulnerabilityTime = 2f;

	private bool doubleJumped;
	private float collisionTime;
	private bool invulnerable = false;
	private bool grounded = true;
	private bool isJumping = false;
	[HideInInspector] public bool facingRight = false;
	private Vector3 direction;
	private Rigidbody2D rb2D;

	//number of directions in which bombs are spawned
	public float[] bombLinesAngle;
	//bombs trajectory dispersion
	public float bombLinesDispersionAngle;
	//bullet prefab to spawn
	public GameObject bombPrefab;
	//bomb spawn point
	public Transform bombSpawnPoint;

	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
		invulnerable = false;
	}

	void Update () {
		CheckMove ();
		CheckJump ();
	}

	void FixedUpdate () {
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		Move ();
		Jump ();
		Bomb (); 

		if (Time.time >= (collisionTime + invulnerabilityTime)) {
			invulnerable = false;
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"),LayerMask.NameToLayer ("Enemy"),invulnerable);
		}
	}

	void CheckMove () {	
		direction = new Vector3 (0, 0, 0);

		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			direction += Vector3.right;
			facingRight = true;
		}

		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			direction += Vector3.left;
			facingRight = false;
		}
	}

	void CheckJump () {
		if (grounded)
			doubleJumped = false;

		if ((Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) && grounded) {
			isJumping = true;
		}

		if ((Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) && !grounded && !doubleJumped) {
			isJumping = true;
			doubleJumped = true;
		}
	}

	void Move () {
		if (direction != new Vector3 (0, 0, 0))

			transform.Translate (speed * direction * Time.deltaTime);

		if ((facingRight && transform.localScale.x <= 0) || (!facingRight && transform.localScale.x >= 0))
			transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);

	}
		
	void Jump () {
		if (isJumping) {
			rb2D.AddForce (new Vector2 (0f, jumpForce * jumpHeight));
			isJumping = false;
		}
	}

	void Bomb () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			Debug.Log ("Bomb has been thrown");
			Instantiate (bombPrefab,this.bombSpawnPoint.position, Quaternion.identity);
		}
	}

	void OnCollisionStay2D (Collision2D col) {
		Debug.Log ("Col");
		//if (col.collider.tag == "Enemy" && !invulnerable) {
		if (col.collider.tag == "Enemy") {
			Debug.Log ("Collision with an enemy.");
			collisionTime = Time.time;
			invulnerable = true;
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"),LayerMask.NameToLayer ("Enemy"),invulnerable);
			//Триггер для анимации мигания - здесь!

			if (col.transform.position.x < transform.position.x) {
				rb2D.AddForce(new Vector3 ((jumpForce * jumpHeight / 2f), (jumpForce * jumpHeight / 2f), 0f));
			} else {
				rb2D.AddForce(new Vector3 (-(jumpForce * jumpHeight / 2f), (jumpForce * jumpHeight / 2f), 0f));
			}
		}
	}
		

}
