using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyTophatPuzzle : MaterialGiver
{
    public Material successMaterial;
    public GameObject clue;

    public override void OnShot(GameObject obj)
    {
        if (inator.type == Inator.InatorType.Object && obj != null)
            PuzzleSuccess();
        base.OnShot(obj);
    }

    private void PuzzleSuccess()
    {
        gameObject.GetComponent<SpriteRenderer>().color = successMaterial.color;
        backupMaterial = successMaterial;

        clue.SetActive(true);
    }
}
