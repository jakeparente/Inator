using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class TreasureChest : MonoBehaviour
{
    public GameObject top;
    public GameObject bottom;

    public GameObject prize;

    private void FixedUpdate()
    {
        if (top == null || bottom == null)
        {
            Destroy(this.gameObject);
            prize.SetActive(true);
        }
    }

    public void WinCondition()
    {
        if (top != null)
        {
            top.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
            prize.SetActive(true);
        }
    }

    internal void Open()
    {
        throw new NotImplementedException();
    }
}
