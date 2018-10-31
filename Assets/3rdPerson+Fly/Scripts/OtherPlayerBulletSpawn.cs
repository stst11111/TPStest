using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OtherPlayerBulletSpawn : MonoBehaviour {

    Objpool op;
    public GameObject bullet;

    public float dispersion = 0;
    float lastshoottime=0;
    // Use this for initialization
    void Start()
    {
        op = ObjPoolMgr.GetInstance().AddPool(bullet, null, 1);
    }
    private void Update()
    {
        if (Time.time >= lastshoottime+0.1f)
        {
            SpawnBullet();
            lastshoottime = Time.time;
        }
    }
    void SpawnBullet()
    {
        GameObject bullettemp = op.Ins(null, transform.position, transform.rotation, 1);
        //散射
        bullettemp.transform.eulerAngles += new Vector3(UnityEngine.Random.Range(-dispersion, dispersion), UnityEngine.Random.Range(-dispersion, dispersion), 0);
    }

}
