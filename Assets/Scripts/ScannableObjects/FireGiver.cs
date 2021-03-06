﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGiver : ScannableObject
{
    private ParticleSystem[] particleSystems;
    private ParticleSystem.MinMaxGradient[] originalColors;

    public override void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        originalColors = new ParticleSystem.MinMaxGradient[particleSystems.Length];

        for (int i = 0; i < originalColors.Length; i++)
        {
            var main = particleSystems[i].main;
            originalColors[i] = main.startColor;
        }
    }

    public override void OnScan()
    {
        inator.LoadInator(scannedName, this.gameObject, "Fire");
        base.OnScan();
    }

    public override void OnShot(Material material)
    {
        if (inator.type == Inator.InatorType.Material && material != null)
        {
            ParticleSystem[] _particleSystems = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem system in _particleSystems)
            {
                var main = system.main;
                main.startColor = material.color;
            }
        }
    }

    //Cannot spawn an object on a fire
    public override void OnShot(GameObject obj, string tag)
    {
        return;
    }

    //Fires have no mesh renderer
    public override void Reset()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            var main = particleSystems[i].main;
            main.startColor = originalColors[i];
        }

        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
            if (child.tag == "SpawnedLoadedObject")
                Destroy(child.gameObject);
    }

    //Transfer the fire when an object touches it
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ScannableObject scannable)
            && other.gameObject.tag != "Environment"
            && other.gameObject.tag != "Fruit")
        {
            scannable.SpawnFire(this.gameObject);
        }
    }
}