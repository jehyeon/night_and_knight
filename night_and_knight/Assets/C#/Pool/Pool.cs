using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/**
 * @brief Pooling한 GameObject들을 Stack으로 관리
 */
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

    /**
     * @return 원본 GameObject로부터 풀링에 사용할 오브젝트를 생성 후 리턴
     */
    PoolAble Create()
    {
        GameObject go = Object.Instantiate(Original);
        go.name = Original.name;
        return go.GetOrAddComponent<PoolAble>();
    }
    
    /**
     * @param poolAble를 mPoolStack에 넣어주기 (오브젝트 비활성화)
     */
    public void Push(PoolAble poolAble)
    {
        if (poolAble == null)
            return;
        poolAble.transform.parent = Root;
        poolAble.gameObject.SetActive(false);
        poolAble.IsUsing = false;
        
        mPoolStack.Push(poolAble);
    }
    
    /**
     * @param mPoolStack로부터 꺼내와서 parent를 부모 오브젝트로 설정 (오브젝트 활성화)
     */
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
        
        if (parent == null) 
            poolAble.transform.parent = GameManager.SceneMng.CurrentScene.transform;

        poolAble.transform.parent = parent;
        poolAble.IsUsing = true;

        return poolAble;
    }
}
