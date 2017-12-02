using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceHolder : MonoBehaviour {

    public GameObject Player;
    public string placeScene;
    public  bool Ethancheck;
    public Transform spawner1;
    public static bool Ethan2;
    public GameObject secondPlayer;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        DontDestroyOnLoad(transform.gameObject);
        Scene PlaceScene = SceneManager.GetActiveScene();
        string Scenename = PlaceScene.name; 
        if(Scenename == placeScene)
        {
            Player.SetActive(true);
        }
        if(Ethancheck == true)
        {
            Scene currentscene = SceneManager.GetActiveScene();
            string sceneName = currentscene.name;
            if (Ethan2 == false && sceneName == "Debug scene 2")
            {
                Instantiate(secondPlayer, spawner1.position, spawner1.rotation);
                Ethan2 = true;
            }
            if(sceneName == "Debug scene 1")
            {
                Ethan2 = false;
            }
        }

	}
}
