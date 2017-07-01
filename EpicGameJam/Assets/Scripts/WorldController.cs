using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldController : MonoBehaviour {

	public bool bulletPiercing = false;

	//prefabs of levels
	public GameObject[] levelPrefabs;

	//Drag & drop main menu here
	public GameObject mainMenu;

	//Drag & drop main menu here
	public GameObject retryMenu;


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
		Instantiate (levelPrefabs [0]);
	}

	public void ShowRetryMenu(bool state){
		retryMenu.gameObject.SetActive (state);
	}


}
