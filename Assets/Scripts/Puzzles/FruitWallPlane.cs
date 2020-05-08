using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitWallPlane : MonoBehaviour
{
    public string correctFruit;
    public Material correctMaterial;

    private WallFruitPuzzle puzzle;

    private void Start()
    {
        puzzle = GetComponentInParent<WallFruitPuzzle>();
        //StartCoroutine(TestMode());
    }

    private IEnumerator TestMode()
    {
        yield return new WaitForSecondsRealtime(4f);
        puzzle.PlaneSolved();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy fired object
        if (other.transform.tag == "FiredProjectile")
            Destroy(other.gameObject);

        //Check if already solved
        if (GetComponent<MeshRenderer>().material == correctMaterial)
            return;

        if (other.gameObject.GetComponent<MeshRenderer>())
        {
            if ((other.gameObject.name.Split(char.Parse(" "))[0] == correctFruit
                || other.gameObject.name.Split(char.Parse("("))[0] == correctFruit)
                && other.gameObject.GetComponent<MeshRenderer>().material.name.Split(char.Parse(" "))[0] == correctMaterial.name)
            {
                GetComponent<MeshRenderer>().material = correctMaterial;
                puzzle.PlaneSolved();
            }
        }
    }
}
