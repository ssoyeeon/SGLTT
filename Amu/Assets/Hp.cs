using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    //눈빛 보내기 ㅎ Hp를 Ramdom.Range를 이용해서 무작위로 하고, 이 스크립트를 그냥 인간에게 넣어서 하는거 어때 
    public int hp;
    private int Attack = 5;

    public bool gamestart;

    void Start()
    {
        hp = Random.Range(10, 40);
    }

    void Update()
    {
        if (gamestart == true)
        {
            if (hp >= 60)
            {
                Debug.Log("크크 난 너에게 넘어가지 않아!");
                Destroy(this);
                gamestart = false;
            }

            if (this.hp >= 0)
            {
                hp++;
            }


            if (Input.GetMouseButton(0))
            {
                hp -= Attack;
            }

            if (hp <= 0)
            {
                hp = 0;
                Destroy(this.gameObject);
                Debug.Log("너는 나에게 넘어왔지 모얌!");
            }

            //switch(hp)        이거 한 번만 실행하게 하고싶당....
            //{
            //    case 10:
            //        Debug.Log("내가.. 넘어갈듯 하냐!!");
            //        break;
            //    case 20:
            //        Debug.Log("안 넘어간다고!");
            //        hp += 5;
            //        break;
            //    case 30:
            //        Debug.Log("크크 난 피가 계속 찬다고..!!");
            //        break;

            //}
        }

        if(gamestart == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gamestart = true;
            }
        }
    }
}
