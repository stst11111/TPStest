using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class VPlayerInfo : MonoBehaviour
{
    public Image HpBar;
    public Text Hp;
    private MPlayerInfo Info;
    bool reviving=false;
    // Use this for initialization
    void Start()
    {
        Info = MPlayerInfo.getins();
        MPlayerInfo.getins().Revive += revive;
    }
    private void Update()
    {
        HpBar.fillAmount = Info.hp/ Info.HPMAX;
        Hp.text = (Info.hp).ToString();             
        if(reviving)
        {
            //test
            print(2);
            UDPCNS.getins().gameObject.transform.position = new Vector3(-15, 2, -29);
            reviving = false;
        }
    }

    /// <summary>
    /// 改变血条
    /// </summary>
    /// <param name="from"></param>
    /// <param name="a"></param>
    //float dama=1;
    //void hpchange(object from, OneArg<float> a)
    //{
    //    dama = a.Arg;
    //}
    void revive(object from, EventArgs a)
    {
        reviving = true;       
    }
}
