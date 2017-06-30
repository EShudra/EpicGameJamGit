using UnityEngine;
using System.Collections;

public class PlatformerCharacter : MonoBehaviour {

	/*
	 * 1) Add ground to the "Ground" layer;
	 * 2) In Rigidbody2D set freeze rotation by z-axis;
	 * 
	 * Increase gravity scale to 5 (for example) and jump force to 1000.
	 */

	public float moveSpeed = 10f;
	public float jumpForce = 1000f;
	public Transform groundCheck;

	private bool grounded = true;
	private bool isJumping = false;
	private bool facingRight = false;
	private Vector3 direction;
	private Rigidbody2D rb2D;


	void Awake () {
		rb2D = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		//Movement
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		direction = new Vector3 (0, 0, 0);

		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			direction += Vector3.right;
			facingRight = true;
		}

		if (Input.GetKey (KeyCode.A)) {
			direction += Vector3.left;
			facingRight = false;
		}

		if (Input.GetKey (KeyCode.Space) && grounded) {
			isJumping = true;
		}

		facingRight = false;

	}

	void FixedUpdate () {
		//Movement
		if (direction != new Vector3 (0, 0, 0))
			transform.Translate (moveSpeed * direction * Time.deltaTime);

		if (isJumping) {
			rb2D.AddForce (new Vector3 (0f, jumpForce, 0f));
			isJumping = false;
		}
	}
}
