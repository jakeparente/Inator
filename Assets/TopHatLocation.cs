using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TopHatLocation : MonoBehaviour
{
    public Vector3 topHatLocation;
    public Vector3 scale;
    public Quaternion rotation = Quaternion.identity;

    public void SpawnTopHat(GameObject obj)
    {
        //Instantiate tophat
        GameObject topHat = Instantiate(obj, transform);
        topHat.transform.localPosition = topHatLocation;
        topHat.transform.localScale = scale;
        topHat.transform.localRotation = rotation;

        //Remove Components
        topHat.GetComponent<Rigidbody>().isKinematic = true;
        topHat.GetComponent<Rigidbody>().mass = 0;
        Destroy(topHat.GetComponent<XRGrabInteractable>());
    }
}
