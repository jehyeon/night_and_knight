using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.TitleScene;

        GameManager.InputMng.KeyAction += OnKeyboard;
    }
    
    void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            GameManager.SceneMng.LoadScene(Define.Scene.GameScene);
        }
    }

    public override void Clear()
    {
        Debug.Log("TitleScene Clear!");
    }
}
