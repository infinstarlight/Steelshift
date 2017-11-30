using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentShift : MonoBehaviour {

    
    public Animator growShift;
    private static bool Present = false;
    private bool oneTime = false;


	// Use this for initialization
	void Start () {
        growShift = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        GoingUp();
	}

   //public static void MoveGameObjectToScene(GameObject, SceneManagement.Scene scene);

    void GoingUp()
    {
        if (Present == true && Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            
                Present = false;
                gameObject.SetActive(false);
                TimeShift.grow = true;
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
    if(other.tag == "Player")
        {
            Present = true;
            print("Player is present");
        }    
    }

}
