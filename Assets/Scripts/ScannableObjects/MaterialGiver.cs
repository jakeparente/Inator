using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialGiver : ScannableObject
{
    [HideInInspector]
    public MeshRenderer meshRenderer;
    public Material backupMaterial;

    public override void Start()
    {
        base.Start();
        if (GetComponent<MeshRenderer>() != null)
            meshRenderer = GetComponent<MeshRenderer>();
    }

    public override void OnScan()
    {
        //name = name of material before a space
        if (meshRenderer)
            inator.LoadInator(meshRenderer.material.name.Split(char.Parse(" "))[0], meshRenderer.material);
        else
            inator.LoadInator(backupMaterial.name.Split(char.Parse(" "))[0], backupMaterial);
        base.OnScan();
    }
}