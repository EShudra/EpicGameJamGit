using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//Скорость персонажа
	public float speed = 10f;
	//Используется как множитель к силе прыжка. ГД нужно дать доступ только к этой переменной.
	public float jumpHeight = 1f;
	//Просто спрайт персонажа
	public Sprite skin;
	//Текущее хп/максимальное хп, всё просто
	public float currentHp;
	public float maximumHp;
	//Максимальное количество бомб
	public int bombMaxCount;
	//Разброс бомб
	public float bombAccuracy;
	//Количество бомб за бросок
	public int bombsPerThrow;
	//Угол отклонения бомб
	public float bombAnlge;

	[HideInInspector] public float jumpForce = 1000f;
	public Transform groundCheck;
	public float gravityScale = 7f;
	public float invulnerabilityTime = 2f;

	private bool doubleJumped;
	private float collisionTime;
	private bool invulnerable = false;
	private bool grounded = true;
	private bool isJumping = false;
	private bool facingRight = false;
	private Vector3 direction;
	private Rigidbody2D rb2D;


	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
		rb2D.gravityScale = gravityScale;
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

		if (Input.GetKeyDown (KeyCode.W) && grounded) {
			isJumping = true;
		}

		if (Input.GetKeyDown (KeyCode.W) && !grounded && !doubleJumped) {
			isJumping = true;
			doubleJumped = true;
		}
	}

	void Move () {
		if (direction != new Vector3 (0, 0, 0))
			//transform.Translate (speed * direction * Time.deltaTime);
			this.GetComponent<Rigidbody2D>().transform.Translate(speed * direction * Time.deltaTime);
	}
		
	void Jump () {
		if (isJumping) {
			rb2D.AddForce (new Vector2 (0f, jumpForce * jumpHeight));
			isJumping = false;
		}
	}

	void Bomb () {
		Debug.Log ("Bomb has been planted"); 
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
