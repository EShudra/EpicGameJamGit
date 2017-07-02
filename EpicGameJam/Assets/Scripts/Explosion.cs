using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public void Explode () {
		GetComponent<Animator>().Play ("explosion4");
	}
}
