using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFix : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            //�÷��̾��� ������ٵ� �ӷ��� 0���� ���� 
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            //�÷��̾��� ������ٵ� �ӷ��� 0���� ���� 
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
