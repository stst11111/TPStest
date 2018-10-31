using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolMgr : MonoBehaviour {
    private Dictionary<GameObject, Objpool> poolDic;
    private static ObjPoolMgr ins;
    
    public static ObjPoolMgr GetInstance()
    {
        if (ObjPoolMgr.ins == null)
        {
            ObjPoolMgr.ins = new ObjPoolMgr();
            ins. poolDic = new Dictionary<GameObject, Objpool>();
        }

        return ObjPoolMgr. ins;
    }
    public Objpool AddPool(GameObject prefab,Transform parent,int amount)//新建一个key为prefab的pool
    {
        Objpool pool = new Objpool(prefab,parent,amount);
        poolDic.Add(prefab, pool);
        return pool;
    }
    public void RemovePool(GameObject name)
    {
        poolDic.Remove(name);
    }
    public void Clear()
    {
        poolDic.Clear();
    }


}
