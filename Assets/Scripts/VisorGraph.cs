using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisorGraph : MonoBehaviour {

    public int visionReach;
    public GameObject visorObj;
    public Graph visionGraph;

    // Use this for initialization
    void Start () {

        if(visorObj == null)
        {
            visorObj = gameObject;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerStay(Collider other)
    {
        string tag = other.gameObject.tag;
    }

    //public bool IsVisible(int[] visibilityNodes)
    //{
    //    int vision = visionReach;
    //    int src = visionGraph.GetNearestVertex(visorObj);
    //    HashSet<int> visibleNodes = new HashSet<int>();
    //    Queue<int> queue = new Queue<int>();
    //    queue.Enqueue(src);
    //}

 
}
