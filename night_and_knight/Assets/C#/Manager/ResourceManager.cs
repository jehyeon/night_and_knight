using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief 에셋 파일을 로드하는 Manager
 */
public class ResourceManager
{
    /**
     * @param Resources폴더를 시작 위치로 path에 해당하는 에셋 파일 로드
     * @return 로드한 파일을 T타입으로 리턴
     */
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = GameManager.PoolMng.GetOriginal(name);
            if (go != null)
                return go as T;
        }
        
        return Resources.Load<T>(path);
    }
    
    /**
     * @param Prefabs폴더를 시작 위치로 path에 해당하는 GameObject 생성
     * @param 부모 오브젝트를 parent 로 설정
     * @return 생성된 GameObject 리턴
     */
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        if (original.GetComponent<PoolAble>() != null)
            return GameManager.PoolMng.Pop(original, parent).gameObject;

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        
        return go;
    }
    
    /**
     * @param go 제거
     */
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        PoolAble poolAble = go.GetComponent<PoolAble>();
        if (poolAble != null)
        {
            GameManager.PoolMng.Push(poolAble);
            return;
        }
        
        Object.Destroy(go);
    }
}

/* 사용예제
    void Start()
    {
        GameObject mTemp = GameManager.Resource.Instantiate("Temp");
        GameManager.Resource.Destroy(mTemp);
    }
*/
