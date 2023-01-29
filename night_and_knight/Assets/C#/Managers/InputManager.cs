using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * @brief 사용자의 키보드, 마우스 입력을 받는 Manager
 */
public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    private bool mPressed = false;

    /**
     * @brief 입력이 없다면 바로 리턴, 입력이 있다면 KeyAction/MouseAction을 Invoke
     * @details 매 프레임마다 실행
     */
    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (KeyAction != null && Input.anyKey)
            KeyAction.Invoke();

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                mPressed = true;
            }
            else
            {
                if (mPressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                mPressed = false;
            }
        }
    }
}

/* 사용예제
    void Start()
    {
        GameManager.InputMng.KeyAction += OnKeyboard;
    }

    void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * Time.deltaTime * mSpeed;
        }
    }
*/
