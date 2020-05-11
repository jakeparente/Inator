using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFlame : ScannableObject
{
    public GameObject BlueTorchFlame;
    public GameObject RedTorchFlame;
    public GameObject GreenTorchFlame;
    public GameObject YellowTorchFlame;

    public Material Blue;
    public Material Red;
    public Material Green;
    public Material Yellow;
    public Material Black;

    public GameObject eraser;

    void Update()
    {
        CheckAllStates();
    }

    //Check each of the torch lights to see if they have a child object called fire and to see what colour the fire is.
    private void CheckAllStates()
    {
        if(VerifyCorrectColour(BlueTorchFlame, Blue)
            && VerifyCorrectColour(RedTorchFlame, Red)
            && VerifyCorrectColour(GreenTorchFlame, Green)
            && VerifyCorrectColour(YellowTorchFlame, Yellow))
        {
            MakeAllFlamesBlack();
        }
    }

    //Verifies that torch flame has the right colour.
    private bool VerifyCorrectColour(GameObject TorchFlame, Material material)
    {
        if(!HasFire(TorchFlame))
        {
            return false;
        }
        ParticleSystem[] particleSystems = TorchFlame.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem system in particleSystems)
        {
            var main = system.main;
            if (main.startColor.color == material.color)
            {
                return true;
            }
        }

        return false;
    }

    //Check the state of an individual flame to see if it is on fire and what colour it is.
    private bool HasFire(GameObject TorchFlame)
    {
        ParticleSystem[] particleSystems =  TorchFlame.GetComponentsInChildren<ParticleSystem>();
        if (particleSystems == null || particleSystems.Length == 0)
        {
            return false;
        }
        return true;
    }

    //Our flames will black out ur mum
    private void MakeAllFlamesBlack()
    {
        ParticleSystem[] particleSystemsBlue = BlueTorchFlame.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem system in particleSystemsBlue)
        {
            var main = system.main;
            main.startColor = Black.color;
        }

        ParticleSystem[] particleSystemsRed= RedTorchFlame.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem system in particleSystemsRed)
        {
            var main = system.main;
            main.startColor = Black.color;
        }

        ParticleSystem[] particleSystemsGreen = GreenTorchFlame.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem system in particleSystemsGreen)
        {
            var main = system.main;
            main.startColor = Black.color;
        }

        ParticleSystem[] particleSystemsYellow = YellowTorchFlame.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem system in particleSystemsYellow)
        {
            var main = system.main;
            main.startColor = Black.color;
        }

        //Spawn the eraser when the puzzle is solved
        eraser.SetActive(true);
    }

    //If the torches are destroyed we get a million errors lol
    public override void OnShot(string tag)
    {
        if (tag == "Reset")
            Reset();
    }
}
