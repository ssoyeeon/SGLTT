using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public Collider doorCollider;
    public GameObject doorKey;

    private void OnTriggerEnter(Collider other)
    {
        if(doorKey)
        {
            doorCollider.enabled = false;
        }
    }
}
