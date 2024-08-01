using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StackLand : MonoBehaviour
{
    //게임 오브젝트가 생성되면 바로 리스트에 들어가기
    //카드를 쌓았을 때 밑으로 합쳐지게 하기
    //카드를 쌓았을 때 맨 위 카드를 부모로 만들고 그 밑에 들어가기
    //움직일 때 챠르르를 어떻게 하죠?
    //그냥 오브젝트 잡고 이동하면 돼
    //스택부터 만들어야하네요~

    public List<GameObject> cards;      //안에 생성되는 오브젝트들을 넣을거에요
    public GameObject parentsCards;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
