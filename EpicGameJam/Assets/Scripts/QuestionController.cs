using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionController : MonoBehaviour {

	public List<Question> qList;
	int pointer = 0;

	// Use this for initialization
	void Start () {
		foreach (var item in qList) {
			item.gameObject.SetActive (false);
		}
		qList [pointer].gameObject.SetActive (true);
	}
	
	public void NextQuestion(){
		pointer++;
		if (pointer <= qList.Count - 1) {
			qList [pointer].gameObject.SetActive (true);
		} else {
			//start game here
		}
	}
}
