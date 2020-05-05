using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCubesPuzzleHelper : MaterialGiver
{
    public Material correctMaterial;
    public WallCubesPuzzle puzzleManager;

    public override void Start()
    {
        base.Start();
        puzzleManager = GetComponentInParent<WallCubesPuzzle>();
    }

    public override void OnShot(Material material)
    {
        string previousMaterial = meshRenderer.material.name.Split(char.Parse(" "))[0];
        base.OnShot(material);
        string currentMaterial = meshRenderer.material.name.Split(char.Parse(" "))[0];

        if (currentMaterial != previousMaterial)
        {
            if (currentMaterial == correctMaterial.name)
            {
                puzzleManager.piecesLeft--;
                puzzleManager.CheckIfSolved();
            }
            else if (previousMaterial == correctMaterial.name)
                puzzleManager.piecesLeft++;
        }
    }
}
