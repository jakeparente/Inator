using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MaterialGiver
{
    //Just so you cant spawn an object in the middle of the floor
    public override void OnShot(GameObject obj)
    {
        return;
    }

    //So the floor can't be deleted
    public override void OnShot(string str)
    {
        Debug.Log(name + " shot");
        Debug.Log(str);
        if (str == "Eraser")
            return;
        else if (str == "Reset")
            Reset();
    }

    public override void OnScan()
    {
        Debug.Log(name + " scanned");
        base.OnScan();
    }
}
