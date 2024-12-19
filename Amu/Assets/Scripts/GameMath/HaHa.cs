using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaHa : MonoBehaviour
{
    void Start()
    {
        float udot = Vector3.Dot(transform.up, Vector3.up);
        float fdot = Vector3.Dot(transform.forward, Vector3.up);
        float rdot = Vector3.Dot(transform.right, Vector3.up);

        if(udot >= 0.9f)
        {
            Debug.Log("6");
        }
        else if(udot <= -0.9f)
        {
            Debug.Log("1");
        }
        else if(fdot >= 0.9f)
        {
            Debug.Log("4");
        }
        else if(fdot <= -0.9f)
        {
            Debug.Log("3");
        }
        else if(rdot >= 0.9f)
        {
            Debug.Log("2");
        }
        else if (rdot <= -0.9f)
        {
            Debug.Log("5");
        }

    }

    void Update()
    {
        
    }
}
