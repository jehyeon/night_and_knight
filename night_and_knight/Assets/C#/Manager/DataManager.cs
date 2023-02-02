using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IData<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Stat> StatDic { get; private set; }

    public void Init()
    {
        StatDic = LoadJson<StatData, int, Stat>("StatData").MakeDict();
    }

    /**
     * @param path 위치의 Json 파일을 TextAsset 타입으로 로드
     * @return 로드한 데이터를 Data 타입의 객체로 리턴
     */
    Data LoadJson<Data, Key, Value>(string path) where Data : IData<Key, Value>
    {
        TextAsset textAsset = GameManager.ResourceMng.Load<TextAsset>($"Datas/{path}");
        
        return JsonUtility.FromJson<Data>(textAsset.text);
    }
}
