using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryTest : MonoBehaviour
{
    Dictionary<int, string> test = new Dictionary<int, string>();

    void Start()
    {
        test.Add(1, "�� �峭��");
        test.Add(2, "�κ�");
        test.Add(3, "���� å");
    }

    void Update()
    {
        
    }
}
