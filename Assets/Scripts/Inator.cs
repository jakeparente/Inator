﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using UnityEngine.Audio;

public class Inator : MonoBehaviour
{
    public enum InatorType { Empty, Material, Object, Eraser, Projectile, Reset, Effect, Magnet }
    public InatorType type;

    public Material loadedMaterial = null;
    public GameObject loadedObject = null;

    public float range = 100f;
    public GameObject raycastStartPoint;

    [SerializeField]
    private TextMeshProUGUI inatorText;

    [HideInInspector]
    public AudioSource audioSource;

    public AudioClip scanClip;
    public AudioClip shootClip;

    void Start()
    {
        inatorText = GetComponentInChildren<TextMeshProUGUI>();
        type = InatorType.Empty;
        audioSource = GetComponent<AudioSource>();
    }

    public void ShootProjectile(GameObject loadedProjectile)
    {
        GameObject firedProjectile = Instantiate(loadedProjectile, raycastStartPoint.transform.position, raycastStartPoint.transform.rotation);
        firedProjectile.GetComponent<Rigidbody>().AddForce(raycastStartPoint.transform.forward * 1000f);
        firedProjectile.GetComponent<Rigidbody>().useGravity = true;
        firedProjectile.transform.tag = "FiredProjectile";
    }

    public void Shoot()
    {
        audioSource.clip = shootClip;
        audioSource.Play();
        if (type == InatorType.Projectile && loadedObject != null)
            ShootProjectile(loadedObject);

        else if (Physics.Raycast(raycastStartPoint.transform.position, raycastStartPoint.transform.forward, out RaycastHit hit, range)
            && hit.transform.gameObject.GetComponent<ScannableObject>() != null)
        {
            if (type == InatorType.Object || type == InatorType.Effect || type == InatorType.Magnet)
                hit.transform.gameObject.GetComponent<ScannableObject>().OnShot(loadedObject);
            else if (type == InatorType.Material)
                hit.transform.gameObject.GetComponent<ScannableObject>().OnShot(loadedMaterial);
            else if (type == InatorType.Eraser)
                hit.transform.gameObject.GetComponent<ScannableObject>().OnShot("Eraser");
            else if (type == InatorType.Reset)
                hit.transform.gameObject.GetComponent<ScannableObject>().OnShot("Reset");
        }
    }

    public void Scan()
    {
        audioSource.clip = scanClip;
        audioSource.Play();
        if (Physics.Raycast(raycastStartPoint.transform.position, raycastStartPoint.transform.forward, out RaycastHit hit, range)
            && hit.transform.gameObject.GetComponent<ScannableObject>() != null)
        {
            if (hit.transform.gameObject.name == "Red Cube")
                Debug.Log(hit.transform.gameObject.name);
            hit.transform.gameObject.GetComponent<ScannableObject>().OnScan();
        }
    }

    //Load the Inator with a name
    public void LoadInator(string scannedName)
    {
        inatorText.text = scannedName + "-inator!";

        if (scannedName == "Eraser")
            type = InatorType.Eraser;
        else if (scannedName == "Reset")
            type = InatorType.Reset;

        loadedMaterial = null;
        loadedObject = null;
    }

    //Load the Inator with a material
    public void LoadInator(string scannedName, Material scannedMaterial)
    {
        type = InatorType.Material;
        inatorText.text = scannedName + "-inator!";
        loadedMaterial = scannedMaterial;

        loadedObject = null;
    }

    //Load the Inator with an object, projectile, effect, or magnet
    public void LoadInator(string scannedName, GameObject scannedObject, string tag)
    {
        //Convert tag to enum
        type = (InatorType)System.Enum.Parse(typeof(InatorType), tag);

        inatorText.text = scannedName + "-inator!";

        //Find any other loaded objects under the map and delete them
        GameObject[] loadedObjects = GameObject.FindGameObjectsWithTag("LoadedObjectClone");
        foreach (GameObject obj in loadedObjects)
        {
            Destroy(obj);
        }

        //Instantiate a clone of the object and spawn it under the map
        loadedObject = Instantiate(scannedObject);
        loadedObject.transform.position = new Vector3(0f, -10f, 0f);

        if (loadedObject.GetComponent<Rigidbody>() != null)
            loadedObject.GetComponent<Rigidbody>().useGravity = (type == InatorType.Projectile) ? true : false;

        loadedObject.transform.tag = "LoadedObjectClone";
        loadedMaterial = null;
    }
}