using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tweing : MonoBehaviour     //Tweening으로 쓰기.
{
    //두트윈을 쓸 때는 연출 목적이므로, 업데이트 함수에는 최대한 쓰지 않는다.
    public enum State                       //자료형 enum (열거형) 상수 값들의 목록
    {
        Default,                            //자동으로 0번
        Star,                               //자동으로 1번
        Up = 10,                            //10번으로 지정
        Right,                              //자동으로 11번  
        Punch
    }

    public State state = State.Default;     //Default를 기본 값으로 지정해준다.
    public SpriteRenderer spr;

    void Start()
    {
        if(state == State.Right)
        {
            this.transform.DoMoveX(30, 30).SetEase(Ease, Linear).SetLoops(-1, LoopType.Yoyo);       //이동 거리, ~동안
        }    

        if(state == State.Star)
        {
        }    
    }
}
