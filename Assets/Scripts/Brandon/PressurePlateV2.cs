using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateV2 : MonoBehaviour {

    public AudioClip Activate;
    public AudioClip Deactivate;
    public Animator PlateAnim;
    public GameObject Door;
    


    private AudioSource PressureAudio;
   // private bool Toggle = false;
    

	// Use this for initialization
	void Start () {

        PressureAudio = GetComponent<AudioSource>();
        PlateAnim = Door.GetComponent<Animator>();
       
		
	}
	
	// Update is called once per frame
	void Update () {

        ToggleAnim();

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Phys")
        {
            //   Toggle = true;

            if (PlateAnim.GetBool("Activated") == false)
            {
                PlateAnim.SetBool("Activated", true);
                PlateAnim.SetBool("Idle", true);

                PressureAudio.clip = Activate;
                PressureAudio.Play();
                print("Pressure Plate Triggered.");
            }

        }
    }

    void ToggleAnim()
    {
       // if(Toggle == true)
        {
            //if(PlateAnim.GetBool("Activated") == false)
            //{
            //    PlateAnim.SetBool("Activated", true);
            //    PlateAnim.SetBool("Idle", true);
                
            //   PressureAudio.clip = Activate;
            //    PressureAudio.Play();
            //    print("Pressure Plate Triggered.");
            //}
        }
    }
}
