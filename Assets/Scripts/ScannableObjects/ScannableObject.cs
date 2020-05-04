using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScannableObject : MonoBehaviour
{
    public string scannedName;
    public Inator inator;

    //Tracked for Reset()
    private MeshRenderer[] meshRenderers;
    private Material[] originalMaterials;
    private GameObject[] attatchedSpawnedObjects;

    public virtual void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        originalMaterials = new Material[meshRenderers.Length];
        for (int i = 0; i < meshRenderers.Length; i++)
            originalMaterials[i] = meshRenderers[i].material;
    }

    //Detect when an object is scanned
    public virtual void OnScan()
    {

    }

    //Shot with an object
    public virtual void OnShot(GameObject obj)
    {
        if (inator.type == Inator.InatorType.Object && obj != null)
            SpawnObjectOnTop(obj);
    }

    //Shot with something represented by a string
    public virtual void OnShot(string tag)
    {
        if (tag == "Eraser")
            Destroy(this.gameObject);
        else if (tag == "Reset")
            Reset();
    }

    //Shot with a material
    public virtual void OnShot(Material material)
    {
        if (inator.type == Inator.InatorType.Material && material != null)
        {
            MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer m in meshRenderers)
                m.material = material;
        }
    }

    private void SpawnObjectOnTop(GameObject loadedObject)
    {
        //Instantiate Object
        GameObject spawnedObject = Instantiate(loadedObject, this.transform);
        spawnedObject.transform.tag = "SpawnedLoadedObject";

        //Reset size and position
        spawnedObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        spawnedObject.transform.localPosition = Vector3.zero + (new Vector3(0f, 0.6f + (spawnedObject.transform.localScale.y / 2), 0f));
        spawnedObject.transform.localRotation = Quaternion.identity;

        //Remove components
        spawnedObject.GetComponent<Rigidbody>().isKinematic = true;
        spawnedObject.GetComponent<Rigidbody>().mass = 0;
        Destroy(spawnedObject.GetComponent<XRGrabInteractable>());
    }

    //Return object to original state when shot by the reset-inator
    public void Reset()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
            meshRenderers[i].material = originalMaterials[i];

        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
            if (child.tag == "SpawnedLoadedObject")
                Destroy(child.gameObject);
    }
}