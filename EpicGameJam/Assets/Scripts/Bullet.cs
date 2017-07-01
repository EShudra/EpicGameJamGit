using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	//bullet moving speed
	public float bulletSpeed;

	//bullet damage
	public float damage;

	//timebirth
	float birthTime;
	//lifrtime of bullet
	public float lifetime = 10;

	//bool piercing = false;

	//move bullet formard
	void Move(){
		this.transform.Translate (new Vector3(bulletSpeed*Time.deltaTime, 0, 0), Space.Self);

		/*if (this.transform.localScale.x > 0) {
			//this.transform.localPosition += new Vector3 (bulletSpeed * Time.deltaTime, 0, 0);
			//this.transform.localPosition += new Vector3 (bulletSpeed * Time.deltaTime, 0, 0);
			this.transform.localPosition.x += bulletSpeed * Time.deltaTime;
		} else {
			//this.transform.localPosition -= new Vector3 (bulletSpeed * Time.deltaTime, 0, 0);
			this.transform.localPosition.x -= bulletSpeed * Time.deltaTime;
		}*/

	}


	// Use this for initialization
	void Start () {
		birthTime = Time.time;
		//init bullet piercing


		if (this.transform.localScale.x < 0) {
			if (this.transform.rotation.eulerAngles.z > 0) {
				//this.transform.rotation.eulerAngles.Set (0, 0, -180 - this.transform.rotation.eulerAngles.z);
				transform.rotation = Quaternion.Euler( 0, 0, -180 - this.transform.rotation.eulerAngles.z);
				//this.transform.localScale.Set (this.transform.localScale.x*-1,1,1);
				transform.localScale = new Vector3(this.transform.localScale.x*-1,this.transform.localScale.y,this.transform.localScale.z);
			} else {
				//this.transform.rotation.eulerAngles.Set (0, 0, 180 - this.transform.rotation.eulerAngles.z);
				transform.rotation = Quaternion.Euler( 0, 0, 180 - this.transform.rotation.eulerAngles.z);
			}
		} 

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move ();
		//preventing bullet from stucking
		if (Time.time - birthTime > lifetime) {
			Destroy (this.gameObject);
		}
	}

	void OnCollisionStay2D  (Collision2D coll){
		/*foreach(GameObject item in collision.gameObject){
			Debug.Log("bam!");
		}*/
		//Debug.Log ("!!!!");
		//Debug.Log (coll.gameObject.tag);
		//if ((coll.gameObject.tag == "Wall")||(coll.gameObject.tag == "Enemy")) {
		if ((coll.gameObject.tag == "Wall")||(coll.gameObject.tag == "Enemy") || (coll.gameObject.tag == "Ground")) {
			this.enabled = false;
			Destroy (this.gameObject);
		}	
	}
	
}
