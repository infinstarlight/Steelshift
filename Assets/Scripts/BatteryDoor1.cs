using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BatteryDoor1 : MonoBehaviour {

    public GameObject BatterySpace; //Trigger Space for the button. Does Nothing.
    public GameObject BatteryItem; //Battery Object Goes Here!!
    public GameObject DoorObj1; //The Door its supposed to control!
    private bool BatteryGet = false;
    private bool allowBatt = false;
    private string SceneName; //Holder for current scene name.
    public Animator Battery1; //Battery Animation
    public Animator Door1; //Door Animation
    private Renderer DoorMesh; //Mesh of DoorObj
    private Collider DoorSolid;

	void Start () {
        Battery1 = BatteryItem.GetComponent<Animator>();
        Door1 = DoorObj1.GetComponent<Animator>();
        DoorMesh = DoorObj1.GetComponent<Renderer>();
        DoorSolid = DoorObj1.GetComponent<Collider>();
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneName = currentScene.name; //Returns the current scene.

        if (SceneName == "New Scene 2")
        {
            DoorMesh.enabled = false;
            DoorSolid.enabled = false;
        }

        else if (SceneName == "New Scene 1")
        {
            DoorMesh.enabled = true;
            DoorSolid.enabled = true;
        }

        if (allowBatt && SceneName == "New Scene 2")
        {
        }

        if (allowBatt && SceneName == "New Scene 1")
        {
            if (Input.GetKeyDown(KeyCode.E) && BatteryControl.Battery.Door1Battery == true)
            {
                Battery1.SetBool("BatteryGot", true);
                Door1.SetBool("BDoorAct", false);
                Debug.Log("BatteryTakeAway");
                BatteryControl.Battery.Door1Battery = false;
                BatteryControl.Battery.BatteryNum++;
                BatteryGet = true;
            }

            else if (Input.GetKeyDown(KeyCode.E) && BatteryControl.Battery.BatteryNum >=1 && BatteryControl.Battery.Door1Battery == false)
            {
                Battery1.SetBool("BatteryGot", false);
                Door1.SetBool("BDoorAct", true);
                Debug.Log("BatteryPutIn");
                BatteryControl.Battery.Door1Battery = true;
                BatteryControl.Battery.BatteryNum--;
                BatteryGet = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            allowBatt = true;
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        allowBatt = false;
    }
}
