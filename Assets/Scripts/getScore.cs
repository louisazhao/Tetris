using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class getScore : MonoBehaviour {
	private Text scoreDisplay;
	private int score;


	// Use this for initialization
	void Start () {
		scoreDisplay = GetComponent<Text> ();
		score = Game.currentScore;
	}
	
	// Update is called once per frame
	void Update () {
		scoreDisplay.text = "Your score this time: " + score.ToString ();
	}
}
