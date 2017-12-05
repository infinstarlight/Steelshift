using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour {
    public Animator isPresent;
    public GameObject item1;
    // Use this for initialization
    void Start () {
        isPresent = item1.GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Pastscript.RightDoorOpen)
        {
            isPresent.SetBool("TimeStop", true);
        }
    }
}
