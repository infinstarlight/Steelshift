using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Volume : MonoBehaviour {

    private GameObject Player;
    public Player_Stats PStats;

	// Use this for initialization
	void Start () {

        Player = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PStats.Health = 0;
        }
    }
}
