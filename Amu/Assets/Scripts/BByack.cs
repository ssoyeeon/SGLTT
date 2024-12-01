using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BByack : MonoBehaviour
{
    public GameObject prefabWall;
    public Vector3 prefabStartPosition = new Vector3(0,0,0);
    public bool isStart;
    public GameObject startWall;
    public float te = 12;

    void Update()
    {
        GameObject temp;
        if(startWall.transform.position == new Vector3(0,0,0))
        {
            te *= 2;
            Vector3 newPosition = new Vector3(0, te, 0);
            temp = GameObject.Instantiate(prefabWall, prefabStartPosition);
        }
    }
}
