using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class InatorController : MonoBehaviour
{
    public bool isGrabbing = false;
    public Inator inator;
    public GameObject handModel;

    private XRController controller;
    private XRBaseInteractor interactor;
    private InputHelpers.Button[] scanButtons;

    void Start()
    {
        interactor = GetComponent<XRBaseInteractor>();
        controller = GetComponent<XRController>();
        scanButtons = new InputHelpers.Button[] { InputHelpers.Button.PrimaryButton, InputHelpers.Button.SecondaryButton };

        handModel = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (isGrabbing && interactor.selectTarget.gameObject == inator.gameObject)
        {
            InputHelpers.IsPressed(controller.inputDevice, scanButtons[0], out bool primaryActivated, 0.1f);
            InputHelpers.IsPressed(controller.inputDevice, scanButtons[1], out bool secondaryActivated, 0.1f);

            if (primaryActivated || secondaryActivated)
            {
                controller.SendHapticImpulse(0.2f, 0.1f);
                inator.Scan();
            }

            InputHelpers.IsPressed(controller.inputDevice, InputHelpers.Button.Trigger, out bool triggerActivated, 0.1f);
            if (triggerActivated)
                controller.SendHapticImpulse(0.4f, 0.1f);
        }
    }

    public void SetGrab(bool _isGrabbing)
    {
        isGrabbing = _isGrabbing;
        handModel.SetActive(!_isGrabbing);
    }
}
