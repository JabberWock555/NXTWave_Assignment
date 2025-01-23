using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SwipeGesture : MonoBehaviour
{
    public Crate crate; // The object to rotate

    private Vector3 initialPosition;
    private Vector3 finalPosition;
    private bool isSwiping = false;
    private float swipeThreshold = 0.01f; // Minimum swipe distance (meters)

    private Queue<Vector3> swipeQueue = new Queue<Vector3>();


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand")) // Assuming the hand is tagged as "Hand"
        {
            StartSwipeGesture(other);
            //crate.ManageCrate(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand")) // Continue detecting gestures while the hand is in the collider
        {
            Transform handTransform = other.transform;
            DetectSwipeGesture(handTransform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand")) // Reset when the hand leaves the tracking region
        {
            ResetSwipeGesture();
            swipeQueue.Clear();
            //crate.ManageCrate(false);
        }
    }

    void StartSwipeGesture(Collider handCollider)
    {
        // Start tracking the swipe when the hand enters the region
        initialPosition = handCollider.transform.position;
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

                if (delta.magnitude > swipeThreshold) // Check if swipe distance exceeds the threshold
                {
                    RotateObjectBasedOnSwipe(delta);
                }

                swipeQueue.Enqueue(finalPosition);
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
        crate.RotateGun(swipeDirection, delta.magnitude);
    }



    void ResetSwipeGesture()
    {
        isSwiping = false;
        initialPosition = Vector3.zero;
        finalPosition = Vector3.zero;
    }

}
