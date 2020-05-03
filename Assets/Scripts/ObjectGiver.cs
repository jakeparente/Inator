using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGiver : ScannableObject
{
    public override void OnScan()
    {
        inator.LoadInator(scannedName, this.gameObject);
        base.OnScan();
    }
}