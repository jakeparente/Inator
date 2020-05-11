using System.Collections;
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
    public enum InatorType { Empty, Material, Fire, Tophat, Eraser, Projectile, Reset, Magnet }
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

        Rigidbody rb = firedProjectile.GetComponent<Rigidbody>();
        rb.AddForce(raycastStartPoint.transform.forward * 1000f);
        rb.useGravity = true;

        firedProjectile.transform.tag = "FiredProjectile";
    }

    public void Shoot()
    {
        audioSource.clip = shootClip;
        audioSource.Play();
        if (type == InatorType.Projectile && loadedObject != null)
            ShootProjectile(loadedObject);

        else if (Physics.Raycast(raycastStartPoint.transform.position, raycastStartPoint.transform.forward, out RaycastHit hit, range)
            && hit.transform.TryGetComponent(out ScannableObject scannable))
        {
            if (type == InatorType.Tophat || type == InatorType.Fire || type == InatorType.Magnet)
               scannable.OnShot(loadedObject, type.ToString());

            else if (type == InatorType.Eraser || type == InatorType.Reset)
                scannable.OnShot(type.ToString());

            else if (type == InatorType.Material)
                scannable.OnShot(loadedMaterial);
        }
    }

    public void Scan()
    {
        audioSource.clip = scanClip;
        audioSource.Play();
        if (Physics.Raycast(raycastStartPoint.transform.position, raycastStartPoint.transform.forward, out RaycastHit hit, range)
            && hit.transform.TryGetComponent(out ScannableObject scannable))
        {
            scannable.OnScan();
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

        if (loadedObject.TryGetComponent(out Rigidbody rb))
            rb.useGravity = (type == InatorType.Projectile) ? true : false;

        loadedObject.transform.tag = "LoadedObjectClone";
        loadedMaterial = null;
    }
}