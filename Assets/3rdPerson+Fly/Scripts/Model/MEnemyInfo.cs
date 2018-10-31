using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MEnemyInfo {
    private float hp = 100;
    private float HPMAX = 100;
    OneArg<float> num;
    public MEnemyInfo(float a)
    {
        HPMAX = a;
        hp = a;
        num = new OneArg<float>();
    }
    public event EventHandler<OneArg<float>> OnHpChange;
    public event EventHandler<OneArg<float>> Die;
    public void revive()
    {
        hp = HPMAX;
    }
    public void damage(float hurt)
    {
        hp -= hurt;
        num.Arg = hp/HPMAX;
        OnHpChange(this,num);
        if (hp <= 0)
            Die(this,null);
    }


}
