using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private static PlayerInputManager ins;
    public static PlayerInputManager getins() { return ins; }
    //allowturing
    public bool allowturning=true;

    //leftjoy
    OneArg<Vector2> _leftjoy;
    public event EventHandler<OneArg<Vector2>> LeftJoy;
    //public event EventHandler<EventArgs> OnLeftJoyUp;
    public void Joy(Vector2 j)
    {
        _leftjoy.Arg = j;
        LeftJoy(this, _leftjoy);
    }
    public void LeftJoyUp() { Joy(Vector2.zero); }
    // Use this for initialization
    void Awake()
    {
        ins = this;
        _leftjoy = new OneArg<Vector2>(Vector2.zero);
    }
    //四相选择遥感
    public event EventHandler<EventArgs> Joy0Up;
    public event EventHandler<EventArgs> Joy0Down;
    public event EventHandler<EventArgs> Joy0Right;
    public event EventHandler<EventArgs> Joy0Left;
    public event EventHandler<EventArgs> Joy0mid;
    Vector2 Joy0Pos=Vector2.zero;
    public void Joy0(Vector2 j)
    {
        if(allowturning)
            allowturning = false;
        Joy0Pos = j;
    }
    public void OnJoy0Up()
    {
        allowturning = true;
        if (Joy0Pos.magnitude < 0.3f)
            Joy0mid(null, null);
        else if (Joy0Pos.y >= Mathf.Abs(Joy0Pos.x))
            Joy0Up(null, null);
        else if (Joy0Pos.y <= -Mathf.Abs(Joy0Pos.x))
            Joy0Down(null, null);
        else if (Joy0Pos.x >= Mathf.Abs(Joy0Pos.y))
            Joy0Right(null, null);
        else
            Joy0Left(null, null);
    }
    //单触按键
    public event EventHandler<EventArgs> But0Down;
    public event EventHandler<EventArgs> But1Down;
    public event EventHandler<EventArgs> But2Down;
    public void but0down()
    {
        But0Down(null, null);
    }
    public void but1down()
    {
        But1Down(null, null);
    }
    public void but2down()
    {
        But2Down(null, null);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
