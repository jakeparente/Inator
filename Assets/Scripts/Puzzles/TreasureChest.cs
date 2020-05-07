using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class TreasureChest : MonoBehaviour
{
    public XRGrabInteractable lidGrabInteractable;

    public void OnWinCondition()
    {
        lidGrabInteractable.enabled = true;
    }
}
