using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
	public static int gridWidth = 10;
	public static int gridHeight=20;
	public GameObject[] tetrominos=new GameObject[7];
	public static Transform[ , ] grid = new Transform[gridWidth, gridHeight];
	public int scoreOneLine=40;
	public int scoreTwoLine = 100;
	public int scoreThreeLine=300;
	public int scoreFourLine=1200;
	private int noOfLinesThisTurn = 0;
	private AudioSource audio;
	public static int currentScore = 0;
	public Text scoreText;
	public Text levelDisplay;
	public ParticleSystem brokenEffect;
	public Image m_image;

	private GameObject nextBlock;
	private GameObject nextNextBlock;
	private bool gameStarted=false;
	private int originalLevel;
	private int increasedLevel=0;
	private int currentLevel;

	public static bool gamePaused=false;


	// Use this for initialization
	void Start () {
		currentScore = 0;
		originalLevel = SliderManager.levelValue;
		spawnNextMino ();
	}

	void Update()
	{
		updateScore ();
		updateUI ();
		updateLevel ();
		updateSpeed ();
	}

	void updateLevel()
	{
		increasedLevel = currentScore / 3000;
		currentLevel = originalLevel + increasedLevel;

	}

	void updateSpeed()
	{
		Tetromino.fallSpeed = 1.0f - ((float)currentLevel * 0.1f);
	}

	public void updateUI()
	{
		scoreText.text = "Score: " + currentScore.ToString ();
		levelDisplay.text = "Level: "+currentLevel.ToString ();
		audio = GetComponent<AudioSource> ();
	}

	public bool checjIsAboveGrid(Tetromino tetromino)
	{
		for(int x=0;x<gridWidth;x++){
			foreach (Transform mino in tetromino.transform) {
				Vector2 pos = round (mino.position);
				if (pos.y > gridHeight - 1) {
					return true;
				}
			}
		}
		return false;


	}


	public bool checkIsInsideGrid(Vector2 position)
	{
		return((int)position.x >= 0 && (int)position.x < gridWidth && (int)position.y >= 0);
	}

	public Vector2 round(Vector2 position)
	{
		return new Vector2 (Mathf.Round (position.x), Mathf.Round (position.y));
	}

	public void spawnNextMino()
	{
		if (gamePaused == false) {
			int randomIndex;
			if (!gameStarted) {
				gameStarted = true;
				randomIndex = Random.Range (0, 6);
				nextBlock = tetrominos [randomIndex];	
				Instantiate (nextBlock, new Vector2 (5, 20), Quaternion.identity);
				m_image.sprite = nextBlock.GetComponent<Tetromino> ().apparence;
			} else {
				Instantiate (nextBlock, new Vector2 (5, 20), Quaternion.identity);
				randomIndex = Random.Range (0, 6);
				nextNextBlock = tetrominos [randomIndex];
				m_image.sprite = nextNextBlock.GetComponent<Tetromino> ().apparence;
				nextBlock = nextNextBlock;
			}
		}
	}


	public void updateGrid(Tetromino tetromino)
	{
		for (int y = 0; y < gridHeight; y++) {
			for (int x = 0; x < gridWidth; x++) {
				if (grid [x, y] != null) {
					if (grid [x, y].parent == tetromino.transform) {
						grid [x, y] = null;
					}
				}
			}

		}
		foreach (Transform mino in tetromino.transform) {
			Vector2 pos = round (mino.position);
			if (pos.y < gridHeight) {
				grid [(int)pos.x, (int)pos.y] = mino;
			}
		}

	}

	public Transform getTransformAtGrid(Vector2 pos)
	{
		if (pos.y > gridHeight - 1) {
			return null;
		} else {
			return grid [(int)pos.x, (int)pos.y];
		}
	}

	public bool isFullRow(int y)
	{
		for (int x = 0; x < gridWidth; x++) {
			if (grid [x, y] == null) {
				return false;
			}
		}
		noOfLinesThisTurn++;
		return true;
	}

	public void deleteMinoAt(int y)
	{
		for (int x = 0; x < gridWidth; x++) {
			//Instantiate (brokenEffect, new Vector3(grid [x, y].transform.position.x,grid [x, y].transform.position.y,0),Quaternion.identity);
			Destroy (grid [x, y].gameObject);
			grid [x, y] = null;
		}			
	}

	public void moveRowDown(int y)
	{
		for (int x = 0; x < gridWidth; x++) {
			if (grid [x, y] != null) {
				grid [x, y - 1] = grid [x, y];
				grid [x, y] = null;
				grid [x, y - 1].position += new Vector3 (0, -1, 0);
			}
		}
	}

	public void moveAllRowsDown(int y)
	{
		for (int i = y; i < gridHeight; i++) {
			moveRowDown (i);
		}
	}

	public void DeleteRow()
	{
		for (int y = 0; y < gridHeight; y++) {
			if (isFullRow (y)) {
				deleteMinoAt (y);
				audio.Play ();
				moveAllRowsDown (y+1);
				--y;//because the row is deleted, we have to decrease the y
			}
		}
	}

	public void gameover()
	{
		SceneManager.LoadScene("gameover");
	}
		

	public void updateScore()
	{
		if (noOfLinesThisTurn > 0) {
			if (noOfLinesThisTurn == 1) {
				clearedOneLine ();
			} else if (noOfLinesThisTurn == 2) {
				clearedTwoLines ();
			} else if (noOfLinesThisTurn == 3) {
				clearedThreeLines ();
			} else if (noOfLinesThisTurn == 4) {
				clearedFourLines ();
			}
			noOfLinesThisTurn = 0;
		}
	}

	void clearedOneLine()
	{
		currentScore += scoreOneLine;
	}
	void clearedTwoLines()
	{
		currentScore += scoreTwoLine;
	}
	void clearedThreeLines()
	{
		currentScore += scoreThreeLine;
	}
	void clearedFourLines()
	{
		currentScore += scoreFourLine;
	}
}
