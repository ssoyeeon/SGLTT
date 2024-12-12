using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dotweenen : MonoBehaviour
{
    public GameObject endGameObject;

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("StartScene");
        }

        endGameObject.transform.position += new Vector3(0, 0.001f, 0);
    }
}
