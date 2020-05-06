using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGiver : ScannableObject
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
