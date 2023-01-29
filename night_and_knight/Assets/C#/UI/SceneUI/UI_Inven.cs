using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }
    private void Start()
    {
        Init();
    }
    
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = GetGameObject((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            GameManager.ResourceMng.Destroy(child.gameObject);

        // todo TestCode
        for (int i = 0; i < 8; i++)
        {
            GameObject go = GameManager.UIMng.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;
            UI_Inven_Item invenItem = go.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo($"Item{i}");
        }
    }
}
