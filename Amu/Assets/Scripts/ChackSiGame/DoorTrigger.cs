using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Collider doorCollider;
    public GameObject doorKey;

    public void OnCollisionEnter(Collision collision)
    {
        if (doorKey)
        {
            doorCollider.enabled = false;
        }
    }
}
