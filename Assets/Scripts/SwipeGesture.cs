using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeGesture : MonoBehaviour
{
    public GameObject objectToRotate; // The object to rotate

    private Collider swipeTrackingRegion; // The collider defining the swipe tracking region
    private Material material;
    private Vector3 initialPosition;
    private Vector3 finalPosition;
    private bool isSwiping = false;
    private float swipeThreshold = 0.01f; // Minimum swipe distance (meters)
    [SerializeField] private float rotationSpeed = 100f; // Rotation speed for the object
    private bool swipeRegistered = false; // Ensures a swipe is registered only once per gesture

    void Start()
    {
        swipeTrackingRegion = GetComponent<Collider>();
        material = objectToRotate.GetComponent<Renderer>().material;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand")) // Assuming the hand is tagged as "Hand"
        {
            isSwiping = true;
            swipeRegistered = false; // Reset the swipe flag
            initialPosition = other.transform.position; // Start tracking the swipe
            material.color = Color.magenta;
        }
    }

    void OnTriggerStay(Collider other)
    {
        material.color = Color.cyan;
        if (isSwiping && other.CompareTag("Hand") && !swipeRegistered)
        {
            finalPosition = other.transform.position; // Update the final position
            Vector3 delta = finalPosition - initialPosition;

            if (delta.magnitude > swipeThreshold) // Check if swipe distance exceeds threshold
            {
                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y)) // Horizontal swipe
                {
                    if (delta.x < 0) // Left swipe
                    {
                        RotateObjectLeft();
                    }
                    else // Right swipe
                    {
                        RotateObjectRight();
                    }
                }
                else // Vertical swipe
                {
                    if (delta.y > 0) // Up swipe
                    {
                        RotateObjectUp();
                    }
                }

                swipeRegistered = true; // Prevent multiple swipes during the same gesture
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            isSwiping = false; // Stop tracking the swipe
            material.color = Color.grey;
        }
    }

    void RotateObjectLeft()
    {
        Debug.Log("Swipe Left - Rotating Object by 90 Degrees");
        objectToRotate.transform.Rotate(Vector3.up, -90f); // Rotate 90 degrees to the left
    }

    void RotateObjectRight()
    {
        Debug.Log("Swipe Right - Rotating Object by 90 Degrees");
        objectToRotate.transform.Rotate(Vector3.up, 90f); // Rotate 90 degrees to the right
    }

    void RotateObjectUp()
    {
        Debug.Log("Swipe Up - Rotating Object by 90 Degrees");
        objectToRotate.transform.Rotate(Vector3.right, 90f); // Rotate 90 degrees upward
    }

}
/* 
    void Start()
    {
        swipeTrackingRegion = GetComponent<Collider>();
        material = objectToRotate.GetComponent<Renderer>().material;
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Hand")) // Assuming the hand is tagged as "Hand"
        {
            StartSwipeGesture(other);
            material.color = Color.magenta;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand")) // Continue detecting gestures while the hand is in the collider
        {
            DetectSwipeGesture(other);
            material.color = Color.cyan;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand")) // Reset when the hand leaves the tracking region
        {
            ResetSwipeGesture();
            material.color = Color.grey;
        }
    }

    void StartSwipeGesture(Collider handCollider)
    {
        // Start tracking the swipe when the hand enters the region
        initialPosition = handCollider.transform.position;
        isSwiping = true;
    }

    void DetectSwipeGesture(Collider handCollider)
    {
        if (isSwiping)
        {
            finalPosition = handCollider.transform.position;
            Vector3 delta = finalPosition - initialPosition;

            if (delta.magnitude > swipeThreshold) // Check if swipe distance exceeds the threshold
            {
                RotateObjectBasedOnSwipe(delta);
                ResetSwipeGesture(); // Reset after detecting a swipe
            }

            initialPosition = finalPosition; // Update the initial position for the next swipe
        }
    }

    void RotateObjectBasedOnSwipe(Vector3 delta)
    {
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
            {
                Debug.Log("Swipe Right");
                RotateObject(Vector3.up); // Rotate clockwise
            }
            else
            {
                Debug.Log("Swipe Left");
                RotateObject(Vector3.down); // Rotate counter-clockwise
            }
        }
        else
        {
            if (delta.y > 0)
            {
                Debug.Log("Swipe Up");
                RotateObject(Vector3.right); // Rotate upward
            }
            else
            {
                Debug.Log("Swipe Down");
                RotateObject(Vector3.left); // Rotate downward
            }
        }
    }

    void RotateObject(Vector3 rotationAxis)
    {
        objectToRotate.transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }

    void ResetSwipeGesture()
    {
        isSwiping = false;
        initialPosition = Vector3.zero;
        finalPosition = Vector3.zero;
    } */