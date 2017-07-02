using UnityEngine;
using System.Collections;

public class ExitPoint : MonoBehaviour {


	public AudioClip doorOpening;
	public AudioClip doorOverTime;
	public AudioClip doorInteraction;

	private float lastTimePrincessScreamed;
	private float princessCooldown = 3f;
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
	}
	
	// Update is called once per frame
	void Update () {
		
		if (timeWhenOpens < Time.time - birthTime ){
			this.gameObject.GetComponent<Animator> ().SetBool ("active", true);	
			//SoundManager.instance.PlaySingle (doorOpening);
		}

		if ((!PrincessIsOut) && (timeWhenOpens + 0.7f < Time.time - birthTime)) {
			PrincessIsOut = true;
			Princess.gameObject.SetActive(true);
			exitOpened = true;
			//SoundManager.instance.PlaySingle (doorOverTime);
			//lastTimePrincessScreamed = Time.time;

			//object o = Instantiate (PrincessPrefab, PrincessPrefab.transform.position, Quaternion.identity, this.transform);
			//GameObject go = o as GameObject;
			//go.transform.localScale = PrincessPrefab.transform.localScale;

		}
		/*
		if (PrincessIsOut && (lastTimePrincessScreamed+princessCooldown)<Time.time) {
			SoundManager.instance.PlaySingle (doorOverTime);
			lastTimePrincessScreamed = Time.time;
		}*/

	}

	void OnTriggerStay2D(Collider2D enteringCollider){
		if (enteringCollider.tag == "Player"){
			//SoundManager.instance.PlaySingle (doorInteraction);
			EndLevel();
		}
	}

	void EndLevel(){
		if (exitOpened) {
			Destroy (level);
			//qCont.NextQuestion ();
			/*foreach (var item in GameObject.FindObjectsOfType<GameObject>()) {
				if (item.GetComponent<Camera> () == null) {
					Destroy (item);
				}
			}*/
		}
	}


}
