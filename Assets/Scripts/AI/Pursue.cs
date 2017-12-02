﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : Seek {

    public float maxPrediction;
    private GameObject targetAux;
    private Agent targetAgent;
    float speed;

    public override void Awake()
    {
        base.Awake();
        targetAgent = target.GetComponent<Agent>();
        targetAux = target;
        target = new GameObject();
    }

    private void OnDestroy()
    {
        Destroy(targetAux);
    }

    public override Steering GetSteering()
    {
        Vector3 direction = targetAux.transform.position - transform.position;
        float distance = direction.magnitude;
        float prediction;
        if (speed <= distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        else
            prediction = distance / speed;


        return base.GetSteering();
    }
}
