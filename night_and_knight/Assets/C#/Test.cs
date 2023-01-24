using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        TestCube();
    }

    public void TestCube()
    {
        List<GameObject> go = new List<GameObject>();
        for (int i = 0; i < 2; i++)
            go.Add(GameManager.ResourceMng.Instantiate("Cube"));
    }
}