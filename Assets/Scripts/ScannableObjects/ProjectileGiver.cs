using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGiver : ScannableObject
{
    public override void OnScan()
    {
        inator.LoadInator(scannedName, this.gameObject, "Projectile");
        base.OnScan();
    }
}
