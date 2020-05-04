using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialGiver : ScannableObject
{
    private MeshRenderer meshRenderer;

    public override void Start()
    {
        base.Start();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public override void OnScan()
    {
        //name = name of material before a space
        inator.LoadInator(meshRenderer.material.name.Split(char.Parse(" "))[0], meshRenderer.material);
        base.OnScan();
    }
}