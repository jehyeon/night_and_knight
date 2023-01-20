using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief 사용자 마우스, 키보드 입력을 받는 Manager
 */
public class InputManager
{
    public Action KeyAction = null;
    
    /**
     * @brief 실행입력이 없다면 바로 리턴, 입력이 있다면 KeyAction을 Invoke
     * @details 매 프레임마다 실행
     */
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
