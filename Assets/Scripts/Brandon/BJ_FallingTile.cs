using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BJ_FallingTile : MonoBehaviour {

    public float Timer = 5.0f;
    public bool isHere = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if(isHere == true)
        {
            Timer--;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        isHere = true;
        if(other.tag == "Player" && Timer == 0)
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

           
    



}
