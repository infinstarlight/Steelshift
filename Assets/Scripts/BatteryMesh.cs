using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryMesh : MonoBehaviour {

    public Renderer Batt;

	// Use this for initialization
	void Start () {
        Batt = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void BatteryGone ()
    {
        Batt.enabled = false;
    }

    void BatteryBack()
    {
        Batt.enabled = true;
    }
}
