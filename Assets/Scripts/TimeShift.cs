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
   // string CurrentScene;
    public AudioClip Activate;
    public static bool Ethan1;
    public static bool Ethan2;
    public GameObject firstPlayer;
    public GameObject secondPlayer;
    private AudioSource aSource;
    public Transform spawner1;

    // Use this for initialization
    void Start () {
        
        shifTing = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
       
	}

    // Update is called once per frame
    void Update () {
        Scene currentscene = SceneManager.GetActiveScene();
        string sceneName = currentscene.name;
        if(Ethan1 == false && sceneName == "New Scene 1")
        {
            Instantiate(firstPlayer, transform.position, transform.rotation);
            Ethan1 = true;
            Ethan2 = false;
        }
       // if(Ethan2 == false && sceneName == "Debug scene 2")
        //{
          //  Instantiate(secondPlayer, spawner1.position, spawner1.rotation);
            //Ethan2 = true;
        //}
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
            SceneManager.LoadScene("New Scene 1", LoadSceneMode.Additive);
        }
        
    }

}
