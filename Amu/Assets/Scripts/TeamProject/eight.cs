using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class eight : MonoBehaviour
{
    //����� ���ٸ� ���� ������ ������ �Ѿ. ��ȣ +1
    //Ʋ�ȴٸ� ���� ������ ������ �Ѿ��, ��ȣ�� �ʱ�ȭ �� 
    //���� �̻������� �� 4, 6, 7, 9 �� ������ �³� ��� �Ұž�? 
    //4,6,7,9���� ���� ������Ʈ�� �ΰ� �� ���� ������Ʈ�� �ִٸ� ���ٸ����� üũ�ؼ� �ϴ°�?

    public bool isTrue;     //����� ������?

    public int scene;   //�� ����

    public int exit = 0;                        //�ⱸ
    public TMP_Text dayText;                    //�ⱸ ������ ä��

    public GameObject bed;                      //ħ�� (��)
    public GameObject door;                     //��   (������ ����)

    void Start()
    {
        scene = Random.Range(0, 10);
    }

    void Update()
    {
        if(isTrue == true)                      //������ ��
        {
            exit++;                             //�ⱸ ��ȣ +1
            dayText.text = exit.ToString(exit + "Days");
        }

        else if ( isTrue == false)              //Ʋ���� ��
        {
            exit = 0;                           //�ⱸ ��ȣ 0��. (�ʱ�ȭ)
            dayText.text = exit.ToString(exit + "Days");
        }

        if(isTrue)                              //�ϴ� ���� ���� ��
        {
           SceneManager.LoadScene(scene);      //���� 0~10 �� ���
        }

        Scene nowscene = SceneManager.GetActiveScene();     //���� ���� ��������
        if (nowscene.buildIndex == 4)                       //���� ���� 4���̸� ( �̻��� �� )
        {
            //�� �ڷ� Ʈ���� ������ ���� ������Ʈ �� ���� �ΰ� �� ���� ������Ʈ�� ���������� �Ǻ��ϸ� �� �� ���׿�.
        }
    }
}
