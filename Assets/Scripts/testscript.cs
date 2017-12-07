using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour {

    public AudioClip test;
    private AudioSource aSource;

	// Use this for initialization
	void Start () {
        aSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
       if(other.tag == "Player")
        {
            Debug.Log("Player is here.");
            if(Input.GetButtonDown("Interact"))
            {
                aSource.Play();
                Debug.Log("Player has pressed E");
            }
        }
    }
}
