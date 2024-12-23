using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : amugerna
{
    public Transform tr;
    public TT[] TTs;
    public int index = 1;
    public int tempIndex = 0;
    public SpriteRenderer spriteRenderer;
    public Sprite IMG
    {
        get { return sr; }
    }
    public int property
    {
        get { return index; }
        set 
        { 
            if(tempIndex+value > 10)
            {
                return;
            }
            tempIndex =value;
        }
    }

    void Start()
    {
        TTs = new TT[2];
        TTs[0] = new OO();
        TTs[1] = new PPAP();
        spriteRenderer.sprite = IMG;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(property);
            property += 2;
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
public class amugerna : MonoBehaviour
{
    protected Sprite sr;
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