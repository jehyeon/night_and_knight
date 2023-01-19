using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager sInstance;
    public static GameManager Instance { get { Init(); return sInstance; } }
    
    private InputManager mInputMng = new InputManager();
    private ResourceManager mResourceMng = new ResourceManager();
    private PoolManager mPoolMng = new PoolManager();
    
    public static InputManager InputMng => Instance.mInputMng;
    public static ResourceManager ResourceMng => Instance.mResourceMng;
    public static PoolManager PoolMng => Instance.mPoolMng;


    void Start()
    {
        Init();
        Test();
    }
    
    void Update()
    {
        mInputMng.OnUpdate();
    }

    static void Init()
    {
        if (sInstance == null)
        {
            GameObject go = GameObject.Find("GameManager");
            if (go == null)
            {
                go = new GameObject { name = "GameManager" };
                go.AddComponent<GameManager>();
            }
            
            DontDestroyOnLoad(go);
            sInstance = go.GetComponent<GameManager>();
        }

        sInstance.mPoolMng.Init();
    }
    
    // GameManager Clear
    public static void Clear()
    {
        PoolMng.Clear();
    }

    public void Test()
    {
        List<GameObject> go = new List<GameObject>();
        for (int i = 0; i < 2; i++)
            go.Add(mResourceMng.Instantiate("Cube"));
    }
}

/* 사용예제
    void Start()
    {
        GameManager gm = GameManager.Instance;
    }
*/
