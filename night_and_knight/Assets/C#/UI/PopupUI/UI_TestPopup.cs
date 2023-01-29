using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * @brief TEST CODE
 */
public class UI_TestPopup : UI_Popup
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

    private int mMoney = 0;
    
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
    
    public void OnButtonClicked(PointerEventData data)
    {
        mMoney++;
        GetTextMeshProUGUI((int)Texts.MoneyText).text = $"Money: {mMoney}!";
    }

    public void OnDrag(PointerEventData data)
    {
        GetImage((int)Images.ItemIcon).transform.position = data.position;
    }
}
