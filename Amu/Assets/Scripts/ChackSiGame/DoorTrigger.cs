using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Collider doorcollider;

    public void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("DoorTrigger"))
        {
            Debug.Log("�� �ƴٰ�!!");
            doorcollider.enabled = false;
        }

    }
}
