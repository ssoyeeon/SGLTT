using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    //���� ������ �� Hp�� Ramdom.Range�� �̿��ؼ� �������� �ϰ�, �� ��ũ��Ʈ�� �׳� �ΰ����� �־ �ϴ°� � 
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
                Debug.Log("ũũ �� �ʿ��� �Ѿ�� �ʾ�!");
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
                Debug.Log("�ʴ� ������ �Ѿ���� ���!");
            }

            //switch(hp)        �̰� �� ���� �����ϰ� �ϰ�ʹ�....
            //{
            //    case 10:
            //        Debug.Log("����.. �Ѿ�� �ϳ�!!");
            //        break;
            //    case 20:
            //        Debug.Log("�� �Ѿ�ٰ�!");
            //        hp += 5;
            //        break;
            //    case 30:
            //        Debug.Log("ũũ �� �ǰ� ��� ���ٰ�..!!");
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
