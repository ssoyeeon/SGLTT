using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRigidbody : MonoBehaviour
{
    public Rigidbody objectRigidbody;
    public GameObject holdObject;
    public PlayerController playerController;

    void Start()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        objectRigidbody.isKinematic = false;
    }

    void Update()
    {
        /*if(Input.GetMouseButtonDown(0) && holdObject == playerController.heldObject)
        {
            objectRigidbody.isKinematic = true;
        }    */
    }
}
