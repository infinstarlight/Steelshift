using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vertex : MonoBehaviour {

    public int id;
    public List<RectTransform.Edge> neighbours;
    [HideInInspector]
    public Vertex prev;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
