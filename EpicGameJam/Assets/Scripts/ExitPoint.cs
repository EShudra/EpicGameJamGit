using UnityEngine;
using System.Collections;

public class ExitPoint : MonoBehaviour {


	private AudioSource aus;
	public AudioClip doorOpening;
	public AudioClip doorOverTime;
	public AudioClip doorInteraction;

	private float lastTimePrincessScreamed;
	private float princessCooldown = 1.5f;
	//
	public QuestionController qCont;

	//
	public GameObject level;

	/*
	public static int lvl;
	public int maxLvl;
	*/

	//time from this created when exit will opens(in seconds)
	public float timeWhenOpens;

	//enabled(you can exit if you want)
	public bool exitOpened = false;

	//D&D
	public GameObject Princess;
	bool PrincessIsOut = false;

	float birthTime = 0;


	// Use this for initialization
	void Start () {
		birthTime = Time.time;
		aus = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (timeWhenOpens < Time.time - birthTime ){
			this.gameObject.GetComponent<Animator> ().SetBool ("active", true);	
			///aus.PlayOneShot (doorOpening, 0.7f);
		}

		if ((!PrincessIsOut) && (timeWhenOpens + 0.7f < Time.time - birthTime)) {
			PrincessIsOut = true;
			Princess.gameObject.SetActive(true);
			exitOpened = true;

			//aus.PlayOneShot (doorOverTime, 0.7f);
			lastTimePrincessScreamed = Time.time;

			//object o = Instantiate (PrincessPrefab, PrincessPrefab.transform.position, Quaternion.identity, this.transform);
			//GameObject go = o as GameObject;
			//go.transform.localScale = PrincessPrefab.transform.localScale;

		}

		if (PrincessIsOut && (lastTimePrincessScreamed+princessCooldown)<Time.time) {
			//aus.PlayOneShot (doorOverTime, 0.7f);
			lastTimePrincessScreamed = Time.time;
			Debug.Log ("You heard the clip, didn't you?");
		}

	}

	void OnTriggerStay2D(Collider2D enteringCollider){
		if (enteringCollider.tag == "Player"){
			//aus.PlayOneShot (doorInteraction, 0.7f);
			Debug.Log ("Intereaction with the princess");
			EndLevel();
		}
	}

	void EndLevel(){
		if (exitOpened) {
			//Time.timeScale = 0.05f;
			//Destroy (level.gameObject, 3);
			foreach ( var item in GameObject.FindObjectsOfType<Enemy>()){
				Destroy(item.gameObject);
			}		
			foreach ( var item in GameObject.FindObjectsOfType<SpawnPoint>()){
				Destroy(item.gameObject);
			}	
			qCont.NextQuestion ();
			/*foreach (var item in GameObject.FindObjectsOfType<GameObject>()) {
				if (item.GetComponent<Camera> () == null) {
					Destroy (item);
				}
			}*/
		}
	}


}
