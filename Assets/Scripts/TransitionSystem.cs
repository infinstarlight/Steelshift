using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSystem : MonoBehaviour {

    private Transition[] points;
    public Transform playerPos;
    private Vector3 endPos = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        points = GameObject.FindObjectsOfType<Transition>();
        foreach (Transition tp in points)
        {
            if (tp.isHere == true)
            {
                tp.exitPoint.isExit = true;
                endPos = tp.exitPoint.transform.position;
                tp.isHere = false;
            }
        }
	}
}
