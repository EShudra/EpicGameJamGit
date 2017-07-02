using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	void Start () {
		GetComponent<Animator>().Play ("explosion4");
		//RaycastHit2D[] hits = Physics2D.CircleCastAll (this.transform.position, 2f * transform.localScale.x);

		Destroy (this.gameObject, 1.5f);
	}
}
