using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Unity.VisualScripting;
using UnityEngine;

public class GrabCubeDetection : MonoBehaviour
{
    private enum BarrelType
    {
        NONE,
        RED,
        PURPLE
    }

    [SerializeField] private BarrelType barrelType = BarrelType.NONE;
    private HandGrabInteractable handGrabInteraction;
    private Material material;

    // Emission intensity values for different states
    private float normalEmissionIntensity = 1f; // No emission when normal
    private float hoverEmissionIntensity = 2.5f; // Low emission when hovered
    private float selectEmissionIntensity = 5f; // High emission when selected

    private void Start()
    {
        handGrabInteraction = GetComponentInChildren<HandGrabInteractable>();
        material = GetComponent<Renderer>().material;
        handGrabInteraction.WhenStateChanged += OnStateChanged;

        SetEmissionColor(normalEmissionIntensity);

    }

    private void OnStateChanged(InteractableStateChangeArgs args)
    {
        if (args.NewState == InteractableState.Select)
        {
            Debug.Log("Cube grabbed");
            SetEmissionColor(selectEmissionIntensity);
        }
        else if (args.NewState == InteractableState.Normal)
        {
            Debug.Log("Cube ungrabbed");
            SetEmissionColor(normalEmissionIntensity);
        }
        else if (args.NewState == InteractableState.Hover)
        {
            Debug.Log("Cube hovered");
            SetEmissionColor(hoverEmissionIntensity);
        }
    }

    private void SetEmissionColor(float intensity)
    {
        Color color = Color.white;
        if (barrelType == BarrelType.RED)
        {
            color = Color.red;
        }
        else if (barrelType == BarrelType.PURPLE)
        {
            color = new Color(0.5f, 0, 0.5f);
        }
        // Adjust the color's brightness based on the intensity
        Color finalColor = color * (intensity);
        material.SetColor("_EmissionColor", finalColor);

        // Update the global illumination (GI) to reflect the emission change
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            RendererExtensions.UpdateGIMaterials(renderer);
        }
    }

    [ContextMenu("CheckHover")]
    public void CheckHover()
    {
        SetEmissionColor(hoverEmissionIntensity);
    }

    [ContextMenu("CheckSelect")]
    public void CheckSelect()
    {
        SetEmissionColor(selectEmissionIntensity);
    }

    [ContextMenu("CheckNormal")]
    public void CheckNormal()
    {
        SetEmissionColor(normalEmissionIntensity);
    }

}