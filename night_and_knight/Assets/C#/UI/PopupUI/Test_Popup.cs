using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * @brief TEST CODE
 */
public class Test_Popup : UI_Popup
{
    enum Buttons
    {
        MoneyButton
    }

    enum Texts
    {
        MoneyButtonText,
        MoneyText
    }

    enum GameObjects
    {
        TestObject
    }

    enum Images
    {
        ItemIcon
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        
        GetButton((int)Buttons.MoneyButton).gameObject.BindEvent(OnButtonClicked, Define.UIEvent.Click);
        GetImage((int)Images.ItemIcon).gameObject.BindEvent(OnDrag, Define.UIEvent.Drag);
    }

    private int mMoney = 0;
    public void OnButtonClicked(PointerEventData data)
    {
        mMoney++;
        GetText((int)Texts.MoneyText).text = $"Money: {mMoney}";
    }

    public void OnDrag(PointerEventData data)
    {
        print("drag");
        GetImage((int)Images.ItemIcon).transform.position = data.position;
    }
}
