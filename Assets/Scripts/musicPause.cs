using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPause : MonoBehaviour {

	private AudioSource backgroundMusic;

	// Use this for initialization
	void Start () {
		backgroundMusic = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Game.gamePaused == true && backgroundMusic.isPlaying) {
			backgroundMusic.Pause ();
		} 
		else if(Game.gamePaused == false && !backgroundMusic.isPlaying)
		{
			backgroundMusic.Play ();
		}
	}
}
