using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionController : MonoBehaviour {

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
		pointer++;
		if (pointer <= goList.Count - 1) {
			goList [pointer].SetActive (true);
		} else {
			//start game here
		}
	}
}
