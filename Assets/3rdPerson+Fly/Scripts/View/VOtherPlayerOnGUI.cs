﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VOtherPlayerOnGUI :MonoBehaviour
{
    MOtherPlayerInfo data;
    public GameObject target;
    Image HpBar;
    Vector3 OnScreen;
    Camera maincam;
    float targetFillamount=1;
    /// <summary>
    /// 设置对应数据与目标对象
    /// </summary>
    /// <param name="a"></param>
    /// <param name="t"></param>
    public void setdata(MOtherPlayerInfo a, GameObject t)
    {
        maincam = Camera.main;
        data = a;
        data.OnHpChange += hpchange;
        target = t;
        HpBar = this.gameObject.GetComponent<Image>();
        gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        OnScreen = maincam.WorldToScreenPoint(target.transform.position);
        if (targetFillamount != HpBar.fillAmount)
        {
            HpBar.color = Color.red;
            HpBar.fillAmount = targetFillamount;
        }
        HpBar.color = Color.Lerp(HpBar.color, Color.white, 0.1f);
        if (OnScreen.x < Screen.width && OnScreen.x > 0 && OnScreen.y < Screen.height && OnScreen.y > 0 && OnScreen.z > 0)
        //在屏幕内
        {
            Ray r = maincam.ScreenPointToRay(OnScreen);
            RaycastHit[] hits = Physics.RaycastAll(r, Vector3.Distance(target.transform.position, maincam.gameObject.transform.position));
            foreach (RaycastHit hit in hits)
            {
                GameObject hitobj = hit.transform.gameObject;
                if (hitobj.layer != 8 && hitobj.layer != 9 && hitobj != target && hitobj.layer != 12)
                //如果有障碍物，不显示
                {
                    HpBar.enabled = false;
                    return;
                }
            }
            //显示图标
            if (!HpBar.enabled)
            {
                HpBar.enabled = true;
            }

            HpBar.rectTransform.position = maincam.WorldToScreenPoint(target.transform.position + 2*Vector3.up);

            return;
        }
        HpBar.enabled = false;

    }

    /// <summary>
    /// 改变血条
    /// </summary>
    /// <param name="from"></param>
    /// <param name="a"></param>
    void hpchange(object from, OneArg<float> a)
    {
        targetFillamount = a.Arg;
    }


}
