using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    private Dictionary<string, Pool> mPoolDic = new Dictionary<string, Pool>();
    private Transform mRoot;

    public void Init()
    {
        if (mRoot == null)
        {
            mRoot = new GameObject { name = "Pool_Root" }.transform;
            Object.DontDestroyOnLoad(mRoot);
        }
    }
    
    // Pool 생성
    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = mRoot;
        
        mPoolDic.Add(original.name, pool);
    }
    
    // 다 사용한 오브젝트를 Pool에 다시 넣어 대기 상태로 전환
    public void Push(PoolAble poolAble)
    {
        string name = poolAble.gameObject.name;
        if (mPoolDic.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolAble.gameObject);
            return;
        }
        
        mPoolDic[name].Push(poolAble);
    }

    // Pool로부터 사용할 오브젝트 반환
    public PoolAble Pop(GameObject original, Transform parent = null)
    {
        if(mPoolDic.ContainsKey(original.name) == false)
            CreatePool(original);

        return mPoolDic[original.name].Pop(parent);
    }
    
    // 원본 프리팹 반환
    public GameObject GetOriginal(string name)
    {
        if (mPoolDic.ContainsKey(name) == false)
            return null;
        
        return mPoolDic[name].Original;
    }
    
    // Pool 오브젝트 Clear
    public void Clear()
    {
        foreach (Transform child in mRoot)
            Object.Destroy(child.gameObject);

        mPoolDic.Clear();
    }
}
