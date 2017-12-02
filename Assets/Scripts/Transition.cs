using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {

    private TransitionSystem transSys;
    public Transition entrancePoint;
    public Transition exitPoint;

    public bool interactRequired = false;
    public bool isHere = false;
    public bool isExit = false;

    public Vector3 oldPos;


	// Use this for initialization
	void Start () {
        transSys = GameObject.FindObjectOfType<TransitionSystem>();
        transform.parent = transSys.transform;
		
	}
	
    void StartTransition(Collider other)
    {
        if (transSys.playerPos == null)
        {
            transSys.playerPos = other.transform;
        }
        if (isExit == false)
        {
            isHere = true;
            oldPos = transSys.playerPos.position;
        }
    }

    public void TeleportMe()
    {
        transSys.playerPos.position = exitPoint.transform.position;
    }


	// Update is called once per frame
	void Update () {
		
	}
}
