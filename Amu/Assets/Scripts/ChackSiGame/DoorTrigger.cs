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

    public void OnTriggerEnter(Collider other)
    {
        if (doorKey)
       {
           doorCollider.enabled = false;
            Debug.Log(other.gameObject.name + "Å¸´Ù");
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (doorKey)
        {
            doorCollider.enabled = true;
            Debug.Log(other.gameObject.name + "Â¥ÀÜ");
        }
    }
    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (doorKey)
    //   {
    //       doorCollider.enabled = false;
    //        Debug.Log(collision.gameObject.name + "Å¸´Ù");
    //    }
    //}

    //public void OnCollisionExit(Collision collision)
    //{
    //    if (doorKey)
    //    {
    //        doorCollider.enabled = true;
    //        Debug.Log(collision.gameObject.name + "Â¥ÀÜ");
    //    }
    //}

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
