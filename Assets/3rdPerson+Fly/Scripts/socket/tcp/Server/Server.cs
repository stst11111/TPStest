using UnityEngine;
using System.Collections;

public class Server : MonoBehaviour
{
    private string outInfo;
    private int rowcount = 0;
    private int rowmax = 10;
    public void Log(string s)
    {
        outInfo += s;
        outInfo += "\n";
        rowcount++;
        if (rowcount > rowmax)
        {
            int i = outInfo.IndexOf('\n');
            outInfo = outInfo.Remove(0, i + 1);
            rowcount--;
        }
    }
    //单例
    private static Server Instance;
    public static Server GetInstance()
    {
        return Instance;
    }

    void Start()
    {
        Instance = this;
		Debug.Log(Application.persistentDataPath);
		Game.SocketManager.GetInstance().Start();
    }
    private void OnGUI()
    {
        outInfo = GUI.TextArea(new Rect(10, 40, 300, 200), outInfo);
    }
}