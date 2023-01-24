using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    enum Buttons
    {
        PointButton
    }

    enum Texts
    {
        PointText,
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
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        
        // !!Test Code
        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);
    }

    private int mMoney = 0;
    public void OnButtonClicked(PointerEventData data)
    {
        mMoney++;
        GetText((int)Texts.MoneyText).text = $"Money: {mMoney}";
    }
}
