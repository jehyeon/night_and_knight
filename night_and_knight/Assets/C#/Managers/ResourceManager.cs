using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    // Resource 폴더를 시작 위치로 한 "path"에 해당하는 T 타입의 에셋 파일을 로드 후 반환
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
    
    // "path"에 해당하는 에셋을 GameObject타입으로 반환
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

    // GameObject 제거
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