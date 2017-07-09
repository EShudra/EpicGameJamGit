using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnswersLibrary : MonoBehaviour {

	//all answer prefabs
	public Answer[] allAnswers;

	public List<Answer> usedAnswers;


	// Use this for initialization
	void Start () {
	
	}

	//get 3 answers
	public Answer[] GetAnswers(){
		Debug.Log ("!!!!!!!!!!!!!!!!!");
		Answer[] ans = new Answer[3];
		bool valid = false;
		int c = 500;
		Answer currAns = new Answer();


		/////////---1---selecting firts answer---
		while (!valid) {
			valid = true;
			//get random answer from lib
			currAns = GetRandowAnswerFromLib ();
			Debug.Log (currAns.answerText);

			//if answer is one-time --> then check answer in used answers. if not then use it!
			if (currAns.oneTimeUse) {
				valid = !isAnswerAlreadyUsed (currAns);
			}
			//safe exit from endless cycle
			c--;
			if (c < 0) {
				Debug.Log ("1111111");
				return ans;}
		}


		//if we get an one-time answer then add it to used list
		if (currAns.oneTimeUse) {
			usedAnswers.Add (currAns);
		}
		if (currAns.oneTimeUseTag) {
			disableAllAnswerWithTag (currAns.parametrTag);
		}
		//add currAns to result
		ans[0] = currAns;


		//////////---2---selecting second answer---
		valid = false;
		c = 500;
		while (!valid) {
			//get random answer from lib
			currAns = GetRandowAnswerFromLib ();

			//check if this a duplicate?
			valid = true;

			if (currAns.oneTimeUse) {//check if already used
				valid = !isAnswerAlreadyUsed (currAns);
			} 
			if (ans [0].answerText == currAns.answerText) {//check if already picked
				valid = false;				
			} else {
				if ((currAns.repeat == false) && (ans [0].parametrTag == currAns.parametrTag)) {//check tag  if repeat tag not allowed
					valid = false;
				} 
			}

			//safe exit from endless cycle
			c--;
			if (c < 0) {Debug.Log ("22222222");return ans;}
		}


		//if we get an one-time answer then add it to used list
		if (currAns.oneTimeUse) {
			usedAnswers.Add (currAns);
		}
		if (currAns.oneTimeUseTag) {
			disableAllAnswerWithTag (currAns.parametrTag);
		}
		//add currAns to result
		ans[1] = currAns;


		//////////---3---selecting second answer---
		valid = false;
		c = 500;
		while (!valid) {
			//get random answer from lib
			currAns = GetRandowAnswerFromLib ();

			//check if this a duplicate?
			valid = true;

			if (currAns.oneTimeUse) {//check if already used
				valid = !isAnswerAlreadyUsed (currAns);
			} 

			if ((ans [0].answerText == currAns.answerText)||(ans [1].answerText == currAns.answerText))  {//check if already picked
				valid = false;				
			} 
			if ((currAns.repeat == false) && (ans [0].parametrTag == currAns.parametrTag)) {//check tag  if repeat tag not allowed
				valid = false;
			} 
			if ((currAns.repeat == false) && (ans [1].parametrTag == currAns.parametrTag)){
				valid = false;
			}

			/*Debug.Log (ans [0].parametrTag);
			Debug.Log (ans [1].parametrTag);
			Debug.Log (currAns.parametrTag);
			Debug.Log (ans [1].parametrTag == currAns.parametrTag);*/
		
			//safe exit from endless cycle
			c--;
			if (c < 0) {Debug.Log ("33333333");return ans;}
		}

		//if we get an one-time answer then add it to used list
		if (currAns.oneTimeUse) {
			usedAnswers.Add (currAns);
		}
		if (currAns.oneTimeUseTag) {
			disableAllAnswerWithTag (currAns.parametrTag);
		}
		//add currAns to result
		ans[2] = currAns;

		Debug.Log (ans[0].answerText);
		Debug.Log (ans[1].answerText);
		Debug.Log (ans[2].answerText);

		return ans;
	}

	Answer GetRandowAnswerFromLib(){
		return allAnswers[Random.Range (0, allAnswers.Length)];
	}



	bool isAnswerAlreadyUsed(Answer currAns){
		bool used = false;
		foreach (var item in usedAnswers) {
			if (item.answerText == currAns.answerText) {
				used = true;
			}
		}
		return used;
	}

	void disableAllAnswerWithTag(string filter){
		foreach (var item in allAnswers) {
			if (item.parametrTag == filter){
				usedAnswers.Add(item);
			}
		}
	}
}
