using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	public WorldController wcont;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClick(){
		wcont.StartGame ();
	}
}
