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

    public override void OnScan()
    {
        inator.LoadInator(scannedName, this.gameObject, false);
        base.OnScan();
    }
}
