using UnityEngine;
using System.Collections;

public class PoolPrefab : MonoBehaviour
{
    public Objpool Pool;
    public float recycletime = 0f;
    
    //如果设定了回归时间，按时回归，否则主动调用Recycle()
    virtual protected void Recycle()
    {
        Pool.Recycle(gameObject);
    }

    virtual protected void OnEnable()
    {
        if (recycletime > 0.01f)
            Invoke("Recycle",recycletime);
    }

}
