﻿using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void shakeScreen(){
		anim.SetTrigger ("Shake");
	}

}
