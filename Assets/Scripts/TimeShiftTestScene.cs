using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeShiftTestScene : MonoBehaviour {

    //public GameObject Button;
    private bool timeShift = false;
    private bool sceneLoad = false;
    private Animator shifTing;
    public static bool grow = false;
    string currentScene;
    public AudioClip Activate;

    private AudioSource aSource;
    public GameObject playerPlayer;
    private Vector3 playerPosition;
    private Quaternion playerRotation;

    bool TimeShifted = false;

	// Use this for initialization
	void Start () {

        Debug.Log("Start!");
        
        shifTing = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        playerPlayer = GameObject.FindGameObjectWithTag("Player");
        currentScene = SceneManager.GetActiveScene().name;
        if(TimeShifted)
        {
            playerPlayer.transform.position = playerPosition;
            playerPlayer.transform.rotation = playerRotation;
            TimeShifted = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && currentScene == "TestScene1")
        {
            playerPosition = playerPlayer.transform.position;
            playerRotation = playerPlayer.transform.rotation;
            SceneManager.LoadScene("TestScene2");
            TimeShifted = true;
            aSource.PlayOneShot(Activate);
        }

        if (Input.GetKeyDown(KeyCode.E) && currentScene == "TestScene2")
        {
            playerPosition = playerPlayer.transform.position;
            playerRotation = playerPlayer.transform.rotation;
            SceneManager.LoadScene("TestScene1");
            TimeShifted = true;
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
        if (sceneLoad == true && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("TestScene1", LoadSceneMode.Additive);
        }
        
    }

}
