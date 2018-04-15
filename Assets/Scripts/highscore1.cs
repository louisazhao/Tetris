using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highscore1 : MonoBehaviour {

	private Text highScore;
	private static int high;
	public Text record;

	// Use this for initialization
	void Start () {
		highScore = GetComponent<Text> ();
		high = PlayerPrefs.GetInt ("highscore", high);
		highScore.text = "Highest score: "+high.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		if (high == Game.currentScore) {
			record.text = "New Record!!";
		} else {
			record.text = " ";
		}
	}
}
