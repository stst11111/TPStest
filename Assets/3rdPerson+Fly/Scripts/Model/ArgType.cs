using UnityEngine;
using UnityEditor;
using System;

delegate void EventHandler(object sender, EventArgs e);
//public class AFloat : EventArgs
//{
//    public AFloat() { num = 0; }
//    public AFloat(float a) { num = a; }
//    public float num { set; get; }
//}
//public class AGameobj : EventArgs
//{
//    public AGameobj() { gobj = null; }
//    public AGameobj(GameObject a) { gobj = a; }
//    public GameObject gobj { get; set; }
//}

public class OneArg<T> : EventArgs
{
    public OneArg() { }
    public OneArg(T a) { Arg = a; }
    public T Arg { get; set; }
}