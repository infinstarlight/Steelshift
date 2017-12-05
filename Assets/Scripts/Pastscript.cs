using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pastscript : MonoBehaviour {


    public GameObject Button;
    public GameObject item1;
    public GameObject item2;
    private bool isPressed = false;
    private bool isHere = false;
    public Animator isPresent;
    public Animator isPresent2;

	// Use this for initialization
	void Start () {

        isPresent = item1.GetComponent<Animator>();
        isPresent2 = item2.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        ButtonPress();
    }

    void ButtonPress()
    { 
        if(isPressed == true && Input.GetKeyDown(KeyCode.R))
        {
            if(isPresent.GetBool ("TimeStop") == false)
            {
                isPresent.SetBool("TimeStop", true);
                isPresent2.SetBool("TimeStop", true);
            }
            else
            {
                isPresent.SetBool("TimeStop", false);
                isPresent2.SetBool("TimeStop", false);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isHere = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isHere = false;
        }
    }

}
