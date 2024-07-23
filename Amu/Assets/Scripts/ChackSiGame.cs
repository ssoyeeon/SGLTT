using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChackSiGame : MonoBehaviour
{
    //���� ������Ʈ�� �̸����� ���� ���� ���� ���콺 ȸ���� ���ؼ�
    //�̸� ���� ������ ���� ��ü ����
    //���� �� �ִ� ��� ������Ʈ�� �Ÿ� ���ؾ����ڳ�...? ����ؼ�...

    public Transform other;
    public float speed = 20f;
    private Rigidbody characterRigidbody;

    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(inputX, 0, inputZ);
        velocity *= speed;
        characterRigidbody.velocity = velocity;

        if (other)
        {
            float dist = Vector3.Distance(other.position, transform.position);
            Debug.Log("Distance to other: " + dist);
            other.localScale = new Vector3(dist * 0.4f, dist * 0.4f, dist * 0.4f); 
        }

        if(Input.GetMouseButtonDown(0))
        {

        }
    }
}
