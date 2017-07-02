	using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IDestroyableObject {

	//speed of enemy movement
	public float enemySpeed;

	//enemy max hp
	public float enemyHp;

	//enemy damage
	[HideInInspector]public float enemyDamage;

	public AudioClip behemothOnHit;
	public AudioClip kittenDeath;
	public string enemyType;

	//movement vector
	public Vector3 moveVector = new Vector3 ( 1, 0, 0);

	BoxCollider2D thisCollider;
	private Animator anim;

	// Use this for initialization
	void Start () {
		thisCollider = this.GetComponent<BoxCollider2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate(){
		Move ();
		anim.SetBool ("onHit", false);
	}

	void Move(){
		this.transform.Translate (moveVector * enemySpeed * Time.deltaTime);
	}

	public void GetDamage (float damage){
		enemyHp -= damage;

		if (enemyHp <= 0) {
			if (this.enemyType == "kitten")
				StartCoroutine (playKittemSound ());
			anim.SetTrigger ("dead");
			//this.GetComponent<BoxCollider2D> ().enabled = false;
			this.transform.tag = "";
			transform.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0,300f));
			this.GetComponent<Rigidbody2D> ().gravityScale = 0.5f;
			enemySpeed = 0;
			Destroy (gameObject, 0.7f);
		}
	}

	void OnCollisionStay2D(Collision2D coll){
		if (coll.collider.tag == "Bullet"){
			GetDamage(coll.collider.gameObject.GetComponent<Bullet> ().damage);

			anim.SetBool ("onHit", true);
		}

		//reverse scale if collidessmth exept player
		if ((coll.collider.tag != "Player") && (coll.collider.tag != "Bullet") && (coll.collider.tag != "Enemy") && (coll.collider.tag != "ExitPoint")) {

			RaycastHit2D[] hits;
			thisCollider.enabled = false;
			Vector3 end = this.transform.position;

			end.x = end.x + thisCollider.size.x / 2 * this.transform.localScale.x;

			end += moveVector * enemySpeed * Time.deltaTime;
			hits = Physics2D.LinecastAll (this.transform.position, end);
			//hits = Physics2D.BoxCastAll (this.transform.position, thisCollider.size*0.9f, 0, moveVector, thisCollider.size.x);

			Debug.DrawLine (this.transform.position, end, Color.red);
			thisCollider.enabled = true;

			if (hits.Length != 0) {
				foreach (var hit in hits) {
					if ((hit.collider.tag != "Player") && (hit.collider.tag != "Bullet") && (hit.collider.tag != "Enemy") && (coll.collider.tag != "ExitPoint")) {
						Debug.Log ("TURN");
						this.transform.localScale = new Vector3 (this.transform.lossyScale.x * -1, this.transform.lossyScale.y, this.transform.lossyScale.z);
						moveVector *= -1;
					}
				}
			}
		}
	}

	IEnumerator playKittemSound(){
		yield return new WaitForSeconds (Random.value * 0.3f);
		GetComponent<AudioSource> ().Play ();
	}

}
