using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pool
{
    private Stack<PoolAble> mPoolStack = new Stack<PoolAble>();

    public GameObject Original { get; private set; }
    public Transform Root { get; set; }

    public void Init(GameObject original, int count = 5)
    {
        Original = original;
        Root = new GameObject().transform;
        Root.name = $"{Original.name}_Root";

        for (int i = 0; i < count; i++)
            Push(Create());
    }

    PoolAble Create()
    {
        GameObject go = Object.Instantiate(Original);
        go.name = Original.name;
        return go.GetOrAddComponent<PoolAble>();
    }

    // Pool에 Push (오브젝트 비활성화)
    public void Push(PoolAble poolAble)
    {
        if (poolAble == null)
            return;
        poolAble.transform.parent = Root;
        poolAble.gameObject.SetActive(false);
        poolAble.IsUsing = false;
        
        mPoolStack.Push(poolAble);
    }

    // Pool에서 Pop (오브젝트 활성화)
    public PoolAble Pop(Transform parent)
    {
        PoolAble poolAble;
        
        if (mPoolStack.Count > 0)
            poolAble = mPoolStack.Pop();
        else
        {
            poolAble = Create();
        }
        
        poolAble.gameObject.SetActive(true);
        
        // DontDestroyOnLoad 해제 용도
        // if (parent == null)
        //     poolAble.transform.parent = GameManager.SceneMng.CurrentScene.transform;

        poolAble.transform.parent = parent;
        poolAble.IsUsing = true;

        return poolAble;
    }
}