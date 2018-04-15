using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour {

	private Slider levelSlider;
	public static int levelValue;

	// Use this for initialization
	void Start () {
		levelSlider = GetComponent<Slider> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		updateLevel ();
	}


	void updateLevel()
	{
		levelValue = (int)levelSlider.value;
	}
}
