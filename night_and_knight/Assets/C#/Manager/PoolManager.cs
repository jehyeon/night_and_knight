using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief Pool 객체들을 관리하는 Manager
 * @details ResourceManager를 보조
 */
public class PoolManager
{
    private Dictionary<string, Pool> mPoolDic = new Dictionary<string, Pool>();
    private Transform mRoot;

    public void Init()
    {
        if (mRoot == null)
        {
            mRoot = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(mRoot);
        }
    }
    
    /**
     * @param original의 Pool을 count만큼 생성
     */
    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = mRoot;
        
        mPoolDic.Add(original.name, pool);
    }
    
    /**
     * @param 다 사용한 poolable오브젝트를 Pool에 다시 넣어 대기 상태로 전환
     */
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
    
    /**
     * @param original의 이름에 해당하는 Pool을 Pop한 후 parent를 부모 오브젝트로 설정
     * @return Pool로부터 사용할 Poolable 리턴
     */
    public PoolAble Pop(GameObject original, Transform parent = null)
    {
        if(mPoolDic.ContainsKey(original.name) == false)
            CreatePool(original);

        return mPoolDic[original.name].Pop(parent);
    }
    
    /**
     * @param name에 해당하는 원본 GameObject에 접근
     * @return 원본 GameObject를 리턴
     */
    public GameObject GetOriginal(string name)
    {
        if (mPoolDic.ContainsKey(name) == false)
            return null;
        
        return mPoolDic[name].Original;
    }
    
    public void Clear()
    {
        foreach (Transform child in mRoot)
            Object.Destroy(child.gameObject);

        mPoolDic.Clear();
    }
}
