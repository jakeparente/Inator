using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MaterialGiver
{
    public override void OnShot(Material material)
    {
        transform.GetChild(0).GetComponent<Light>().color = material.color;
        base.OnShot(material);
    }
}
