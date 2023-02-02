using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/**
 * @brief 전역으로 게임 전체와 다른 Manager를 관리하는 Manager
 * @details 싱글톤 패턴 적용
 */
public class GameManager : MonoBehaviour
{
    private static GameManager sInstance;
    public static GameManager Instance { get { Init(); return sInstance; } }

    private DataManager mDataMng = new DataManager();
    private InputManager mInputMng = new InputManager();
    private PoolManager mPoolMng = new PoolManager();
    private ResourceManager mResourceMng = new ResourceManager();
    private SceneManagerEx mSceneMng = new SceneManagerEx();
    private SoundManager mSoundMng = new SoundManager();
    private UIManager mUIMng = new UIManager();

    public static DataManager DataMng => Instance.mDataMng;
    public static InputManager InputMng => Instance.mInputMng;
    public static PoolManager PoolMng => Instance.mPoolMng;
    public static ResourceManager ResourceMng => Instance.mResourceMng;
    public static SceneManagerEx SceneMng => Instance.mSceneMng;
    public static SoundManager SoundMng => Instance.mSoundMng;
    public static UIManager UIMng => Instance.mUIMng;


    private void Start()
    {
        Init();
    }
    
    private void Update()
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

            sInstance.mDataMng.Init();
            sInstance.mSoundMng.Init();
            sInstance.mPoolMng.Init();
        }
    }
    
    public static void Clear()
    {
        InputMng.Clear();
        SoundMng.Clear();
        SceneMng.Clear();
        UIMng.Clear();
        PoolMng.Clear();
    }
}
