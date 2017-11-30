using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthPresent : MonoBehaviour {

    public Animator growShift;


    // Use this for initialization
    void Start () {
        growShift = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        GoingUp();
	}

    void GoingUp()
    {
        if (TimeShift.grow == true)
        {
            if (growShift.GetBool("Past") == false)
            {
                growShift.SetBool("Past", true);
                gameObject.SetActive(false);
               
            }
        }
    }

}
