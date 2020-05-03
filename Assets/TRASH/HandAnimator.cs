using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandAnimator : MonoBehaviour
{
    private InputDevice device;
    private Animator handAnimator;
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        handAnimator = GetComponent<Animator>(); 
    }

    void Update()
    {
        //if()
    }
}
