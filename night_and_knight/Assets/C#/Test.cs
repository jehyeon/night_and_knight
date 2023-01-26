using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * @brief TEST CODE
 */
public class Test : MonoBehaviour
{
    private void Start()
    {
        TestButtonUI();
    }

    public void TestCube()
    {
        List<GameObject> go = new List<GameObject>();
        for (int i = 0; i < 2; i++)
            go.Add(GameManager.ResourceMng.Instantiate("Cube"));
    }

    public void TestButtonUI()
    {
        Test_Popup button = GameManager.UIMng.ShowPopupUI<Test_Popup>("Test_Popup");
    }
}