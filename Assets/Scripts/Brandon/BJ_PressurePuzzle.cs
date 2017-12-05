using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BJ_PressurePuzzle : MonoBehaviour {

    public GameObject PressureBox;
    public Transform BoxSpawn;

    private Rigidbody PBrb;
    private bool onlyOnce = false;

	// Use this for initialization
	void Start () {

        PBrb = PressureBox.GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (onlyOnce == false)
        {
            if (other.tag == "Player")
            {
                GameObject PBCopy = Instantiate(PressureBox, BoxSpawn.position, Quaternion.identity) as GameObject;
                onlyOnce = true;
            }
        }
    }
}
