using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RescourceBarAmount : MonoBehaviour {

	public int parameter; //if 1 then hp else grenades
	public Player player;
	public Text textfield;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player> ();	
		if (player != null){
			if(parameter == 1){
				textfield.text = "HP amount: "+player.currentHp+"/"+player.maximumHp;}
			else{
				textfield.text = "Grenades amount: "+player.bombCurrentAmount+"/"+player.bombMaxCount;}
		}
	}
	
	// Update is called once per frame
	void Update () {
		player = FindObjectOfType<Player> ();
		if (player != null) {
			if (parameter == 1) {
				textfield.text = "HP amount: " + player.currentHp + "/" + player.maximumHp;
			} else {
				textfield.text = "Grenades amount: " + player.bombCurrentAmount + "/" + player.bombMaxCount;
			}
		}
	}
}
