using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform tr;
    public TT[] TTs;
    public int index;
    void Start()
    {
        TTs = new TT[2];
        TTs[0] = new OO();
        TTs[1] = new PPAP();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TTs[index].aa();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(index+1 >= TTs.Length)
            {
                return;
            }
            index++;
        }
    }
}
public interface TT
{
    void aa();
}
public interface TP
{
    void aa();
}
public interface PP
{
    void aa();
}
public class OO : TT
{
    public void aa()
    {
        Debug.Log("OO");
        
    }
}
public class PPAP : TT
{
    public void aa()
    {
        Debug.Log("³ª´Â °³²ÜÀë PPap");
    }
}