using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGiver : ScannableObject
{
    public override void OnScan()
    {
        inator.LoadInator(scannedName);
        base.OnScan();
    }
}
