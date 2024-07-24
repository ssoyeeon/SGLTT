using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChackSiGame : MonoBehaviour
{
    //���� ������Ʈ�� �̸����� ���� ���� ���� ���콺 ȸ���� ���ؼ�
    //�̸� ���� ������ ���� ��ü ����
    //���� �� �ִ� ��� ������Ʈ�� �Ÿ� ���ؾ����ڳ�...? ����ؼ�...

    public Transform other;                                     //�ϴ� ���� �ϳ��� ��ƿԾ��
    public float speed = 20f;
    private Rigidbody characterRigidbody;
    public Transform Parent;                                   //�θ� ���� ��ġ�߾��!

    private bool isHold;                                        //���̸� ��� �ֳ���?

    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();         //������ٵ� ����������!!
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");             //������ ����
        float inputZ = Input.GetAxis("Vertical");               //������ ����

        Vector3 velocity = new Vector3(inputX, 0, inputZ);      //������ ����
        velocity *= speed;
        characterRigidbody.velocity = velocity;

        

        if(Input.GetMouseButton(0))
        {

            if (other)
            {
                float dist = Vector3.Distance(other.position, transform.position);          //�Ÿ��� ���ؿ�
                Debug.Log("Distance to other: " + dist);
                other.localScale = new Vector3(dist * 0.4f, dist * 0.4f, dist * 0.4f);      //�Ÿ��� ���� ������ ���� �����ؿ�

                /*other.transform.parent = Parent.transform;                                  //�� �� �θ� �Ŷ�!!*/

                isHold = true;                                                              //��� �־��!

                if (Input.GetMouseButton(0))                                            //�� �� �� ������ 
                {
                    isHold = false;                                                         //�� ���� Ǯ����
                   /* other.transform.parent = null;                                          //������ �θ� �������� */
                }

                other.Translate(0f, -Input.GetAxis("Mouse X") * 1.2f, 0f, Space.World);        //�̻���!! �̻���!! �̰� �� �̷�!!
                other.Translate(-Input.GetAxis("Mouse Y") * 1.2f, 0f, 0f);                     //����ֽÿͿ�.. ���콺�� ������, ��,�� �� �ű�� �;����...

            }

            if(Input.GetKey(KeyCode.E))
            {
                other.transform.Rotate(0f, -Input.GetAxis("Mouse X") * speed, 0f, Space.World);
                other.transform.Rotate(-Input.GetAxis("Mouse Y") * speed, 0f, 0f);
            }
        }
    }
}
