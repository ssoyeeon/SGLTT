using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{ 
    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = gameObject.transform.forward * vertical + gameObject.transform.right * horizontal;

        gameObject.transform.Translate(moveDir.normalized * 20 * Time.deltaTime, Space.World);
    }
}
