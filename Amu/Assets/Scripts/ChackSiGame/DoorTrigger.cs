using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Collider doorCollider;
    public GameObject doorKey;
    public float exitDistanceThreshold = 5.0f; // 나갈 때의 거리 임계값
    private GameObject objectInTrigger = null; // 트리거 안에 있는 물체 추적

    void OnTriggerEnter(Collider other)
    {
        if (doorKey)
        {
            doorCollider.enabled = false;
            objectInTrigger = other.gameObject;
            Debug.Log(other.gameObject.name + " 트리거 진입");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (doorKey)
        {
            doorCollider.enabled = true;
            objectInTrigger = null;
            Debug.Log(other.gameObject.name + " 트리거 이탈");
        }
    }

    void Update()
    {
        if (objectInTrigger != null)
        {
            float distance = Vector3.Distance(transform.position, objectInTrigger.transform.position);

            if (distance > exitDistanceThreshold)
            {
                // 트리거에서 나간 것으로 처리
                doorCollider.enabled = true;
                objectInTrigger = null;
                Debug.Log("트리거 이탈");
            }
        }
    }
}
