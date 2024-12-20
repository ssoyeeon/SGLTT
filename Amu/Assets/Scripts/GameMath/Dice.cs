using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dice : MonoBehaviour
{
    public Rigidbody ri;
    public TMP_Text intText;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            int fint = Random.Range(1, 4000);
            int dint = Random.Range(1, 4000);
            int rint = Random.Range(1, 4000);

            ri.AddForce(Vector3.up * 200);
            ri.AddTorque(new Vector3(fint, dint, rint), ForceMode.VelocityChange);
        }
        if(Input.GetMouseButtonDown(0))
        {
            float udot = Vector3.Dot(transform.up, Vector3.up);
            float fdot = Vector3.Dot(transform.forward, Vector3.up);
            float rdot = Vector3.Dot(transform.right, Vector3.up);

            if (udot >= 0.9f)
            {
                intText.text = "5";
                Debug.Log("5");
            }
            else if (udot <= -0.9f)
            {
                intText.text = "2";
                Debug.Log("2");
            }
            else if (fdot >= 0.9f)
            {
                intText.text = "1";
                Debug.Log("1");
            }
            else if (fdot <= -0.9f)
            {
                intText.text = "6";
                Debug.Log("6");
            }
            else if (rdot >= 0.9f)
            {
                intText.text = "3";
                Debug.Log("3");
            }
            else if (rdot <= -0.9f)
            {
                intText.text = "4";
                Debug.Log("4");
            }
        }
    }
}
