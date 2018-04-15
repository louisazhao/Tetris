using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public GameObject confirmationPanel;
	public Button yesButton;
	public Button noButton;
	public Button pauseButton;

	public void playagain()
	{
		SceneManager.LoadScene ("level");
	}
	public void startGame()
	{
		SceneManager.LoadScene ("level");
	}
	public void pauseGame()
	{
		if (Game.gamePaused==false)
			Game.gamePaused = true;
		else
			Game.gamePaused = false;
	}

	public void goBackToStart()
	{
		confirmationPanel.SetActive (true);
		pauseButton.interactable = false;
		Game.gamePaused = true;
		yesButton.onClick.AddListener (yesIsPressured);
		noButton.onClick.AddListener (noIsPressured);
	}

	public void yesIsPressured()
	{
		SceneManager.LoadScene ("Start");
		Game.gamePaused = false;
	}
	public void noIsPressured()
	{
		Game.gamePaused = false;
		pauseButton.interactable = true;
		confirmationPanel.SetActive (false);
	}
}
