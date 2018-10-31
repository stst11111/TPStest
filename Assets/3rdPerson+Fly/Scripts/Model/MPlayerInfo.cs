using UnityEngine;
using UnityEditor;
using System;

public class MPlayerInfo 
{
    //singleton
    private static MPlayerInfo ins;
    public static MPlayerInfo getins()
    {
        return ins;
    }
    public MPlayerInfo()
    {
        ins = this;
        //bloodpercent = new OneArg<float>(1f);
    }
    //event
    //OneArg<float> bloodpercent;
    //public event EventHandler<OneArg<float>> OnHpChange;
    public event EventHandler<EventArgs> Revive;
    //Info
    public float hp=100;
    public float HPMAX = 100;
    public void damage(float hurt)
    {
        hp -= hurt;
        if (hp < 0)
            hp = 0;
        //bloodpercent.Arg = hp / HPMAX;
        //OnHpChange(this, bloodpercent);
        if (hp <= 0)
        {
            Revive(null,null);
            revive();
        }
    }
    public void revive()
    {
        hp = HPMAX;
    }
}