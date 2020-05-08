using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestLock : ScannableObject
{
    public TreasureChest chest;
    public GameObject text;

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
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        
        foreach(GameObject fruit in fruits)
        {
            fruit.transform.tag = "Magnetic";
        }
        text.SetActive(true);
        chest.WinCondition();
    }
}
