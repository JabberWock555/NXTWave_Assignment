using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class GrabCubeDetection : MonoBehaviour
{
    private HandGrabInteractable handGrabInteraction;
    private Material material;

    private void Start()
    {
        handGrabInteraction = GetComponentInChildren<HandGrabInteractable>();
        material = GetComponent<Renderer>().material;
        handGrabInteraction.WhenStateChanged += OnStateChanged;
    }

    private void OnStateChanged(InteractableStateChangeArgs args)
    {
        if (args.NewState == InteractableState.Select)
        {
            Debug.Log("Cube grabbed");
            material.color = Color.green;
        }
        else if (args.NewState == InteractableState.Normal)
        {
            Debug.Log("Cube ungrabbed");
            material.color = Color.red;
        }
        else if (args.NewState == InteractableState.Hover)
        {
            Debug.Log("Cube hovered");
            material.color = Color.yellow;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //DebugLogger.Log(other.gameObject.name);
        other.gameObject.tag = "Hand";
    }


}
