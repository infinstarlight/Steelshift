using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryMesh : MonoBehaviour {

    public GameObject Batt;
    private Renderer BattMesh;
    private Collider BattSolid;

	// Use this for initialization
	void Start () {
        BattMesh = Batt.GetComponent<Renderer>();
        BattSolid = Batt.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void BatteryGone ()
    {
        BattMesh.enabled = false;
        BattSolid.enabled = false;
    }

    void BatteryBack()
    {
        BattMesh.enabled = true;
        BattSolid.enabled = true;
    }
}
