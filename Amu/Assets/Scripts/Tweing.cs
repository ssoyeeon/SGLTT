using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tweing : MonoBehaviour     //Tweening���� ����.
{
    //��Ʈ���� �� ���� ���� �����̹Ƿ�, ������Ʈ �Լ����� �ִ��� ���� �ʴ´�.
    public enum State                       //�ڷ��� enum (������) ��� ������ ���
    {
        Default,                            //�ڵ����� 0��
        Star,                               //�ڵ����� 1��
        Up = 10,                            //10������ ����
        Right,                              //�ڵ����� 11��  
        Punch
    }

    public State state = State.Default;     //Default�� �⺻ ������ �������ش�.
    public SpriteRenderer spr;

    void Start()
    {
        if(state == State.Right)
        {
            this.transform.DoMoveX(30, 30).SetEase(Ease, Linear).SetLoops(-1, LoopType.Yoyo);       //�̵� �Ÿ�, ~����
        }    

        if(state == State.Star)
        {
        }    
    }
}
