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
    public virtual void OnShot(GameObject obj, string tag)
    {
        if (obj != null)
        {
            if (inator.type == Inator.InatorType.Tophat)
            {
                if (TryGetComponent(out TopHatLocation tophatLocation))
                    tophatLocation.SpawnTopHat(obj);
                else SpawnObject(obj);
            }
            else if (inator.type == Inator.InatorType.Fire)
                SpawnFire(obj);

            else if (inator.type == Inator.InatorType.Magnet)
                SpawnMagnet(obj);
        }
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
            if (TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.color = material.color;
                return;
            }
            MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer m in meshRenderers)
                m.material = material;
        }
    }

    //Spawn Magnet
    public void SpawnMagnet(GameObject loadedObject)
    {
        //Instantiate Object
        GameObject spawnedObject = Instantiate(loadedObject, this.transform);
        spawnedObject.transform.tag = "SpawnedLoadedObject";
        spawnedObject.transform.localPosition = Vector3.zero;

        transform.tag = (transform.tag == "Magnetic") ? transform.tag = "Untagged" : transform.tag;  
    }

    //Set object on fire
    public void SpawnFire(GameObject fire)
    {
        //Check if object already has fire
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
            if (child.transform.tag == "SpawnedFire")
                return;

        //Instantiate fire
        GameObject spawnedFire = Instantiate(fire, this.transform);
        spawnedFire.transform.tag = "SpawnedFire";
        spawnedFire.transform.localPosition = Vector3.zero;
    }

    //Spawn object or effect
    public void SpawnObject(GameObject loadedObject)
    {

        //Instantiate Object
        GameObject spawnedObject = Instantiate(loadedObject, this.transform);
        spawnedObject.transform.tag = "SpawnedLoadedObject";

        //Reset size and position
        spawnedObject.transform.localScale = new Vector3(GetHeight(), GetHeight(), GetHeight());
        spawnedObject.transform.localPosition = Vector3.zero + (new Vector3(0f, GetHeight(), 0f));
        spawnedObject.transform.localRotation = Quaternion.identity;

        Destroy(spawnedObject.GetComponent<XRGrabInteractable>());

        //Remove some rigidbody components
        Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.mass = 0;
    }

    //Really hard to predict right now, different for each object
    //Might have to find a better way to do this
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

        if (TryGetComponent(out SpriteRenderer spriteRenderer))
            spriteRenderer.color = GetComponent<MaterialGiver>().backupMaterial.color;

        //Destroy spawned objects and effects
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.tag == "SpawnedLoadedObject" || child.tag == "SpawnedFire")
                Destroy(child.gameObject);
        }
    }
}