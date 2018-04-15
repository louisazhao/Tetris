using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tetromino : MonoBehaviour {

	float fall=0;
	public static float fallSpeed = 1;
	public bool allowRotation = true;
	public bool limitRotation=false;
	public int individualScore = 100;
	private float individualTimer;
	private AudioSource minoSound;
	public AudioClip rotateSound;
	public AudioClip landSound;
	private float continuousVerticalSpeed = 0.05f;
	private float continuousHorizontalSpeed=0.1f;
	private float verticalTimer=0;
	private float horizontalTimer=0;
	private float buttonDownWaitMax = 0.2f;
	private float buttonDownWaitTimer=0;
	private bool moveImmediateHori = false;
	private bool moveImmediateVer=false;
	public Sprite apparence;

	// Use this for initialization
	void Start () {
		minoSound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		checkUserInput ();
		updateIndividualScore ();
	}

	void updateIndividualScore()
	{
		if (individualTimer < 1) {
			individualTimer += Time.deltaTime;
		} else {
			individualTimer = 0;
			individualScore = Mathf.Max (individualScore - 10,0);
		}
	}

	void checkUserInput()
	{
		if (Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.DownArrow)) {
			horizontalTimer = 0;
			verticalTimer = 0;
			buttonDownWaitTimer = 0;
			moveImmediateVer = false;
			moveImmediateHori = false;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			if (moveImmediateHori) {
				if (buttonDownWaitTimer < buttonDownWaitMax) {
					buttonDownWaitTimer += Time.deltaTime;
					return;
				}
				if (horizontalTimer < continuousHorizontalSpeed) {
					horizontalTimer += Time.deltaTime;
					return;
				}
			}

			if (!moveImmediateHori) {
				moveImmediateHori = true;
			}
			horizontalTimer = 0;
			transform.position += new Vector3 (1,0,0);
			if (checkIfValidPosition ()) {
				minoSound.volume = 0.03f;
				minoSound.PlayOneShot (rotateSound);
				FindObjectOfType<Game> ().updateGrid (this);

			} else {
				transform.position -= new Vector3 (1, 0, 0);
			}
				
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			if (moveImmediateHori) {
				if (buttonDownWaitTimer < buttonDownWaitMax) {
					buttonDownWaitTimer += Time.deltaTime;
					return;
				}
				if (horizontalTimer < continuousHorizontalSpeed) {
					horizontalTimer += Time.deltaTime;
					return;
				}
			}
			if (!moveImmediateHori) {
				moveImmediateHori = true;
			}
			horizontalTimer = 0;
			transform.position -= new Vector3 (1,0,0);
			if (checkIfValidPosition ()) {
				minoSound.volume = 0.03f;
				minoSound.PlayOneShot (rotateSound);
				FindObjectOfType<Game> ().updateGrid (this);

			} else {
				transform.position += new Vector3 (1, 0, 0);
			}

		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (allowRotation) {
				if (limitRotation) {
					if (transform.rotation.eulerAngles.z >= 90) {
						transform.Rotate (0, 0, -90);
					} else {
						transform.Rotate (0, 0, 90);
					}
				} else {
					transform.Rotate (0, 0, 90);
				}

				if (checkIfValidPosition ()) {
					FindObjectOfType<Game> ().updateGrid (this);
					minoSound.volume = 0.03f;
					minoSound.PlayOneShot (rotateSound);

				} else {
					if (limitRotation) {
						if (transform.rotation.eulerAngles.z >= 90) {
							transform.Rotate (0, 0, -90);
						} else {
							transform.Rotate (0, 0, 90);
						}
					} else {
						transform.Rotate (0, 0, -90);
					}
				}
			}

		} else if (Input.GetKey (KeyCode.DownArrow)||Time.time-fall>=fallSpeed) {
			if (moveImmediateVer) {
				if (buttonDownWaitTimer < buttonDownWaitMax) {
					buttonDownWaitTimer += Time.deltaTime;
					return;
				}
				if (verticalTimer < continuousVerticalSpeed) {
					verticalTimer += Time.deltaTime;
					return;
				}
			}

			if (!moveImmediateVer) {
				moveImmediateVer = true;
			}
			verticalTimer = 0;
			if (Game.gamePaused == false) {
				transform.position -= new Vector3 (0, 1, 0);
			}
			if (checkIfValidPosition ()) {
				FindObjectOfType<Game> ().updateGrid (this);
				if (Input.GetKey(KeyCode.DownArrow)) {
					minoSound.volume = 0.03f;
					minoSound.clip = rotateSound;
					minoSound.Play ();
				}
			} else {
				transform.position += new Vector3 (0,1,0);
				FindObjectOfType<Game> ().DeleteRow ();
				if (FindObjectOfType<Game> ().checjIsAboveGrid (this)) {
					FindObjectOfType<Game> ().gameover ();
				} else {
					enabled = false;
					minoSound.volume = 1.0f;
					minoSound.clip = landSound;
					minoSound.Play ();
					Game.currentScore += individualScore;
					FindObjectOfType<Game> ().spawnNextMino ();
				}
			}
			fall = Time.time;
		}
	}

	bool checkIfValidPosition()
	{
		foreach (Transform mino in transform) {
			Vector2 pos = FindObjectOfType<Game> ().round (mino.position);
			if (FindObjectOfType<Game> ().checkIsInsideGrid (pos) == false) {
				return false;
			}
			if (FindObjectOfType<Game> ().getTransformAtGrid (pos) != null && FindObjectOfType<Game> ().getTransformAtGrid (pos).parent != transform) {
				return false;
			}
		}
		return true;
	}
		
}
