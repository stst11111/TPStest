using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System;

/// <summary>
/// 无服务器局域网玩家类
/// </summary>
public class UDPCNS : MonoBehaviour
{
    //singleton
    private static UDPCNS ins;
    public static UDPCNS getins()
    { return ins; }

    public GameObject otherplayer;//prefab
    public String MyIP;
    public Dictionary<string, OtherPlayer> PlayerList;
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
    }// 获取本机IP
    Socket socket, sock,sockethurt; //接受，目标socket，伤害数据包目标
    public DgramPlayer dpsend;//自己发送的数据包
    DgramPlayer dpget;//获取的数据报   
    IPEndPoint ipEnd; //侦听端口  
    EndPoint ep;
    EndPoint clientEnd; //其他客户端 



    string AddIP;
    JoinPlayer currentjoin;
    //test
    string editstring;
    byte[] recvData = new byte[1024]; //接收的数据，必须为字节 
    byte[] sendData = new byte[1024]; //发送的数据，必须为字节 
    int recvLen; //接收的数据长度 
    Thread connectThread; //连接线程
    Vector3 pos, rot;

    //初始化
    void InitSocket()
    {
        currentjoin = new JoinPlayer();
        dpsend = new DgramPlayer(transform);
        MyIP = GetIPAddress();
        dpsend.IP = MyIP;
        PlayerList = new Dictionary<string, OtherPlayer>();     
   
        //发送
        sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//初始化一个Scoket实习,采用UDP传输
        clientEnd = new IPEndPoint(IPAddress.Broadcast, 9095);//初始化一个发送广播和指定端口的网络端口实例
        sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);//设置该scoket实例的发送形式



        //接受
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//初始化一个Scoket协议
        ipEnd = new IPEndPoint(IPAddress.Any, 9095);//初始化一个侦听局域网内部所有IP和指定端口
        ep = (EndPoint)ipEnd;
        socket.Bind(ipEnd);//绑定这个实例

        print("waiting for UDP dgram");

        //开启一个线程连接
        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();
        InvokeRepeating("SocketSend", 0, 0.04f);
    }

    /// <summary>
    /// 发送
    /// </summary>
    void SocketSend()
    {
        //清空发送缓存
        sendData = new byte[1024];
        //数据类型转换
        dpsend.set(transform);
        dpsend.HP = MPlayerInfo.getins().hp;
        sendData = ConvertHelper.StructureToByte(dpsend);
        //发送给指定服务端
        sock.SendTo(sendData, clientEnd);

    }

    /// <summary>
    /// 伤害数据包
    /// </summary>
    /// 
    public void SocketSendHurt(string hurtIP)
    {
        //清空发送缓存
        sendData = new byte[1024];
        //数据类型转换
        sendData = ConvertHelper.StructureToByte(dpsend);
        //发送给指定IP
        dpsend.damageIP = hurtIP;
        sock.SendTo(sendData, clientEnd);
        dpsend.damageIP = null;
        dpsend.damage = 0;

    }

    /// <summary>
    /// 接收
    /// </summary>
    void SocketReceive()
    {
        //进入接收循环
        while (true)
        {           
            //对data清零
            recvData = new byte[1024];
            //获取客户端，获取客户端数据，用引用给客户端赋值
            recvLen = socket.ReceiveFrom(recvData, ref ep);
            //接收到一个包
            dpget = ((DgramPlayer)ConvertHelper.ByteToStructure(recvData));
            if (dpget.IP == MyIP)
            {                
                continue;
            }
            else if (PlayerList.ContainsKey(dpget.IP))
            {
                //伤害数据包
                if (dpget.damage != 0&&MyIP==dpget.damageIP)
                {
                    
                    MPlayerInfo.getins().damage(dpget.damage);                    
                }
                else
                {
                    //解析
                    OtherPlayer temp = PlayerList[dpget.IP];
                    GameObject p = temp.gobj;
                    temp.pos = dpget.getpos();
                    temp.rot = dpget.getrot();
                    temp.speed = dpget.speed;
                    //hp
                    temp.UpdateHp(dpget.HP);
                    //gun
                    temp.GunManage(dpget.getgunrot(),dpget.shooting);
                }

            }
            else
            {
                //加入     
                //由于主线程才能使用unity的api，把添加用户的逻辑交给update;
                lock(currentjoin)
                {
                    currentjoin.join = true;
                    currentjoin.IP = dpget.IP;
                }
                
            }
        }
    }

    //连接关闭
    void SocketQuit()
    {
        //关闭线程
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //最后关闭socket
        if (socket != null)
            socket.Close();
        print("disconnect");
    }

    private void Awake()
    {
        ins = this;
        new MPlayerInfo();
    }

    // Use this for initialization
    void Start()
    {
        InitSocket(); //在这里初始化server
    }

    private void Update()
    {
        lock (currentjoin)
        {
            if (currentjoin.join)
            {
                currentjoin.join = false;
                GameObject p = Instantiate(otherplayer);
                PlayersManager.getins().NewPlayer(p);
                lock (PlayerList)
                {
                    PlayerList.Add(currentjoin.IP, p.GetComponent<OtherPlayer>());
                }
                p.GetComponent<OtherPlayer>().IP = currentjoin.IP;
                print(currentjoin.IP);
            }
        }
     
    }
//    //test
//string editString;
//    void OnGUI()
//    {
        
//         editString = GUI.TextField(new Rect(10, 10, 100, 20), editString);
//        GUI.TextField(new Rect(10, 30, 222, 222), MyIP);
//    }
//    void OnApplicationQuit()
//    {
//        SocketQuit();
//    }


}

public class JoinPlayer
{
    public bool join = false;
    public string IP;
}

