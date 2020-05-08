using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFruitPuzzle : MonoBehaviour
{
    public GameObject fire;

    [SerializeField]
    private int planesToSolve = 4;

    public void PlaneSolved()
    {
        planesToSolve--;

        if (planesToSolve <= 0)
            fire.SetActive(true);
    }
}
