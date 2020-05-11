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
        GameObject tophat = Instantiate(obj, transform);
        tophat.transform.localPosition = topHatLocation;
        tophat.transform.localScale = scale;
        tophat.transform.localRotation = rotation;
        tophat.transform.tag = "SpawnedLoadedObject";

        //Remove Components
        Rigidbody rb = tophat.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.mass = 0;
        Destroy(tophat.GetComponent<XRGrabInteractable>());
    }
}
