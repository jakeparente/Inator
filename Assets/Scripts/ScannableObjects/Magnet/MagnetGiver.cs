using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGiver : ScannableObject
{
    public override void OnScan()
    {
        inator.LoadInator(scannedName, transform.GetChild(0).gameObject, "Magnet");
        base.OnScan();
    }
}
