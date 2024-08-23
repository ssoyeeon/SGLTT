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

    public List<Transform> ChackSiObject = new List<Transform>();

    public Rigidbody rig;

    public Camera camera;
    public float distance = 10f;                                //레이 거리에여 

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rig.angularVelocity = Vector3.zero;

            RaycastHit hit;                                         //레이를 쏴요
            Vector3 mouseScreenPosition = Input.mousePosition;      //마우스 포지션을 Vector3 변수에 넣어요
            Ray ray = camera.ScreenPointToRay(Input.mousePosition); //레이를 파우스 포지션에 쏴요

            if (Physics.Raycast(ray, out hit))                      //만약 레이가 맞았다면
            {
                if (hit.collider.tag == "Cube")                     //만약 레이를 맞은게 큐브(아이)라면
                {
                    GameObject objectHit = hit.collider.gameObject; //레이 쏴요!
                    mouseScreenPosition.z = distance;               //z는 거리에여 10이에요!
                    objectHit.transform.position = camera.ScreenToWorldPoint(mouseScreenPosition);  //오브젝트를 마우스 포지션에 따라 움직여요

                    float dist = Vector3.Distance(other.position, transform.position);          //거리를 구해요
                                                                                                //Debug.Log("Distance to other: " + dist);
                    other.localScale = new Vector3(dist * 0.4f, dist * 0.4f, dist * 0.4f);      //거리에 따라 스케일 값을 변경해요

                    if (Input.GetKey(KeyCode.E))        //E를 누르면요
                    {
                        other.transform.Rotate(0f, -Input.GetAxis("Mouse X") * speed, 0f, Space.World); //돌아간당
                        other.transform.Rotate(-Input.GetAxis("Mouse Y") * speed, 0f, 0f);              //빙글빙글
                    }
                }
            }
        }
    }
}
