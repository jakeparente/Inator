using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class Inator : MonoBehaviour
{
    public enum InatorType { Empty, Material, Object, Eraser, Projectile, Reset, Effect }
    public InatorType type;

    public Material loadedMaterial = null;
    public GameObject loadedObject = null;

    public float range = 100f;
    public GameObject raycastStartPoint;

    [SerializeField]
    private TextMeshProUGUI inatorText;

    void Start()
    {
        inatorText = GetComponentInChildren<TextMeshProUGUI>();
        type = InatorType.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        //Fire1 and fire2 map to face buttons, need to be changed to actual vr inputs
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
            Scan();
    }

    public void ShootProjectile(GameObject loadedProjectile)
    {
        GameObject firedProjectile = Instantiate(loadedProjectile, raycastStartPoint.transform.position, raycastStartPoint.transform.rotation);
        firedProjectile.GetComponent<Rigidbody>().AddForce(raycastStartPoint.transform.forward * 500f);
        firedProjectile.GetComponent<Rigidbody>().useGravity = true;
        firedProjectile.transform.tag = "FiredProjectile";
    }

    public void Shoot()
    {
        if (type == InatorType.Projectile && loadedObject != null)
            ShootProjectile(loadedObject);

        else if (Physics.Raycast(raycastStartPoint.transform.position, raycastStartPoint.transform.forward, out RaycastHit hit, range)
            && hit.transform.gameObject.GetComponent<ScannableObject>() != null)
        {
            if (type == InatorType.Object || type == InatorType.Effect)
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
        if (Physics.Raycast(raycastStartPoint.transform.position, raycastStartPoint.transform.forward, out RaycastHit hit, range)
            && hit.transform.gameObject.GetComponent<ScannableObject>() != null)
        {
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

    //Load the Inator with an object, projectile, or effect
    public void LoadInator(string scannedName, GameObject scannedObject, bool projectileFlag)
    {
        //Bad way to do effects, i'll change it soon
        if (scannedName == "Fire")
            type = InatorType.Effect;
        else
            type = projectileFlag ? InatorType.Projectile : InatorType.Object;

        inatorText.text = scannedName + "-inator!";

        //Find any other loaded objects under the map and delete them
        GameObject[] loadedObjects = GameObject.FindGameObjectsWithTag("LoadedObjectClone");
        foreach (GameObject obj in loadedObjects)
        {
            Debug.Log(obj.name + " destroyed");
            Destroy(obj);
        }

        //Instantiate a clone of the object and spawn it under the map
        loadedObject = Instantiate(scannedObject);
        loadedObject.transform.position = new Vector3(0f, -5f, 0f);

        if (loadedObject.GetComponent<Rigidbody>() != null)
            loadedObject.GetComponent<Rigidbody>().useGravity = (type == InatorType.Projectile) ? true : false;
        loadedObject.transform.tag = "LoadedObjectClone";

        loadedMaterial = null;
    }
}