using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCubesPuzzle : MonoBehaviour
{
    public GameObject[] cubes = new GameObject[6];
    public int piecesLeft = 6;

    public GameObject clue;

    public void CheckIfSolved()
    {
        if (piecesLeft <= 0)
        {
            clue.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
