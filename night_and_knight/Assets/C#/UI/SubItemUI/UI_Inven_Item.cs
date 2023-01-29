using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.UI;

public class UI_Inven_Item : UI_Base
{
    // 종류가 적을땐 GameObjects로 통일
    enum GameObjects
    {
        ItemIcon,
        ItemNameText
    }

    private string mItemName;
    
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        GetGameObject((int)GameObjects.ItemNameText).GetComponent<TextMeshProUGUI>().text = mItemName;
        
        GetGameObject((int)GameObjects.ItemIcon).gameObject.BindEvent(OnButtonClicked, Define.UIEvent.Click);
    }

    public void SetInfo(string itemName)
    {
        mItemName = itemName;
    }

    public void OnButtonClicked(PointerEventData data)
    {
        Debug.Log($"{mItemName} Click!");
    }
}
