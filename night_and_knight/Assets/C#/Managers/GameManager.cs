using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief 전역으로 게임 전체와 다른 Manager를 관리하는 Manager
 * @details 싱글톤 패턴 적용
 * @see asdf
 */
public class GameManager : MonoBehaviour
{
    private static GameManager sInstance;
    public static GameManager Instance { get { Init(); return sInstance; } }
    
    private InputManager mInputMng = new InputManager();
    private ResourceManager mResourceMng = new ResourceManager();
    private PoolManager mPoolMng = new PoolManager();
    private UIManager mUIMng = new UIManager();
    
    public static InputManager InputMng => Instance.mInputMng;
    public static ResourceManager ResourceMng => Instance.mResourceMng;
    public static PoolManager PoolMng => Instance.mPoolMng;
    public static UIManager UIMng => Instance.mUIMng;


    void Start()
    {
        Init();
    }
    
    void Update()
    {
        mInputMng.OnUpdate();
    }

    static void Init()
    {
        if (sInstance == null)
        {
            GameObject go = GameObject.Find("@GameManager");
            if (go == null)
            {
                go = new GameObject { name = "@GameManager" };
                go.AddComponent<GameManager>();
            }
            
            DontDestroyOnLoad(go);
            sInstance = go.GetComponent<GameManager>();
        }

        sInstance.mPoolMng.Init();
    }
    
    /**
     * @brief GameManager Clear
     */
    public static void Clear()
    {
        PoolMng.Clear();
    }

    
}

/* 사용예제
    void Start()
    {
        GameManager gm = GameManager.Instance;
    }
*/
