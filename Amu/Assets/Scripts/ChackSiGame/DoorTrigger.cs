using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Collider doorCollider;
    public GameObject doorKey;
    public float exitDistanceThreshold = 5.0f; // ���� ���� �Ÿ� �Ӱ谪
    private GameObject objectInTrigger = null; // Ʈ���� �ȿ� �ִ� ��ü ����

    void OnTriggerEnter(Collider other)
    {
        if (doorKey)
        {
            doorCollider.enabled = false;
            objectInTrigger = other.gameObject;
            Debug.Log(other.gameObject.name + " Ʈ���� ����");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (doorKey)
        {
            doorCollider.enabled = true;
            objectInTrigger = null;
            Debug.Log(other.gameObject.name + " Ʈ���� ��Ż");
        }
    }

    void Update()
    {
        if (objectInTrigger != null)
        {
            float distance = Vector3.Distance(transform.position, objectInTrigger.transform.position);

            if (distance > exitDistanceThreshold)
            {
                // Ʈ���ſ��� ���� ������ ó��
                doorCollider.enabled = true;
                objectInTrigger = null;
                Debug.Log("Ʈ���� ��Ż");
            }
        }
    }
}
