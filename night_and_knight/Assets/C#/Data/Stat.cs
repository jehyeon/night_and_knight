using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    public int id;
    public int hp;
    public int damage;
}

[Serializable]
public class StatData : IData<int, Stat>
{
    public List<Stat> stats = new List<Stat>();
    public Dictionary<int, Stat> MakeDict()
    {
        Dictionary<int, Stat> dic = new Dictionary<int, Stat>();
        foreach (Stat stat in stats)
            dic.Add(stat.id, stat);
        
        return dic;
    }
}