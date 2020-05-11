using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPortrait : MaterialGiver
{
    public override void OnScan()
    {
        inator.LoadInator(backupMaterial.name.Split(char.Parse(" "))[0], backupMaterial);
    }
}
