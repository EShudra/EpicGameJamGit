using UnityEngine;
using System.Collections;

public class Question : MonoBehaviour {

	//places for answers
	public GameObject[] answerPlacers;

	//DRAG & DROP answer library
	public AnswersLibrary aLib;

	Answer[] ansPrefabs = new Answer[3];

	// Use this for initialization
	void Start () {
		ansPrefabs = aLib.GetAnswers ();
		Instantiate (ansPrefabs[0],answerPlacers[0].transform.position,Quaternion.identity,this.transform);
		Instantiate (ansPrefabs[1],answerPlacers[1].transform.position,Quaternion.identity,this.transform);
		Instantiate (ansPrefabs[2],answerPlacers[2].transform.position,Quaternion.identity,this.transform);
	}

	void OnDestroy(){
		//this.transform.GetComponentInParent<QuestionController> ().NextQuestion ();
		GameObject.FindObjectOfType<WorldController>().UpdateSettings();
		this.transform.parent.gameObject.GetComponent<QuestionController>().NextQuestion();
	}
	

}
