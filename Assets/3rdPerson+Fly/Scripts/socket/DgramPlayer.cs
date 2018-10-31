using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

[Serializable]
public class DgramPlayer
{
    //位置信息
    public float[] trans;
    public DgramPlayer(Transform t)
    {
        //DamageList = new Dictionary<string, float>();
        //设置位置信息
        trans = new float[6];
        trans[0] = t.position.x;
        trans[1] = t.position.y;
        trans[2] = t.position.z;
        trans[3] = t.eulerAngles.x;
        trans[4] = t.eulerAngles.y;
        trans[5] = t.eulerAngles.z;
        //gunrot
        gunlocalrot = new float[3];
        //设置标识ip
        IP = GetIPAddress();
    }
    public void set(Transform t)
    {       
        trans[0] = t.position.x;
        trans[1] = t.position.y;
        trans[2] = t.position.z;
        trans[3] = t.eulerAngles.x;
        trans[4] = t.eulerAngles.y;
        trans[5] = t.eulerAngles.z;
    }
    public Vector3 getpos()
    {
        return new Vector3(trans[0], trans[1], trans[2]);
    }
    public Vector3 getrot()
    {
        return new Vector3(trans[3], trans[4], trans[5]);
    }
    //gun
    public bool shooting = false;
    public float[] gunlocalrot;
    public void setgunrot(Vector3 r)
    {
        gunlocalrot[0] = r.x;
        gunlocalrot[1] = r.y;
        gunlocalrot[2] = r.z;            
    }
    public Vector3 getgunrot()
    {
        return new Vector3(gunlocalrot[0], gunlocalrot[1], gunlocalrot[2]);
    }
    //ip
    public string IP;
    public string damageIP;
    //anim
    public float speed=0f;
    // 获取本机IP
    private String GetIPAddress()
    {
        String str;
        String Result = "";
        String hostName = Dns.GetHostName();
        IPAddress[] myIP = Dns.GetHostAddresses(hostName);
        foreach (IPAddress address in myIP)
        {
            str = address.ToString();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= '0' && str[i] <= '9' || str[i] == '.') Result = str;
            }
        }
        return Result;
    }
    //public Dictionary<string, float> DamageList;
    public float HP;
    public float damage = 0f;
}
//[Serializable]
//[StructLayout(LayoutKind.Sequential, Pack = 1)]//按1字节对齐
//public struct DgramPlayer
//{
//public float[] trans;
//public DgramPlayer(Transform t)
//{
//    trans = new float[6];
//    trans[0] = t.position.x;
//    trans[1] = t.position.y;
//    trans[2] = t.position.z;
//    trans[3] = t.eulerAngles.x;
//    trans[4] = t.eulerAngles.y;
//    trans[5] = t.eulerAngles.z;
//}
//public void set(Transform t)
//{
//    trans = new float[6];
//    trans[0] = t.position.x;
//    trans[1] = t.position.y;
//    trans[2] = t.position.z;
//    trans[3] = t.eulerAngles.x;
//    trans[4] = t.eulerAngles.y;
//    trans[5] = t.eulerAngles.z;
//}
//public Vector3 getpos()
//{
//    return new Vector3(trans[0], trans[1], trans[2]);
//}
//public Vector3 getrot()
//{
//    return new Vector3(trans[3], trans[4], trans[5]);
//}

//}