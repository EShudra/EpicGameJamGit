using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionController : MonoBehaviour {

	public GameObject endGame;

	public WorldController wCont;

	//public List<Question> qList;
	public List<GameObject> goList;
	int pointer = 0;

	// Use this for initialization
	void Start () {
		foreach (var item in goList) {
			item.SetActive (false);
		}
		goList [pointer].SetActive (true);
	}
	
	public void NextQuestion(){
		//GameObject.FindObjectOfType<WorldController>().UpdateSettings();
		wCont.UpdateSettings();
		pointer++;
		if (pointer <= goList.Count - 1) {
			//Debug.Log ("NEXT POS");
			Destroy (goList [pointer-1]);
			goList [pointer].SetActive (true);
		} 
		/*else {
			//Time.timeScale = 0.05f;
			//Debug.Log("!!!!!!!!!!!");
			//GameObject.FindGameObjectWithTag ("endGame").SetActive (true);
			GameObject.FindGameObjectWithTag ("endGame").gameObject.SetActive (true);
			endGame.SetActive (true);
			//show end game <-------------
		}*/
	}
}
