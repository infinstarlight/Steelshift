using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BJ_EnterRoom : MonoBehaviour {

    public GameObject Room;
    public Animator RoomAnim;

	// Use this for initialization
	void Start () {

        RoomAnim = Room.GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (RoomAnim.GetBool("Entered?") == false)
            {
                RoomAnim.SetBool("Entered?", true);
            }
        }
    }
}
