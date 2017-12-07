using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawner : MonoBehaviour {

    public GameObject SceneManager;
    public static bool SpawnOnce;

	
	// Update is called once per frame
	void Update () {
	if(SpawnOnce == false)
        {
            Instantiate(SceneManager, transform.position, transform.rotation);
            SpawnOnce = true;
        }	

    
	}
}
