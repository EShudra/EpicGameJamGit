using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	//bullet moving speed
	public float bulletSpeed;

	//bullet damage
	public float damage;

	//true if bullet pass through enemies
	public bool piercing;

	//move bullet formard
	void Move(){
		this.transform.Translate (new Vector3(bulletSpeed*Time.deltaTime, 0, 0));	
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}
}
