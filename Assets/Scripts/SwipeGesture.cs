using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    private Queue<Vector3> swipeQueue = new Queue<Vector3>();

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
            Transform handTransform = other.transform.parent;
            DetectSwipeGesture(handTransform);
            //material.color = Color.cyan;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand")) // Reset when the hand leaves the tracking region
        {
            ResetSwipeGesture();
            material.color = Color.grey;
            swipeQueue.Clear();
        }
    }

    void StartSwipeGesture(Collider handCollider)
    {
        // Start tracking the swipe when the hand enters the region
        initialPosition = handCollider.transform.parent.position;
        isSwiping = true;
    }

    void DetectSwipeGesture(Transform handCollider)
    {
        if (isSwiping)
        {

            if (swipeQueue.Count < 10)
            {
                swipeQueue.Enqueue(handCollider.position);
                return;
            }
            else
            {
                initialPosition = swipeQueue.Dequeue();
                finalPosition = handCollider.position;
                Vector3 delta = finalPosition - initialPosition;


                //finalPosition = handCollider.position;

                if (delta.magnitude > swipeThreshold) // Check if swipe distance exceeds the threshold
                {
                    RotateObjectBasedOnSwipe(delta);
                    //ResetSwipeGesture(); // Reset after detecting a swipe
                }

                swipeQueue.Enqueue(finalPosition);
                //            initialPosition = finalPosition; // Update the initial position for the next swipe
            }
        }
    }

    void RotateObjectBasedOnSwipe(Vector3 delta)
    {
        Vector3 swipeDirection = Vector3.zero;
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.z))
        {
            if (delta.x > 0)
            {
                Debug.Log("Swipe Right");
                swipeDirection = Vector3.up;
            }
            else
            {
                Debug.Log("Swipe Left");
                swipeDirection = Vector3.down;
            }
        }
        else
        {
            if (delta.z > 0)
            {
                Debug.Log("Swipe Up");
                swipeDirection = Vector3.right;
            }
            else
            {
                Debug.Log("Swipe Down");
                swipeDirection = Vector3.left;
            }
        }
        RotateObject(swipeDirection, delta.magnitude);
    }

    void RotateObject(Vector3 rotationAxis, float delatMagnitude)
    {
        if (rotationAxis == null) return;

        objectToRotate.transform.Rotate(rotationAxis * delatMagnitude * rotationSpeed * Time.deltaTime);
    }

    void ResetSwipeGesture()
    {
        isSwiping = false;
        initialPosition = Vector3.zero;
        finalPosition = Vector3.zero;
    }

}
/* 
    */