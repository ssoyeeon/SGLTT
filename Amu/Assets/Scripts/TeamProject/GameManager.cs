using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isCollect;      //�� ����
    public bool isStart;        //���� ����
    public bool isFinish;       //���� ����

    void Start()
    {
        isCollect = false;
        isStart = false;
        isFinish = false;
    }

    void Update()
    {
        
    }
}
