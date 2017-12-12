using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryControl : MonoBehaviour {

    public static BatteryControl Battery;

    public int BatteryNum;
    public bool Door1Battery = false;
    public bool Door2Battery = true;

    void Awake()
    {
        if (Battery == null)
        {
            DontDestroyOnLoad(gameObject);
            Battery = this;
        }

        else if (Battery != this)
        {
            Destroy(gameObject);
        }
    }
}
