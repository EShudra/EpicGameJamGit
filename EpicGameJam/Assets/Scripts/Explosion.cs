using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour, IWorldObject {

	public WorldController wCont;

	public CameraBehaviour cam;
	public float explosionRadius = 2f;
	public float explosionRadiusMul = 1f;
	public float explosionDamage = 500f;
	public AudioClip explosionSound;
	private Animator anim;

	void Start () {
		wCont = GameObject.FindObjectOfType<WorldController> ();
		InitParameters ();

		this.transform.localScale = new Vector3 (transform.localScale.x * explosionRadiusMul,
			transform.localScale.y * explosionRadiusMul,
			transform.localScale.z * explosionRadiusMul);
		GetComponent<Animator>().Play ("explosion4");

		anim = GetComponent<Animator> ();
		anim.Play ("explosion4");
		SoundManager.instance.PlaySingle (explosionSound);
		GameObject.FindObjectOfType<CameraBehaviour> ().shakeScreen ();

		RaycastHit2D[] hits = Physics2D.CircleCastAll (this.transform.position, explosionRadius*explosionRadiusMul, new Vector3(0,0,1));
		Debug.Log ("hits.Length: "+hits.Length);

		foreach (RaycastHit2D obj in hits) {
			if (obj.transform.tag == "Enemy") {
				obj.transform.gameObject.GetComponent<Enemy>().GetDamage (explosionDamage);
				obj.transform.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0,170f));
			}
				
		}

		Destroy (this.gameObject, 1f);
	}

	public void InitParameters(){
		explosionRadiusMul = wCont.explosionScale;
	}
}
