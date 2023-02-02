using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * @brief 모든 Scene의 조상 클래스
 */
public abstract class BaseScene : MonoBehaviour
{ 
    public Define.Scene SceneType { get; protected set; } = Define.Scene.UnknownScene;

    private void Awake()
    {
        Init();
    }
    
    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            GameManager.ResourceMng.Instantiate("UI/EventSystem").name = "@EventSystem";
    }
    
    public abstract void Clear();
}