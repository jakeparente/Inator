using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCubesPuzzle : MonoBehaviour
{
    public GameObject[] cubes = new GameObject[6];
    public int piecesLeft = 6;

    public void CheckIfSolved()
    {
        if (piecesLeft <= 0)
        {
            Debug.Log("DING DING DING");
            Destroy(this.gameObject);
        }
    }
}
