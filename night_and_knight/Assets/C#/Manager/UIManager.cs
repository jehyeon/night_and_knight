using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private int mOrder = 10; // 현재까지 최근에 사용한 오더
    
    private UI_Scene mSceneUI; // 현재의 고정 캔버스 UI
    private Stack<UI_Popup> mPopupStack = new Stack<UI_Popup>(); // 팝업 캔버스 UI Stack

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };

            return root;
        }
    }

    /**
     * @param sort가 true면, go의 Canvas 컴포넌트를 가져와 mOrder값을 1더해서 설정 (PopupUI)
     * @param sort가 false면 go의 Canvas 컴포넌트를 가져와 mOrder값을 0으로 설정 (SceneUI)
     */
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true; // 부모 캔버스와는 독립적인 오더값을 가짐

        if (sort)
        {
            canvas.sortingOrder = mOrder++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    /**
     * @param 이름이 name인 SceneUI 생성
     * @return 생성한 sceneUI T컴포넌트로 리턴
     */
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GameManager.ResourceMng.Instantiate($"UI/SceneUI/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        mSceneUI = sceneUI;

        go.transform.SetParent(Root.transform);
        
        return sceneUI;
    }
    
    /**
     * @param 이름이 name인 PopupUI 생성
     * @return 생성한 PopupUI T컴포넌트로 리턴
     */
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GameManager.ResourceMng.Instantiate($"UI/PopupUI/{name}");
        T popupUI = Util.GetOrAddComponent<T>(go);
        mPopupStack.Push(popupUI);
        
        go.transform.SetParent(Root.transform);
        
        return popupUI;
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GameManager.ResourceMng.Instantiate($"UI/SubItemUI/{name}");
        
        if (parent != null)
            go.transform.SetParent(parent);
        
        return go.GetOrAddComponent<T>();
    }
    
    /**
     * @brief 가장 Order가 높은 PopupUI 제거
     */
    public void ClosePopupUI()
    {
        if (mPopupStack.Count == 0)
            return;

        UI_Popup popupUI= mPopupStack.Pop();
        GameManager.ResourceMng.Destroy(popupUI.gameObject);
        popupUI = null;
        mOrder--;
    }
    
    /**
     * @param 가장 Order가 높은 PopupUI 확인 후 제거
     */
    public void ClosePopupUI(UI_Popup popup)
    {
        if (mPopupStack.Count == 0)
            return;

        if (mPopupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed");
            return;
        }
        
        ClosePopupUI();
    }
    
    /**
     * @brief 모든 PopupUI 제거
     */
    public void CloseAllPopupUI()
    {
        while (mPopupStack.Count > 0)
        {
            ClosePopupUI();
        }
    }
    
    public void Clear()
    {
        CloseAllPopupUI();
        mSceneUI = null;
    }
}
