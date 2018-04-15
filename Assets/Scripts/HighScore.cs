using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

	private Text highScore;
	private static int high;

	// Use this for initialization
	void Start () {
		highScore = GetComponent<Text> ();
		high = PlayerPrefs.GetInt ("highscore", high);
		highScore.text = "High Score: " + high.ToString ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Game.currentScore > high) 
		{	
			high = Game.currentScore;
		}
		highScore.text = "High Score: " + high.ToString ();
		PlayerPrefs.SetInt ("highscore", high);
	}



}
