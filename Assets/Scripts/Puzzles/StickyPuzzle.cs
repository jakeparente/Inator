using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPuzzle : MaterialGiver
{
    public GameObject tophat;
    public GameObject banana;

    public override void OnShot(GameObject obj)
    {
        if (inator.type == Inator.InatorType.Object && obj != null)
        {
            if (obj.name.Split(char.Parse("("))[0] == "TopHat")
                tophat.SetActive(true);
            else if (obj.name.Split(char.Parse("("))[0] == "Banana")
                banana.SetActive(true);
            else
                SpawnObject(obj, false);
        }
        else if (inator.type == Inator.InatorType.Effect && obj != null)
            SpawnObject(obj, true);
    }
}
