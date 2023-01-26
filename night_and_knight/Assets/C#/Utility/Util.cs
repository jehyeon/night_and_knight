using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * @brief 추가기능 메소드를 지원하는 클래스
 */
public class Util : MonoBehaviour
{
    /**
     * @param go에 T컴포넌트가 있다면 가져오고, 없다면 생성
     * @return 가져온/생성한 T컴포넌트 리턴
     */
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        
        return component;
    }

    /**
     * @param go의 모든 자식들 중 T컴포넌트를 가지며, name과 이름이 일치하는 GameObject 검색
     * @param recursive가 true라면 바로 밑 자식 뿐 아니라 손자, 증손자, ... 모두 검색, false라면 자식만 검색
     * @return 검색한 GameObject를 T컴포넌트로 리턴
     */
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    /**
     * @param FindChild<Transform>에 파라미터를 그대로 넣어서 실행하여 gameObject 검색
     * @return 해당 GameObject 리턴
     */
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }
}
