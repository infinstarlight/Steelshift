using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BatteryDoor2 : MonoBehaviour {

    public GameObject BatterySpace;
    public GameObject BatteryItem;
    public GameObject DoorObj2;
    public GameObject EndDoor;//End Door
    private bool BatteryGet = false;
    private bool allowBatt = false;
    private bool FirstTime = true;
    private string SceneName;
    public Animator Battery2;
    public Animator Door2; 
    private Renderer DoorMesh;
    private Collider DoorSolid;
    private Renderer DoorMesh2;
    private Collider DoorSolid2;

	// Use this for initialization
	void Start () {
        Battery2 = BatteryItem.GetComponent<Animator>();
        Door2 = DoorObj2.GetComponent<Animator>();
        DoorMesh = DoorObj2.GetComponent<Renderer>();
        DoorSolid = DoorObj2.GetComponent<Collider>();
        DoorMesh2 = EndDoor.GetComponent<Renderer>();
        DoorSolid2 = EndDoor.GetComponent<Collider>();

        if (FirstTime)
        {
            BatteryControl.Battery.BatteryNum++;
            FirstTime = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneName = currentScene.name;


        if (SceneName == "New Scene 1")
        {
            DoorMesh.enabled = false;
            DoorSolid.enabled = false;
            DoorMesh2.enabled = false;
            DoorSolid2.enabled = false;

        }

        else if (SceneName == "New Scene 2")
        {
            DoorMesh.enabled = true;
            DoorSolid.enabled = true;
            DoorMesh2.enabled = true;
            DoorSolid2.enabled = true;
        }

        if (allowBatt && SceneName == "New Scene 1")
        {
            
        }

        if (allowBatt && SceneName == "New Scene 2")
        {
            if (Input.GetKeyDown(KeyCode.E) && BatteryControl.Battery.Door2Battery == true)
            {
                Battery2.SetBool("BatteryGot", true);
                Door2.SetBool("BDoorAct", true);
                Debug.Log("BatteryTakeAway");
                BatteryControl.Battery.Door2Battery = false;
                BatteryControl.Battery.BatteryNum++;
                BatteryGet = true;
            }

            else if (Input.GetKeyDown(KeyCode.E) && BatteryControl.Battery.BatteryNum >= 1 && BatteryControl.Battery.Door2Battery == false)
            {
                Battery2.SetBool("BatteryGot", false);
                Door2.SetBool("BDoorAct", false);
                Debug.Log("BatteryPutIn");
                BatteryControl.Battery.Door2Battery = true;
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
