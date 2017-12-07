using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShiftSFX : MonoBehaviour {
    public AudioClip Activate;

    private AudioSource aSource;


    // Use this for initialization
    void Start()
    {

        aSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update () {

        if(Input.GetButtonDown("Time Shift"))
        {
            aSource.PlayOneShot(Activate);
        }
		
	}
}
