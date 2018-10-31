using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyCon : PoolPrefab {
    public MEnemyInfo data;
    Canvas charcanvas;
    GameObject hpbar;
    EnemySpawn spawner;
    
    public void sethpbar(GameObject a,EnemySpawn spawn)
    {
        spawner = spawn;
        hpbar = a;
        a.SetActive(true);
        //获得模型
        if (data == null)
            data = new MEnemyInfo(100);
        else
            data.revive();
        //绑定view model
        a.GetComponent<VenemyOnGUI>().setdata(data, gameObject);
        data.Die += Die;
    }
    void Die(object ob,EventArgs a)
    {
        spawner.EnemyDie(gameObject,hpbar);
    }
	//// Use this for initialization
	//void Start () {        
 //       Instantiate(hpbar,VCharactorCanvas.getins().transform).GetComponent<VenemyOnGUI>().setdata(data,gameObject);
	//}

}
