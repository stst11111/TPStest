using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletSpawn : MonoBehaviour {
    Objpool op;
    public GameObject bullet;
    Transform maincam;
    bool shooting = false;
    GameObject AimmingTargrt = null;

    public GameObject shootcircle;
    public float dispersion = 0;
    public float delay = 0;
	// Use this for initialization
	void Start () {
        op = ObjPoolMgr.GetInstance().AddPool(bullet ,null,1);
        //InvokeRepeating("SpawnBullet", 0.1f,0.1f);
        maincam = Camera.main.gameObject.transform;
        CursorManager.getins().OnChangeTarget += OnChangeTarget;
        PlayerInputManager.getins().Joy0Right += SwitchState;
	}
	
	void SpawnBullet () { 
        GameObject bullettemp= op.Ins(null,transform.position,transform.rotation,1);
        //散射
        bullettemp.transform.eulerAngles += new Vector3(UnityEngine.Random.Range(-dispersion,dispersion), UnityEngine.Random.Range(-dispersion, dispersion),0);
        //设置跟踪
        if (AimmingTargrt)
        {
            bullettemp.GetComponent<BulletCon>().SetTarget(AimmingTargrt,delay);
        }
	}

    private void Update()
    {
        transform.rotation = maincam.transform.rotation;
        if (shooting)
        {
            UDPCNS.getins().dpsend.setgunrot(transform.localEulerAngles);
        }
    }

    void SwitchState(object a,EventArgs aa)
    {
        shooting = !shooting;
        if (shooting)
        {
            shootcircle.SetActive(true);
            InvokeRepeating("SpawnBullet", 0.1f, 0.1f);
        }
        else
        {
            shootcircle.SetActive(false);
            CancelInvoke();
        }
        UDPCNS.getins().dpsend.shooting = shooting;
    }

    void OnChangeTarget(object sender, OneArg<GameObject> a)
    {
        AimmingTargrt = a.Arg;
    }
}
