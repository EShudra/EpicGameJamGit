using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	//how many enemies can spamn this point
	public int maxEnemies;

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
		Instantiate (enemyPrefab,this.transform.position,Quaternion.identity);
	}

}
