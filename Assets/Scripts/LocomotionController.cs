using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public XRController leftTeleportRay;
    public XRController leftHandController;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshhold = 0.1f;

    // Update is called once per frame
    void Update()
    {
        leftTeleportRay.gameObject.SetActive(CheckIfActivated(leftTeleportRay)); 
        leftHandController.gameObject.SetActive(!CheckIfActivated(leftTeleportRay));
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshhold);
        return isActivated;
    }
}
