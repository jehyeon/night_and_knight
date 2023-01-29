using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * @brief 모든 UI의 조상 클래스
 */
public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> mObjectDic = new Dictionary<Type, UnityEngine.Object[]>();
    
    public abstract void Init();

    /*
     * @param T컴포넌트를 가지고 있는 모든 자식 GameObject를 검색해 mObjectDic에 Add
     */ 
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] uiNames = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[uiNames.Length];
        mObjectDic.Add(typeof(T), objects);

        for (int i = 0; i < uiNames.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, uiNames[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, uiNames[i], true);
            
            if (objects[i] == null)
                Debug.Log(($"Failed to bind({uiNames[i]})"));
        }
    }

    /*
     * @param T컴포넌트를 가지고 있으며 파라미터로 넘긴 idx에 해당하는 GameObject 검색
     * @return 검색한 GameObject를 T컴포넌트로 리턴
     */
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects;
        if (mObjectDic.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    protected GameObject GetGameObject(int idx) { return Get<GameObject>(idx); }

    protected TextMeshProUGUI GetTextMeshProUGUI(int idx) { return Get<TextMeshProUGUI>(idx); }

    protected Button GetButton(int idx) { return Get<Button>(idx); }

    protected Image GetImage(int idx) { return Get<Image>(idx); }

    /*
     * @details 액션에 액션을 더하는 것도 가능
     * @param go에 UI_EventHandler를 붙여 go가 이벤트 콜백을 받을 수 있도록 한다.
     */
    public static void BindEvent(GameObject go, Action<PointerEventData> action,
        Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }
    }
}
