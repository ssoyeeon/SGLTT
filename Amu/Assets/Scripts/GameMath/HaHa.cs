using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaHa : MonoBehaviour
{
    private readonly Vector3[] faceDirections = new Vector3[6]
    {
        Vector3.up,    // 6
        Vector3.down,  // 1
        Vector3.right, // 2
        Vector3.left,  // 5
        Vector3.forward,  // 4
        Vector3.back   // 3
    };

    private readonly int[] faceNumbers = new int[6] { 6, 1, 2, 5, 4, 3 };

    void Update()
    {
        float maxDot = -1f;
        int faceIndex = 0;

        for (int i = 0; i < 6; i++)
        {
            float dot = Vector3.Dot(transform.up, faceDirections[i]);
            if (dot > maxDot)
            {
                maxDot = dot;
                faceIndex = i;
            }
        }

        if (maxDot >= 0.9f)
        {
            Debug.Log($"ÁÖ»çÀ§ À­¸é: {faceNumbers[faceIndex]}");
        }
    }
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
}
