using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief 게임 씬 상에서 생길 여러가지 UI 캔버스 프리팹들의 생성과 삭제를 관리
 */
public class UIManager
{
    private int mOrder = 10;
    
    private Stack<UI_Popup> mPopupStack = new Stack<UI_Popup>();
    private UI_Scene mSceneUI = null;

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
     * @param go의 캔버스 컴포넌트 가져와 sort order값 세팅
     */
    public void SetCanvas(GameObject go, bool sort = true)
    {
        
    }
}
