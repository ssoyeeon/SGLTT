using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isCollect;      //맵 선택
    public bool isStart;        //게임 시작
    public bool isFinish;       //게임 종료

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
