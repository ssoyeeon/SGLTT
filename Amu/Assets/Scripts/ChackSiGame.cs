using Microsoft.Win32.SafeHandles;
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

    public List<Transform> ChackSiObject = new List<Transform>();

    public Rigidbody rig;

    public Camera camera;
    public float distance = 10f;                                //���� �Ÿ����� 

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rig.angularVelocity = Vector3.zero;

            RaycastHit hit;                                         //���̸� ����
            Vector3 mouseScreenPosition = Input.mousePosition;      //���콺 �������� Vector3 ������ �־��
            Ray ray = camera.ScreenPointToRay(Input.mousePosition); //���̸� �Ŀ콺 �����ǿ� ����

            if (Physics.Raycast(ray, out hit))                      //���� ���̰� �¾Ҵٸ�
            {
                if (hit.collider.tag == "Cube")                     //���� ���̸� ������ ť��(����)���
                {
                    GameObject objectHit = hit.collider.gameObject; //���� ����!
                    mouseScreenPosition.z = distance;               //z�� �Ÿ����� 10�̿���!
                    objectHit.transform.position = camera.ScreenToWorldPoint(mouseScreenPosition);  //������Ʈ�� ���콺 �����ǿ� ���� ��������

                    float dist = Vector3.Distance(other.position, transform.position);          //�Ÿ��� ���ؿ�
                                                                                                //Debug.Log("Distance to other: " + dist);
                    other.localScale = new Vector3(dist * 0.4f, dist * 0.4f, dist * 0.4f);      //�Ÿ��� ���� ������ ���� �����ؿ�

                    if (Input.GetKey(KeyCode.E))        //E�� �������
                    {
                        other.transform.Rotate(0f, -Input.GetAxis("Mouse X") * speed, 0f, Space.World); //���ư���
                        other.transform.Rotate(-Input.GetAxis("Mouse Y") * speed, 0f, 0f);              //���ۺ���
                    }
                }
            }
        }
    }
}
