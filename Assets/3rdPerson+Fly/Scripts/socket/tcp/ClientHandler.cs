using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading;

public class ClientHandler : MonoBehaviour
{
    private static ClientHandler ins;
    public static ClientHandler getins()
    {
        return ins;
    }
    public bool chatting=false;
    public void ChatWindowSwitch() { chatting =! chatting;print(chatting); }
    private int rowcount = 0;
    private int rowmax = 10;
    //private Text ChatText;
    //public void SetText(Text t) { ChatText = t; }
	private TcpClient _client;
	byte[] data;
    public string Ip = "127.0.0.1";
    public int portNo = 500;
    public string NetName = "Client_01";
	public string message = "";
	public string sendMsg = "";
    public string netMsg = null;

    void Start()
    {
        ins = this;
        //创建TCPclient
        this._client = new TcpClient();
        //避免画面卡死
        Thread ConnectThread = new Thread(ConnectToServer);
        ConnectThread.Start();
        message += "Loading...\n";
    }

    void ConnectToServer()
    {
        //Thread.Sleep(2000);
        //对服务器端口提出TCP连接申请
        this._client.Connect(Ip, portNo);
        data = new byte[this._client.ReceiveBufferSize];
        //连接后发名字
        SendMessage(NetName);
        //开始异步读基础数据流(比特流，，长度，回调函数，)
        this._client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(this._client.ReceiveBufferSize), ReceiveMessage, null);
    }
    /// <summary>
    /// 对话框显示
    /// </summary>
    void OnGUI()
	{
        if (chatting)
        {
            message = GUI.TextArea(new Rect(10, 40, 300, 200), message);
            sendMsg = GUI.TextField(new Rect(10, 250, 210, 20), sendMsg);
            if (GUI.Button(new Rect(230, 250, 80, 20), "Send"))
		    {
			    SendMessage(sendMsg);
			    sendMsg = "";
		    };
        }
    }

    /// <summary>
    /// 发送数据流
    /// </summary>
    /// <param name="message"></param>
	//public void SendMessage(string message)
	//{
	//	try
	//	{
	//		NetworkStream ns = this._client.GetStream();
	//		byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
	//		ns.Write(data, 0, data.Length);
	//		ns.Flush();
	//	}
	//	catch (Exception ex)
	//	{
 //           Debug.LogError(ex.Message);
 //       }
	//}

    /// <summary>
    /// 接收数据流
    /// </summary>
    /// <param name="ar"></param>
	public void ReceiveMessage(IAsyncResult ar)
	{
		try
		{
			int bytesRead;
            //结束读取
			bytesRead = this._client.GetStream().EndRead(ar);
			if (bytesRead < 1)
			{
				return;
			}
			else
			{
				Debug.Log(System.Text.Encoding.ASCII.GetString(data, 0, bytesRead));
                netMsg= System.Text.Encoding.ASCII.GetString(data, 0, bytesRead);;
			    message += netMsg;
                netMsg = null;
                rowcount++;
                if (rowcount > rowmax)
                {
                    int i = message.IndexOf('\n');
                    message= message.Remove(0,i+1);
                    rowcount--;
                }
                //netMsg = netMsg.Replace("[", "").Replace("]", "");
			    //Debug.LogWarning("接收到服务器的数据信息："+ netMsg);
			}
            //开始继续读取
			this._client.GetStream().BeginRead(data, 0, System.Convert.ToInt32(this._client.ReceiveBufferSize), ReceiveMessage, null);
        }
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
		}
    }


}