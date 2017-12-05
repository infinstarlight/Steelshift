using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TimeCountdown : MonoBehaviour {

    //This script is used for the time count. To use it, make sure it's attached to the canvas in some way. Make sure that a "Text" object is in the canvas, and put it in the script.

    public Text TimeCount; //Set this as the text in the canvas. 
    public float TimeNum = 60.0f; //Amount of Seconds

	// Use this for initialization
	void Start () {
        TimeCount.text = "00"; //Sets the time to 00 when start
	}
	
	// Update is called once per frame
	void Update () {

        if (TimeNum > 0)
        {
            TimeNum -= Time.deltaTime; //Counts down to 0
            TimeCount.text = TimeNum.ToString("#00"); //Sets to the first two numbers to be displayed. 
        }

        else if (TimeNum <= 0) //When time reaches zero, do this.
        {
            SceneManager.LoadScene("New Scene 1");
        }
		
	}
}
