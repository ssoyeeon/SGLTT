using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Hp : MonoBehaviour
{

    public int hp;              //�ΰ��� HP
    private int Attack = 5;     //�� ���ݷ�

    public bool gamestart;      //���� �ߴ��� �� �ߴ��� bool ������ Ȯ��

    public float time = 120;   //���� �ð�
    public TMP_Text text;       //�ð� ���� �ؽ�Ʈ

    //�� ���̶� �� ���ø� ������ ���������� ���� ������ ���� ��
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
                hp -= Attack;                   //���콺 ��Ŭ������ ����ؼ� ����!!!!
            }

            if (hp <= 0)
            {
                hp = 0;                         //hp�� 0�̸�
                Destroy(this.gameObject);       //���� ������Ʈ�� �����
                Debug.Log("�ʴ� ������ �Ѿ���� ���!");       //ũũ �� ������ �Ѿ�Դ�!
            }

            /*switch(hp)      
            {
                case 10:
                    Debug.Log("����.. �Ѿ�� �ϳ�!!");
                    break;
                case 20:
                    Debug.Log("�� �Ѿ�ٰ�!");
                    hp += 5;
                    break;
                case 30:
                    Debug.Log("ũũ �� �ǰ� ��� ���ٰ�..!!");
                    break;
                case 40:
                    Debug.Log("����� �ϰ� �ִ°ž�?");
                    break;
                case 50:
                    Debug.Log("��Ʊ��� ��?");
                    break;
                case 60:
                    Debug.Log("���� �����ڴ°���?");
                    break;

            }
            */
        }

        if (time <= 0)              //�ð� �ʰ�~!
        {
            time = 0;
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
