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
    public Transform Parent;                                   //부모도 같이 납치했어요!

    private bool isHold;                                        //아이를 잡고 있나요?

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

        

        if(Input.GetMouseButton(0))
        {

            if (other)
            {
                float dist = Vector3.Distance(other.position, transform.position);          //거리를 구해요
                Debug.Log("Distance to other: " + dist);
                other.localScale = new Vector3(dist * 0.4f, dist * 0.4f, dist * 0.4f);      //거리에 따라 스케일 값을 변경해요

                /*other.transform.parent = Parent.transform;                                  //너 내 부모가 돼라!!*/

                isHold = true;                                                              //잡고 있어요!

                if (Input.GetMouseButton(0))                                            //한 번 더 누르면 
                {
                    isHold = false;                                                         //불 값이 풀려요
                   /* other.transform.parent = null;                                          //아이의 부모가 사라졌어요 */
                }

                other.Translate(0f, -Input.GetAxis("Mouse X") * 1.2f, 0f, Space.World);        //이상해!! 이상해!! 이거 왜 이래!!
                other.Translate(-Input.GetAxis("Mouse Y") * 1.2f, 0f, 0f);                     //살려주시와요.. 마우스로 가까이, 좌,우 를 옮기고 싶었어요...

            }

            if(Input.GetKey(KeyCode.E))
            {
                other.transform.Rotate(0f, -Input.GetAxis("Mouse X") * speed, 0f, Space.World);
                other.transform.Rotate(-Input.GetAxis("Mouse Y") * speed, 0f, 0f);
            }
        }
    }
}
