using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour, IWorldObject {

	public WorldController wCont;

	public AudioClip shootingSound;
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
		wCont = GameObject.FindObjectOfType<WorldController> ();
		InitParameters ();
	}

	void Update () {
		//spawn BULLET ->>>if SPACE is pressed and bullet spawn cooldown is ended
		if (Input.GetKey (KeyCode.Space) && (Time.time - lastBulletSpawnTime > fireRate)) {
			Debug.Log("fire!!!!");
			lastBulletSpawnTime = Time.time;
			foreach (float item in fireLinesAngle) {
				object obj = Instantiate( bulletPrefab, this.transform.position, Quaternion.Euler (0,0,item + fireLinesDispersionAngle*(Random.value*2-1)),this.transform );
				SoundManager.instance.PlaySingle (shootingSound);
				GameObject go = obj as GameObject;
				go.transform.position = go.transform.TransformPoint (go.transform.localPosition);
				//go.transform.localRotation.eulerAngles.z;
				this.transform.DetachChildren();
			}
		}
	}

	public void InitParameters (){
		fireRate = wCont.gunFireRate;
		fireRate *= wCont.gunFireRateMul;
		wCont.gunFireRateMul = 1;
		fireLinesDispersionAngle = wCont.gunAccuracyAngle;
		fireLinesAngle = new float[ wCont.gunFireLinesCount ];
		Debug.Log ("!!!!!"+fireLinesAngle.Length);
		for (int i = 0; i< fireLinesAngle.Length -1; i++){
			fireLinesAngle[i] = wCont.gunFiveLines[i];
			//Debug.Log ("!!!!!!!!!!"+ wCont.gunFiveLines[i]);
		}
	}
}

