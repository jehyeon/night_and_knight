using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.GameScene;
        
        GameManager.UIMng.ShowSceneUI<UI_Inven>();

        // todo Test Code
        for (int i = 0; i < 5; i++)
            GameManager.ResourceMng.Instantiate("Cube");
    }
    
    public override void Clear()
    {
        Debug.Log("GameScene Clear!");
    }
}