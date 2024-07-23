using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChackSiGame : MonoBehaviour
{
    //게임 오브젝트를 이리저리 돌릴 수도 있음 마우스 회전을 통해서
    //이리 저리 돌리고 놓은 물체 저장
    //만질 수 있는 모든 오브젝트를 거리 구해야하자나...? 계속해서...

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
