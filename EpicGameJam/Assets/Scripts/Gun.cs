﻿using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	//how often bullets are spawned. period in seconds between spawn
	public float fireRate;

	//number of directions in which bullets are spawned
	public float[] fireLinesAngle;

	//bullets trajectory dispersion
	public float fireLinesDispersionAngle;

	//bullet prefab to spawn
	public GameObject bulletPrefab;

	//here we'll keep time when last bullet spawned
	float lastBulletSpawnTime;


	void Start () {
		//init Last bullet spawn time
		lastBulletSpawnTime = Time.time;
	}

	void Update () {

		//spawn BULLET ->>>if SPACE is pressed and bullet spawn cooldown is ended
		if (Input.GetKey (KeyCode.Space) && (Time.time - lastBulletSpawnTime > fireRate)) {
			lastBulletSpawnTime = Time.time;
			foreach (float item in fireLinesAngle) {

				object obj = Instantiate( bulletPrefab, this.transform.position, Quaternion.Euler (0,0,item + fireLinesDispersionAngle*(Random.value*2-1)),this.transform );
				GameObject go = obj as GameObject;
				go.transform.position = go.transform.TransformPoint (go.transform.localPosition);
				//go.transform.localRotation.eulerAngles.z;
				this.transform.DetachChildren();


			}

		}
	}
}

