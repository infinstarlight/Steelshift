using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeShift : MonoBehaviour {

    //public GameObject Button;
    private bool timeShift = false;
    private bool sceneLoad = false;
    private Animator shifTing;
    public static bool grow = false;
    string currentScene;
    public AudioClip Activate;

    private AudioSource aSource;


	// Use this for initialization
	void Start () {
        
        shifTing = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        currentScene = SceneManager.GetActiveScene().name;
        if (Input.GetKeyDown(KeyCode.E) && currentScene == "Debug scene 1")
        {
            SceneManager.LoadScene("Debug scene 2");
            aSource.PlayOneShot(Activate);
        }
        if (Input.GetKeyDown(KeyCode.E) && currentScene == "Debug scene 2")
        {
            SceneManager.LoadScene("Debug scene 1");
            aSource.PlayOneShot(Activate);
        }
        planeShift();
        loadScene1();
	}


    void planeShift()
    {
        if (timeShift == true && Input.GetKeyUp(KeyCode.E))
        {
            if(shifTing.GetBool ("Moving") == false)
            {
                shifTing.SetBool("Moving", true);
            }
            else
            {
                shifTing.SetBool("Moving", false);
            }
        }
    }

    void loadScene1()
    {
        if (sceneLoad == true&& Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Debug scene 1", LoadSceneMode.Additive);
        }
        
    }

}
