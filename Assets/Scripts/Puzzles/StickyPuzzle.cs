using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StickyPuzzle : MaterialGiver
{
    public GameObject tophat;
    public GameObject banana;

    public TextMeshProUGUI fancyText;
    public TextMeshProUGUI bananaText;
    public TextMeshProUGUI yellowText;

    public override void OnShot(GameObject obj)
    {
        if (inator.type == Inator.InatorType.Object && obj != null)
        {
            if (obj.name.Split(char.Parse("("))[0] == "TopHat")
            {
                tophat.SetActive(true);
                fancyText.gameObject.SetActive(false);
                bananaText.gameObject.SetActive(true);
            }
            else if (obj.name.Split(char.Parse("("))[0] == "Banana")
            {
                banana.SetActive(true);
                fancyText.gameObject.SetActive(false);
                bananaText.gameObject.SetActive(false);
                yellowText.gameObject.SetActive(true);
            }
            else
                OnShot(obj);
        }
        else
            OnShot(obj);
    }
}
