using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hp : MonoBehaviour
{
    //���� ������ �� Hp�� Ramdom.Range�� �̿��ؼ� �������� �ϰ�, �� ��ũ��Ʈ�� �׳� �ΰ����� �־ �ϴ°� � 
    //�ð� ���� �����°� �����.

    public int hp;              //�ΰ��� HP
    private int Attack = 5;     //�� ���ݷ�

    public bool gamestart;      //���� �ߴ��� �� �ߴ��� bool ������ Ȯ��

    private float time = 120;   //���� �ð�
    public TMP_Text text;       //�ð� ���� �ؽ�Ʈ

    void Start()
    {
        hp = Random.Range(10, 40);      //�ΰ��� ������ �پ�������
        gamestart = true;               //������ ���۵ƴٱ�~
    }

    void Update()
    {
        text.text = time.ToString("��~��!");      //��~�� �� �־��ָ鼭 ȭ�鿡 ���� �ð��� ����ݽô٤���
        time -= Time.deltaTime;                   //�ð��� ��� ������ ����߰���~?
        if (gamestart == true)                   //bool �� ����� �������~? �װ� Ʈ����~
        {
            if (hp >= 60)                       //HP�� 60�� �Ѿ�� 
            {
                Debug.Log("ũũ �� �ʿ��� �Ѿ�� �ʾ�!");     //�� �ʿ��� �Ѿ�� �ʾ�!ŰŰ 
                Destroy(this);                                //�޹��� ���� ����
                gamestart = false;
            }

            if (this.hp >= 0)                   //�ΰ��� ������� ����ؼ� �þ��!!!
            {
                hp++;                           //�þ�پƾƾ�!!!!!!!!!!!
            }


            if (Input.GetMouseButton(0))
            {
                hp -= Attack;                   //���콺 ��Ŭ������ ����ؼ� ��������!!!!
            }

            if (hp <= 0)
            {
                hp = 0;                         //hp�� 0�̸�
                Destroy(this.gameObject);       //���� ������Ʈ�� �����
                Debug.Log("�ʴ� ������ �Ѿ���� ���!");       //ũũ �� ������ �Ѿ�Դ�!
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

        if (time == 0)              //�ð� �ʰ�~!
        {
            gamestart = false;      //���� ��.
        }
        if(gamestart == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))        //�����̽��ٸ� ������
            {
                gamestart = true;                       //�ٽ� ����~~!!
            }
        }
    }
}
