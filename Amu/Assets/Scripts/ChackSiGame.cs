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
    private Rigidbody characterRigidbody;

    public Camera camera;
    public float distance = 10f;


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

        if (Input.GetMouseButton(0))
        {

            RaycastHit hit;
            Vector3 mouseScreenPosition = Input.mousePosition;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Cube")
                {
                    GameObject objectHit = hit.collider.gameObject;
                    mouseScreenPosition.z = distance;
                    objectHit.transform.position = camera.ScreenToWorldPoint(mouseScreenPosition);
                }
            }

            if (other)
            {
                float dist = Vector3.Distance(other.position, transform.position);          //�Ÿ��� ���ؿ�
                //Debug.Log("Distance to other: " + dist);
                other.localScale = new Vector3(dist * 0.4f, dist * 0.4f, dist * 0.4f);      //�Ÿ��� ���� ������ ���� �����ؿ�

                if (Input.GetKey(KeyCode.E))
                {
                    other.transform.Rotate(0f, -Input.GetAxis("Mouse X") * speed, 0f, Space.World);
                    other.transform.Rotate(-Input.GetAxis("Mouse Y") * speed, 0f, 0f);
                }
            }
        }
    }
}
