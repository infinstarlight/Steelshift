using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pastscript : MonoBehaviour {


    public GameObject Button;
    public GameObject item1;
    public GameObject item2;
    private bool isPressed = false;
    public Animator isPresent;

	// Use this for initialization
	void Start () {

        isPresent = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ButtonPress();
    }

    void ButtonPress()
    { 
        if(isPressed == true && Input.GetKeyUp(KeyCode.R))
        {
            if(isPresent.GetBool ("Time Stop") == false)
            {
                isPresent.SetBool("Time Stop", true);
            }
            else
            {
                isPresent.SetBool("Time Stop", false);
            }
        }
    }
}
