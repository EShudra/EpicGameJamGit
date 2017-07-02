using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public CameraBehaviour cam;
	public float explosionRadius = 2f;
	public float explosionDamage = 500f;
	public AudioClip explosionSound;
	private Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
		anim.Play ("explosion4");
		GameObject.FindObjectOfType<CameraBehaviour>().shakeScreen ();
		SoundManager.instance.PlaySingle (explosionSound);

		RaycastHit2D[] hits = Physics2D.CircleCastAll (this.transform.position, explosionRadius, new Vector3(0,0,1));
		Debug.Log ("hits.Length: "+hits.Length);

		foreach (RaycastHit2D obj in hits) {
			if (obj.transform.tag == "Enemy") {
				obj.transform.gameObject.GetComponent<Enemy>().GetDamage (explosionDamage);
			}
				
		}

		Destroy (this.gameObject, 1f);
	}
}
