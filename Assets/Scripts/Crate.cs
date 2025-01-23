using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private float rotationSpeed = 100f;
    private AudioSource audioSource;
    private Animation animator;

    private bool canRotate, isOpen;


    void Start()
    {
        animator = GetComponent<Animation>();
        audioSource = GetComponent<AudioSource>();
    }

    public async void ManageCrate()
    {
        isOpen = !isOpen;
        if (animator == null) return;

        animator.Play(isOpen ? "Crate_Open" : "Crate_Close");
        audioSource.Play();

        gun.transform.rotation = Quaternion.identity;
        await Task.Delay(2000);
        canRotate = isOpen;
        Debug.Log("Animation finished");
    }

    public void RotateGun(Vector3 rotationAxis, float delatMagnitude)
    {
        if (rotationAxis == null || !canRotate) return;

        gun.transform.transform.Rotate(rotationAxis * delatMagnitude * rotationSpeed * Time.deltaTime);
    }

    [ContextMenu(" Test Open Animation")]
    public void TestAnimation_Open()
    {
        //ManageCrate(true);
    }

    [ContextMenu(" Test Close Animation")]
    public void TestAnimation_Close()
    {
        //ManageCrate(false);
    }
}
