using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChackSiGame : MonoBehaviour
{
    //게임 오브젝트를 이리저리 돌릴 수도 있음 마우스 회전을 통해서
    //이리 저리 돌리고 놓은 물체 저장
    //만질 수 있는 모든 오브젝트를 거리 구해야하자나...? 계속해서...

    public Transform other;                                     //일단 아이 하나를 잡아왔어요
    public float speed = 20f;
    private Rigidbody characterRigidbody;

    public Camera camera;
    public float distance = 10f;


    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();         //리지드바디 내놓으세요!!
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");             //움직임 설정
        float inputZ = Input.GetAxis("Vertical");               //움직임 설정

        Vector3 velocity = new Vector3(inputX, 0, inputZ);      //움직임 설정
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
                float dist = Vector3.Distance(other.position, transform.position);          //거리를 구해요
                //Debug.Log("Distance to other: " + dist);
                other.localScale = new Vector3(dist * 0.4f, dist * 0.4f, dist * 0.4f);      //거리에 따라 스케일 값을 변경해요

                if (Input.GetKey(KeyCode.E))
                {
                    other.transform.Rotate(0f, -Input.GetAxis("Mouse X") * speed, 0f, Space.World);
                    other.transform.Rotate(-Input.GetAxis("Mouse Y") * speed, 0f, 0f);
                }
            }
        }
    }
}
