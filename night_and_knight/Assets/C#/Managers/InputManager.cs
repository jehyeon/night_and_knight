using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action KeyAction = null;

    public void OnUpdate()
    {
        if (Input.anyKey == false)
            return;
        
        if (KeyAction != null)
            KeyAction.Invoke();
    }
}

/* 사용예제
    void Start()
    {
        GameManager.Input.mKeyAction += OnKeyboard;
    }

    void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * Time.deltaTime * mSpeed;
        }
    }
*/