using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    public AudioClip Activate;
    public AudioClip Deactivate;
    public Light GeneratorLight;
    public Animator PlateAnim;

    public Color Disabled;
    public Color Activated;

    private AudioSource PressureAudio;
    private bool Toggle = false;
    

	// Use this for initialization
	void Start () {

        PressureAudio = GetComponent<AudioSource>();
        PlateAnim = GetComponent<Animator>();
        GeneratorLight.color = Disabled;
		
	}
	
	// Update is called once per frame
	void Update () {

        ToggleAnim();

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Phys")
        {
            Toggle = true;
            
        }
    }

    void ToggleAnim()
    {
        if(Toggle == true)
        {
            if(PlateAnim.GetBool("Activated") == false)
            {
                PlateAnim.SetBool("Activated", true);
                PlateAnim.SetBool("Idle", true);
                GeneratorLight.color = Activated;
               PressureAudio.clip = Activate;
                PressureAudio.Play();
                print("Pressure Plate Triggered.");
            }
            //else
            //{
            //    PlateAnim.SetBool("Activated", false);
            //    PlateAnim.SetBool("Idle", false);
            //    PlateAnim.SetBool("Deactivated", true);
            ////    PressureAudio.clip = Deactivate;
            ////    PressureAudio.Play();
            //    print("Pressure Plate Reset.");
            //}
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    Toggle = false;
    //    print("the Player has exited the collider");
    //}
}
