using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour, IWorldObject {

	public WorldController wCont;
	public AudioClip behemothSpawn;
	//how many enemies can spamn this point
	public int maxEnemies;

	//enemy hp mul
	float hpMul = 1;

	//cooldown of spawning in seconds
	public float coolDown;

	//enemy prefab
	public GameObject enemyPrefab;

	//delay from start when spawn point activates in seconds
	public float startDelay;

	//time when this obj was created
	float birthTime;

	//time when last enemy was spawned
	float lastEnemySpawned;

	//how many enemies have been spawned at the moment
	int enemiesSpawned = 0;

	// Use this for initialization
	void Start () {
		//init time when was created
		birthTime = Time.time;
		wCont = GameObject.FindObjectOfType<WorldController> ();
		InitParameters ();
	}
	
	// Update is called once per frame
	void Update () {
		//works only if delay was exceeded
		if (enemiesSpawned >= maxEnemies){
			this.enabled = false;
		}
		if (Time.time - birthTime > startDelay) {
			this.gameObject.GetComponent<Animator> ().SetBool ("active", true);
			if (Time.time - lastEnemySpawned > coolDown) {
				SpawnEnemy ();
				lastEnemySpawned = Time.time;
			}
		}
	}

	void SpawnEnemy(){
		enemiesSpawned++;
		object o = Instantiate (enemyPrefab,this.transform.position,Quaternion.identity);
		GameObject go = o as GameObject;
		go.GetComponent<Enemy> ().enemyHp *= hpMul;
		if (go.GetComponent<Enemy> ().enemyType == "behemoth")
			SoundManager.instance.PlayEnemySound ();
	}

	public void InitParameters (){
		if (wCont.maxEnemies > 0) {
			maxEnemies = wCont.maxEnemies;
		}
		maxEnemies *= Mathf.FloorToInt( wCont.maxEnemiesMul);
		wCont.maxEnemiesMul =1;

		coolDown *= wCont.spawnCooldownMul;
		wCont.spawnCooldownMul = 1;

		hpMul = wCont.enemyHpMul;

	}
}
