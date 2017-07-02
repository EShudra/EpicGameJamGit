using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour, IWorldObject {

	public WorldController wCont;

	public AudioClip walking1;
	public AudioClip walking2;
	public AudioClip jumpSound;
	public AudioClip hitSound1;
	public AudioClip hitSound2;
	public AudioClip hitSound3;
	public AudioClip hitSound4;

	public bool doubleJumpAbility = true;

	public bool moving = false;
	//player speed
	public float speed = 10f;
	//jump power modifier
	public float jumpHeight = 1f;
	//player sprite
	public Sprite skin;
	//current and max hp
	public int currentHp = 2;
	public int maximumHp = 2;
	//max bomb quantity and current bomb amount
	public int bombMaxCount = 6;
	public int bombCurrentAmount = 6;
	//bomb accuracy
	public float bombAccuracy;
	//number of bombs to instantiate per throw
	public int bombsPerThrow;
	//bomb accuracy angle
	public float bombAngle;

	//all these are the fileds that regulate heart and grenades spawning on the UI
	public GameObject heartPrefab;
	private List<GameObject>[] heartsOnBoard;
	public List<GameObject> heartPlaceholders = new List<GameObject>();
	public Transform firstHeartPosition;
	public int maximumHeartsToSpawn = 10;
	public GameObject grenadePrefab;
	private List<GameObject>[] grenadesOnBoard;
	public List<GameObject> grenadePlaceholders = new List<GameObject>();
	public Transform firstGrenadePosition;
	public int maximumGrenadesToSpawn = 10;


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
	//throwable bomb prefab to spawn
	public GameObject bombThrowablePrefab;
	//bomb spawn point
	public Transform bombSpawnPoint;
	//animator
	private Animator anim;

	void Start () {
		wCont = GameObject.FindObjectOfType<WorldController> ();
		InitParameters ();
		currentHp = maximumHp;

		rb2D = GetComponent<Rigidbody2D> ();
		invulnerable = false;
		anim = GetComponent<Animator> ();

		/*if (bombMaxCount > maximumGrenadesToSpawn)
			bombMaxCount = maximumGrenadesToSpawn;
		if (maximumHp > maximumHeartsToSpawn)
			maximumHp = maximumHeartsToSpawn;*/

		//GenerateBombsAndHearts ();
	}

	/*
	void GenerateBombsAndHearts () {
		GameObject heartParent = GameObject.FindGameObjectWithTag ("HeartParent");
		heartsOnBoard = new List<GameObject>[maximumHp];

		for (int i = 0; i < heartsOnBoard.Length; i++) {
			object o = heartsOnBoard.GetValue (i);
			GameObject obj = o as GameObject;
			obj = Instantiate (heartPrefab, heartParent.transform) as GameObject;
			obj.transform.position = (heartPlaceholders[i] as GameObject).transform.position;
			obj.transform.localScale = heartPrefab.transform.localScale;
			obj.SetActive (true);
			Debug.Log ("Heart created.");
		}

		GameObject bombParent = GameObject.FindGameObjectWithTag ("GrenadeParent");
		grenadesOnBoard = new List<GameObject>[bombMaxCount];

		for (int i = 0; i < grenadesOnBoard.Length; i++) {
			object o = grenadesOnBoard.GetValue (i);
			GameObject obj = o as GameObject;
			obj = Instantiate (grenadePrefab, bombParent.transform) as GameObject;
			obj.transform.position = (grenadePlaceholders[i] as GameObject).transform.position;
			obj.transform.localScale = grenadePrefab.transform.localScale;
			obj.SetActive (true);
			Debug.Log ("Grenade created.");
		}
	}
	*/

	void Update () {
		CheckMove ();
		CheckJump ();
		TriggerMoveAnimation ();
	}

	void FixedUpdate () {
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		Move ();
		Jump ();
		Bomb (); 
		MuteMusic ();

		if (Time.time >= (collisionTime + invulnerabilityTime)) {
			invulnerable = false;
			anim.SetBool ("onHit",false);
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"),LayerMask.NameToLayer ("Enemy"),invulnerable);
		}
	}

	void MuteMusic () {
		if (Input.GetKeyDown (KeyCode.M))
			SoundManager.instance.musicSource.mute = !SoundManager.instance.musicSource.mute;
	}

	void CheckMove () {	
		direction = new Vector3 (0, 0, 0);

		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			direction += Vector3.right;
			facingRight = true;
			moving = true;
		} else if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			direction += Vector3.left;
			facingRight = false;
			moving = true;
		} else {
			moving = false;
		}
	}

	void Move () {
		if (direction != new Vector3 (0, 0, 0))

			transform.Translate (speed * direction * Time.deltaTime);

		if ((facingRight && transform.localScale.x <= 0) || (!facingRight && transform.localScale.x >= 0))
			transform.localScale = new Vector3 (transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);

	}

	void CheckJump () {
		if (grounded)
			doubleJumped = false;

		if (isJumping)
			moving = true;

		if ((Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) && grounded) {
			isJumping = true;
			moving = true;
		}

		else if ((Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) && !grounded && !doubleJumped && doubleJumpAbility) {
			isJumping = true;
			doubleJumped = true;
			moving = true;
		}
	}
		
	void Jump () {
		if (isJumping) {
			rb2D.Sleep();
			rb2D.WakeUp();
			rb2D.AddForce (new Vector2 (0f, jumpForce * jumpHeight));
			if (!doubleJumped)
				SoundManager.instance.PlaySingle (jumpSound);
			isJumping = false;
		}
	}

	void Bomb () {
		if (Input.GetKeyDown (KeyCode.Q) && bombCurrentAmount != 0) {
			Debug.Log ("Bomb has been thrown");
			if (bombCurrentAmount <= bombMaxCount && bombCurrentAmount > 0)
				bombCurrentAmount--;
			Instantiate (bombThrowablePrefab,this.bombSpawnPoint.position, Quaternion.identity);
		}
	}

	void TriggerWalkSounds () {
		if (moving && grounded) {
			SoundManager.instance.RandomizeSfx (walking1, walking2);
		}
			
	}

	void TriggerMoveAnimation () {
		anim.SetTrigger ("moving");
	}

	void OnCollisionStay2D (Collision2D col) {
		
		if (col.collider.tag == "Enemy") {

			SoundManager.instance.RandomizeSfx (hitSound1,hitSound2,hitSound3,hitSound4);

			if (currentHp <= maximumHp && currentHp > 0) {
				currentHp--;
				Debug.Log (currentHp);
				RemoveHeart ();
				if (currentHp == 0)
					Death ();
			}

			//Debug.Log ("Collision with an enemy.");
			collisionTime = Time.time;
			invulnerable = true;
			anim.SetBool ("onHit",true);
			Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"),LayerMask.NameToLayer ("Enemy"),invulnerable);
			//Триггер для анимации мигания - здесь!

			if (col.transform.position.x < transform.position.x) {
				rb2D.AddForce(new Vector3 ((jumpForce * jumpHeight / 2f), (jumpForce * jumpHeight / 2f), 0f));
			} else {
				rb2D.AddForce(new Vector3 (-(jumpForce * jumpHeight / 2f), (jumpForce * jumpHeight / 2f), 0f));
			}
		}
	}

	void RemoveHeart () {
		
		/*
		GameObject[] heartComponents = obj.GetComponentsInChildren<GameObject>();
		foreach (GameObject comp in heartComponents) {
			if (comp.tag == "FullHeart") {
				comp.SetActive (false);
				break;
			}
		}*/
	}

	void Death () {
		//death animation
		if (this.gameObject != null) {
			anim.SetBool ("dead", true);
			Destroy (this.gameObject);
		}
	}

	public void InitParameters(){
		doubleJumpAbility = wCont.playerDoubleJump;
		maximumHp += wCont.playerHpIncrement;
		if (maximumHp <= 0) {
			maximumHp = 1;
		}
		wCont.playerHpIncrement = 0;
		jumpHeight = wCont.playerJumpHeight;
		speed = wCont.playerSpeed;

		bombMaxCount = wCont.playerGrenadesCountMax;
		if (wCont.playerGrenadesCount <= bombMaxCount) {
			bombCurrentAmount = wCont.playerGrenadesCount;
		} else {
			bombCurrentAmount = bombMaxCount;
		}
	}

}