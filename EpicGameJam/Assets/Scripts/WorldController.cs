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
	public Dictionary<string, string> gameSettings;


	//GAME SETTINGS LIBRARY
	//======================================//
	//player-------
	public float playerSpeed;
	public int playerMaxHp;
	public int playerHpIncrement;//<- use to add max hp
	public float playerJumpHeight;

	//gun---------
	public Sprite[] gunSprites;
	public int gunSprite;
	public float gunFireRate;
	public float[] gunFireLinesAngle;
	public float gunFireLinesDispersion;

	//bullet-------
	public Sprite[] bulletSprites;
	public int bulletSprite;
	public float bulletDamage;
	public float bulletSpeed;

	//bomb----------

	//level---------
	public int currentLvlPrefab;

	//======================================//


	void InitializeValue(){
		
	}

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Bullet"), LayerMask.NameToLayer ("Enemy"), !bulletPiercing);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Enemy"), LayerMask.NameToLayer ("Enemy"), true);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Bullet"), LayerMask.NameToLayer ("Bullet"), true);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Bullet"), LayerMask.NameToLayer ("Player"), true);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Bomb"), LayerMask.NameToLayer ("Player"), true);
		}
	
	// Update is called once per frame
	void Update () {
	
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
}
