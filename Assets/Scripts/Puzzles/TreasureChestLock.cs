using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestLock : ScannableObject
{
    public TreasureChest chest;

    public override void OnShot(GameObject obj)
    {
        return;
    }

    public override void OnShot(Material material)
    {
        return;
    }

    public override void OnShot(string tag)
    {
        if (tag == "Eraser")
            Destroy(this.gameObject);
        else
            return;
    }

    private void OnDestroy()
    {
        chest.WinCondition();
    }
}
