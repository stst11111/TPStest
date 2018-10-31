using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objpool : MonoBehaviour {
    public GameObject Prefab;
    public int PrefabAmount=1;
    private Queue<GameObject> pool;
    public Objpool(GameObject p,Transform parent,int amount)
    {
        pool = new Queue<GameObject>();
        Prefab = Instantiate(p,parent);
        pool.Enqueue(Prefab);
        Prefab.GetComponent<PoolPrefab>().Pool = this;
        if(Prefab.activeSelf)
            Prefab.SetActive(false);
        PrefabAmount = amount;
        while (pool.Count < amount)
        {
            GameObject a= Instantiate(Prefab,parent);
            pool.Enqueue(a);
        }
    }

    /// <summary>
    /// 提取对象池中对象
    /// </summary>
    /// <param name="t"></param>
    /// <param name="recycletime"></param>
    /// <returns></returns>
    public GameObject Ins(Transform t,float recycletime=0f)
    {
        if (pool.Count >=2)
        {
            GameObject a= pool.Dequeue();
            a.GetComponent<PoolPrefab>().Pool = this;
            a.transform.parent = t;
            a.GetComponent<PoolPrefab>().recycletime = recycletime;
            a.SetActive(true);
            return a;
        }
        GameObject b= Instantiate(Prefab,t);
        b.GetComponent<PoolPrefab>().recycletime = recycletime;
        b.GetComponent<PoolPrefab>().Pool = this;
        b.SetActive(true);
        if (PrefabAmount < pool.Count)
            PrefabAmount = pool.Count;
        return b;
    }

    public GameObject Ins(Transform t,Vector3 pos,Quaternion rot,float recycletime=0f)
    {
        if (pool.Count >= 2)
        {
            GameObject a = pool.Dequeue();
            a.GetComponent<PoolPrefab>().Pool = this;
            a.transform.parent = t;
            a.transform.position = pos;
            a.transform.rotation = rot;
            a.SetActive(true);
            a.GetComponent<PoolPrefab>().recycletime = recycletime;
            return a;
        }
        GameObject b = Instantiate(Prefab,pos,rot,t);
        b.GetComponent<PoolPrefab>().recycletime = recycletime; 
        b.GetComponent<PoolPrefab>().Pool = this;
        b.SetActive(true);
        if (PrefabAmount < pool.Count)
            PrefabAmount = pool.Count;
        return b;
    }
    public void Recycle(GameObject p)
    {
        p.SetActive(false);
        
        pool.Enqueue(p);
    }

}
