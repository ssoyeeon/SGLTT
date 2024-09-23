using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Collider doorCollider;
    public GameObject doorKey;

    public bool isStay = false;

     //public void OnCollisionEnter(Collision collision)
     //{
     //    if (doorKey)
     //    {
     //        doorCollider.enabled = false;
     //    }
     //}
     public void OnCollisionExit(Collision collision)
     {
        isStay = false; if (doorKey && isStay == false)
        {
            doorCollider.GetComponent<Collider>().enabled = true;
        }
    }
    public void OnCollisionStay(Collision collision)
    {
        isStay = true; if (doorKey && isStay == true)
        {
            doorCollider.enabled = false;
        }
    }

    //void Update()
    //{
    //    if (doorKey && isStay == true)
    //    {
    //        doorCollider.enabled = false;
    //    }
    //    if (doorKey && isStay == false)
    //    {
    //        doorCollider.GetComponent<Collider>().enabled = true;
    //    }
    //}
}
