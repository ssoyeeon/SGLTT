using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Hp : MonoBehaviour
{

    public int hp;              //인간의 HP
    private int Attack = 5;     //내 공격력

    public bool gamestart;      //시작 했는지 안 했는지 bool 값으로 확인

    public float time = 120;   //게임 시간
    public TMP_Text text;       //시간 넣을 텍스트

    //한 명이라도 못 꼬시면 게임이 끝나버리는 눈빛 보내기 게임 ㅎ
    void Start()
    {
        hp = Random.Range(10, 40);      //인간의 생명은 다양하지롱
        gamestart = true;               //게임이 시작됐다구~
    }

    void Update()
    {
        text.text = time.ToString("시~작!");      //시~작 을 넣어주면서 화면에 남은 시간을 띄워줍시다ㅎㅎ
        time -= Time.deltaTime;                   //시간을 계속 지나게 해줘야겠죠~?
        if (gamestart == true)                   //bool 값 만든거 기억하죠~? 그게 트루라면~
        {
            if (hp >= 60)                       //HP가 60이 넘어가면 
            {
                Debug.Log("크크 난 너에게 넘어가지 않아!");     //난 너에게 넘어가지 않아!키키 
                Destroy(this);                                //급발진 게임 종료
                gamestart = false;
            }

            if (this.hp >= 0)                   //인간의 생명력은 계속해서 늘어나요!!!
            {
                hp++;                           //늘어난다아아앗!!!!!!!!!!!
            }


            if (Input.GetMouseButton(0))
            {
                hp -= Attack;                   //마우스 좌클릭으로 계속해서 때려!!!!
            }

            if (hp <= 0)
            {
                hp = 0;                         //hp가 0이면
                Destroy(this.gameObject);       //게임 오브젝트를 지우고
                Debug.Log("너는 나에게 넘어왔지 모얌!");       //크크 넌 나에게 넘어왔닷!
            }

            /*switch(hp)      
            {
                case 10:
                    Debug.Log("내가.. 넘어갈듯 하냐!!");
                    break;
                case 20:
                    Debug.Log("안 넘어간다고!");
                    hp += 5;
                    break;
                case 30:
                    Debug.Log("크크 난 피가 계속 찬다고..!!");
                    break;
                case 40:
                    Debug.Log("제대로 하고 있는거야?");
                    break;
                case 50:
                    Debug.Log("어렵구나 너?");
                    break;
                case 60:
                    Debug.Log("하하 뭐하자는거지?");
                    break;

            }
            */
        }

        if (time <= 0)              //시간 초과~!
        {
            time = 0;
            gamestart = false;      //게임 꺼.
        }

        if(gamestart == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))        //스페이스바를 누르면
            {
                gamestart = true;                       //다시 시작~~!!
            }
        }
    }
}
