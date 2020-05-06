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
            SpawnObject(obj, false);
        else if (inator.type == Inator.InatorType.Effect && obj != null)
            SpawnObject(obj, true);
        else if (inator.type == Inator.InatorType.Magnet && obj != null)
            SpawnMagnet(obj);
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
            if (GetComponent<SpriteRenderer>())
            {
                GetComponent<SpriteRenderer>().color = material.color;
                return;
            }
            MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer m in meshRenderers)
                m.material = material;
        }
    }

    //Spawn Magnet
    private void SpawnMagnet(GameObject loadedObject)
    {
        //Instantiate Object
        GameObject spawnedObject = Instantiate(loadedObject, this.transform);
        spawnedObject.transform.tag = "SpawnedLoadedObject";
        spawnedObject.transform.localPosition = Vector3.zero;

        transform.tag = (transform.tag == "Magnetic") ? transform.tag = "Untagged" : transform.tag;
        
    }

    //Spawn object or effect
    public void SpawnObject(GameObject loadedObject, bool isEffect)
    {
        if (isEffect)
        {
            Transform[] children = GetComponentsInChildren<Transform>();
            foreach (Transform child in children)
                if (child.transform.tag == "SpawnedLoadedEffect")
                    return;
        }

        //Instantiate Object
        GameObject spawnedObject = Instantiate(loadedObject, this.transform);
        spawnedObject.transform.tag = isEffect ? "SpawnedLoadedEffect" : "SpawnedLoadedObject";

        //Reset size and position
        if (isEffect)
            spawnedObject.transform.localPosition = Vector3.zero;
        else
        {
            spawnedObject.transform.localScale = new Vector3(GetHeight(), GetHeight(), GetHeight());
            spawnedObject.transform.localPosition = Vector3.zero + (new Vector3(0f, GetHeight(), 0f));
            spawnedObject.GetComponent<Rigidbody>().isKinematic = true;
            spawnedObject.GetComponent<Rigidbody>().mass = 0;
            Destroy(spawnedObject.GetComponent<XRGrabInteractable>());
        }
        spawnedObject.transform.localRotation = Quaternion.identity;
    }

    private float GetHeight()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        float max = 0f;

        foreach (Collider collider in colliders)
        {
            if (collider.bounds.size.y >= max)
                max = collider.bounds.size.y;
        }
        return max / transform.localScale.y;
    }

    //Return object to original state when shot by the reset-inator
    public virtual void Reset()
    {
        if (meshRenderers.Length != 0)
        {
            for (int i = 0; i < meshRenderers.Length; i++)
                meshRenderers[i].material = originalMaterials[i];
        }

        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
            if (child.tag == "SpawnedLoadedObject" || child.tag == "SpawnedLoadedEffect")
                Destroy(child.gameObject);
    }
}