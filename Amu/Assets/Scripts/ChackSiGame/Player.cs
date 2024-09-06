using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float force = 10.0f;
    public Rigidbody rig;
    public bool isJumping;
    public float jumpTime;

     void Start()
    {
        rig = GetComponent<Rigidbody>();
        isJumping = false;
        jumpTime = 2f;
    }
    void Update()
    {
        Move(); Jump();
    }

    public void Move()
    {
        /*if (Input.GetKey(KeyCode.W))
            //this.transform.Translate(new Vector3(0, 0, 3));
           // rig.AddForce(0, 0, 100);
        if (Input.GetKey(KeyCode.A))
        // rig.AddForce(-100, 0, 0);
        //this.transform.Translate(new Vector3(-3, 0, 0));
        if (Input.GetKey(KeyCode.S))
            //rig.AddForce(0, 0, -100);
       // this.transform.Translate(new Vector3(0, 0, -3));
        if (Input.GetKey(KeyCode.D))
        //rig.AddForce(100, 0, 0);
        this.transform.Translate(new Vector3(3, 0, 0)); */

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        transform.position += new Vector3(Horizontal, 0, Vertical) * force * Time.deltaTime;
       // Vector3 move = new Vector3(Horizontal, 0, Vertical);
       // rig.AddForce(move * force);
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isJumping == false) 
            {
                rig.AddForce(0, 200, 0);
                isJumping =true;
            }
        }
        if(isJumping==true)
        { jumpTime -= Time.deltaTime;
            if (jumpTime <= 0)
            {
                jumpTime = 2;
                isJumping = false;
            }
        }
    }
}
