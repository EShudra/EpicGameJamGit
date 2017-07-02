using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WorldController : MonoBehaviour {

	public bool bulletPiercing = false;

	//prefabs of levels
	public GameObject[] levelPrefabs;

	//Drag & drop main menu here
	public GameObject mainMenu;

	//Drag & drop main retry here
	public GameObject retryMenu;

	//dictionary of game settings
	public Dictionary<string, string> gameSettings = new Dictionary<string, string>();


	//GAME SETTINGS LIBRARY
	//======================================//
	//player-------
	public float playerSpeed;
	public int playerMaxHp;
	public int playerHpIncrement;//<- use to add max hp
	public float playerJumpHeight;
	public bool playerDoubleJump;

	//gun---------
	public Sprite[] gunSprites;
	public int gunSprite;
	public float gunFireRate;
	public float[] gunFireLinesAngle;
	public int gunFireLinesCount;
	public float[] gunFiveLines = new float[5];
	public float gunFireLinesDispersion;
	public float gunAccuracyAngle;

	//bullet-------
	public Sprite[] bulletSprites;
	public int bulletSprite;
	public float bulletDamage;
	public float bulletSpeed;
	public float bulletLifetime;
	public float bulletDamageMul;
	public float bulletSpeedMul;

	//spawn point-----
	public int maxEnemies;
	public float maxEnemiesMul;
	public float spawnCooldownMul;

	//enemy----------
	public float enemyHpMul;

	//bomb----------
	public int maxBombs;

	//level---------
	public int currentLvlPrefab;

	//vfx
	public bool acidEffect;

	//======================================//


	void InitializeValue(){
		//player
		gameSettings.Add ("playerSpeed", playerSpeed.ToString());
		gameSettings.Add ("playerDoubleJump", playerDoubleJump.ToString());
		gameSettings.Add ("playerJumpPower", playerJumpHeight.ToString());
		gameSettings.Add ("playerHpIncrement", playerHpIncrement.ToString());

		//gun
		gameSettings.Add ("gunFireRate", gunFireRate.ToString());
		gameSettings.Add ("gunAccuracy", gunAccuracyAngle.ToString());
		gameSettings.Add ("gunFireLinesAmount", gunFireLinesCount.ToString());
		gameSettings.Add ("gunFireLineAngle1", gunFiveLines[0].ToString());
		gameSettings.Add ("gunFireLineAngle2", gunFiveLines[1].ToString());
		gameSettings.Add ("gunFireLineAngle3", gunFiveLines[2].ToString());
		gameSettings.Add ("gunFireLineAngle4", gunFiveLines[3].ToString());
		gameSettings.Add ("gunFireLineAngle5", gunFiveLines[4].ToString());
						
		//bullet
		gameSettings.Add ("bulletLifetime", bulletLifetime.ToString());
		gameSettings.Add ("bulletSpeed", bulletSpeed.ToString());
		gameSettings.Add ("bulletDamage", bulletDamage.ToString());
		gameSettings.Add ("bulletSpeedMul", bulletSpeed.ToString());
		gameSettings.Add ("bulletDamageMul", bulletDamage.ToString());

		//spawn point
		gameSettings.Add("enemiesPerSpawnPoint",maxEnemies.ToString());
		gameSettings.Add("enemiesPerSpawnPointMul",maxEnemiesMul.ToString());
		gameSettings.Add("spawnCoolDownMul",spawnCooldownMul.ToString());
		gameSettings.Add("enemyHPMul", enemyHpMul.ToString());

		//bomb
		gameSettings.Add("maxBombs", maxBombs.ToString());

		//vfx
		gameSettings.Add("acid", acidEffect.ToString());


		//

	}

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Bullet"), LayerMask.NameToLayer ("Enemy"), !bulletPiercing);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Enemy"), LayerMask.NameToLayer ("Enemy"), true);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Bullet"), LayerMask.NameToLayer ("Bullet"), true);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Bullet"), LayerMask.NameToLayer ("Player"), true);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Bomb"), LayerMask.NameToLayer ("Player"), true);

		InitializeValue ();
		}
	

	public void StartGame(){
		mainMenu.gameObject.SetActive (false);
		Instantiate (levelPrefabs [currentLvlPrefab]);
	}

	public void ShowRetryMenu(bool state){
		retryMenu.gameObject.SetActive (state);
	}

	public void UpdateGameSetting(string key, string value){
		gameSettings[key] = value;
	}

	public void UpdateSettings(){
		//=====player update settings========================================
		playerSpeed = float.Parse (gameSettings ["playerSpeed"]);

		if ((gameSettings ["playerDoubleJump"] == "true") || (gameSettings ["playerDoubleJump"] == "True")) {
			playerDoubleJump = true;
		} else {
			playerDoubleJump = false;
		}

		playerJumpHeight = float.Parse (gameSettings ["playerJumpPower"]);

		playerHpIncrement = int.Parse( gameSettings["playerHpIncrement"]);


		//=====gun=============================

		gunFireRate = float.Parse(gameSettings ["gunFireRate"]);

		gunFireLinesCount = int.Parse( gameSettings["gunFireLinesAmount"]);
		if (gunFireLinesCount > 5) {
			gunFireLinesCount = 5;
		}

		gunAccuracyAngle = float.Parse(gameSettings ["gunAccuracy"]);

		gunFiveLines[0] = int.Parse(gameSettings ["gunFireLineAngle1"]);
		gunFiveLines[1] = int.Parse(gameSettings ["gunFireLineAngle2"]);
		gunFiveLines[2] = int.Parse(gameSettings ["gunFireLineAngle3"]);
		gunFiveLines[3] = int.Parse(gameSettings ["gunFireLineAngle4"]);
		gunFiveLines[4] = int.Parse(gameSettings ["gunFireLineAngle5"]);


		//=====bullet========================
		bulletLifetime = float.Parse(gameSettings ["bulletLifetime"]);
		bulletSpeed = float.Parse(gameSettings ["bulletSpeed"]);
		bulletSpeedMul = float.Parse(gameSettings ["bulletSpeedMul"]);
		bulletDamage = float.Parse(gameSettings ["bulletDamage"]);
		bulletDamageMul = float.Parse(gameSettings ["bulletDamageMul"]);

		//=====spawn point==============
		enemyHpMul = float.Parse(gameSettings ["enemyHPMul"]);
		maxEnemies = int.Parse(gameSettings ["enemiesPerSpawnPoint"]);
		maxEnemiesMul = float.Parse(gameSettings ["enemiesPerSpawnPointMul"]);
		spawnCooldownMul =float.Parse(gameSettings ["spawnCoolDownMul"]);




	}
}
