using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InatorController : MonoBehaviour
{
    public bool isGrabbing = false;
    public Inator inator;

    private XRController controller;
    private XRBaseInteractor interactor;
    private InputHelpers.Button[] scanButtons;
    private float activationThreshhold = 0.1f;

    void Start()
    {
        interactor = GetComponent<XRBaseInteractor>();
        controller = GetComponent<XRController>();
        scanButtons = new InputHelpers.Button[] { InputHelpers.Button.PrimaryButton, InputHelpers.Button.SecondaryButton };
    }

    void Update()
    {
        if (isGrabbing && interactor.selectTarget.gameObject == inator.gameObject)
        {
            InputHelpers.IsPressed(controller.inputDevice, scanButtons[0], out bool primaryActivated, activationThreshhold);
            InputHelpers.IsPressed(controller.inputDevice, scanButtons[1], out bool secondaryActivated, activationThreshhold);

            if (primaryActivated || secondaryActivated)
                inator.Scan();
        }
    }

    public void SetGrab(bool _isGrabbing) => isGrabbing = _isGrabbing;
}
