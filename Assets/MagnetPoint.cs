using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPoint : MonoBehaviour
{
    public float forceFactor = 200f;

    List<Rigidbody> magneticItems = new List<Rigidbody>();

    Transform magnetPoint;
    // Start is called before the first frame update
    void Start()
    {
        magnetPoint = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        foreach (Rigidbody magneticItem in magneticItems)
        {
            magneticItem.AddForce((magnetPoint.position - magneticItem.position) * forceFactor * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Magnetic"))
            magneticItems.Add(other.GetComponent<Rigidbody>());
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Magnetic"))
            magneticItems.Remove(other.GetComponent<Rigidbody>());
    }
}
