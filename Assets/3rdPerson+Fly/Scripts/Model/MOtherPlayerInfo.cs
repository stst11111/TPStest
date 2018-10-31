using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MOtherPlayerInfo
{
    private float hp = 100;
    private float HPMAX = 100;
    OneArg<float> num;
    public MOtherPlayerInfo(float a)
    {
        HPMAX = a;
        hp = a;
        num = new OneArg<float>();
    }
    public event EventHandler<OneArg<float>> OnHpChange;
    public event EventHandler<EventArgs> Die;
    public void revive()
    {
        hp = HPMAX;
    }
    public void sethp(float f)
    {
        hp =f;
        num.Arg = hp / HPMAX;
        OnHpChange(this, num);
        if (hp <= 0)
            Die(this, null);
    }


}
