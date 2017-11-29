using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visor : MonoBehaviour {

    public string tagWall = "Wall";
    public string tagTarget = "Enemy";
    public GameObject agent;

    // Use this for initialization
    void Start () {

        if(agent == null)
        {
            agent = gameObject;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerStay(Collider other)
    {
        string tag = other.gameObject.tag;
    }
}
