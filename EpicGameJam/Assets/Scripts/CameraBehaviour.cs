using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		if (Input.GetMouseButtonDown (0)) {
			anim.SetTrigger ("Shake");
		}
=======
		/*if (Input.GetMouseButtonDown (0)) {
			anim.SetTrigger ("Shake");
		}*/
>>>>>>> master
	}

	public void shakeScreen(){
		anim.SetTrigger ("Shake");
	}

}
