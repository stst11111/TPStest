using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class CursorManager : MonoBehaviour {

    Camera maincam;
    Vector2 midpoint;
    Color currentcolor;
    int currentCursor=1;
    GameObject AimmingTarget=null;
    static CursorManager ins;
    OneArg<GameObject> gobj;//封装目标对象

    public static CursorManager getins() { return ins; }
    public GameObject[] Cursors;
    /// <summary>
    /// 目标变化时触发
    /// </summary>
    public event EventHandler<OneArg<GameObject>> OnChangeTarget;
    
    /// <summary>
    /// 转换准心
    /// </summary>
    /// <param name="i"></param>
    void SwitchCursor(int i)
    {
        Cursors[currentCursor].SetActive(false);
        currentCursor = i;
        Cursors[i].SetActive(true);
    }
	// Use this for initialization
	void Awake () {
        ins = this;
        maincam = Camera.main;
        midpoint = new Vector2(Screen.width / 2, Screen.height / 2);
        currentcolor = Color.white;
        gobj = new OneArg<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = maincam.ScreenPointToRay(midpoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.layer == 9)
        {
            
            if (AimmingTarget == null)
            {
                AimmingTarget = hit.collider.gameObject;
                gobj.Arg = AimmingTarget;
                OnChangeTarget(this,gobj);
                Cursors[currentCursor].GetComponent<Image>().color = Color.red;
            }
            
            
        }
        else
        {
            
            if (AimmingTarget != null)
            {
                AimmingTarget = null;
                Cursors[currentCursor].GetComponent<Image>().color = currentcolor;
                gobj.Arg = null;
                OnChangeTarget(this, gobj);
            }
            
            
        }
	}

    
}
