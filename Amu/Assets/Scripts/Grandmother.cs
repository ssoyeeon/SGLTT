using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandmother : MonoBehaviour
{
    public int[] pandomNumber = new int[20]; 
    public bool isStart;
    public int person;

    // Start is called before the first frame update
    void Start()
    {
        person = 4;
        isStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart == true)
        {
            for(int i = 0;  i < pandomNumber.Length; i++)
            {
                pandomNumber[0] = Random.Range(0, 20);
            }
        }
    }
}
