using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMouseCursor : MonoBehaviour {    

    

	// Use this for initialization
	void Start () {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
