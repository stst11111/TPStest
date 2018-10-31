using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class EnemySpawn : MonoBehaviour
{
    //test
    public GameObject prefab;
    public GameObject enemygui;
    Objpool op,opgui;

    public void EnemyDie(GameObject enemy,GameObject enemygui)
    {
        opgui.Recycle(enemygui);
        op.Recycle(enemy);
        Invoke("SpawnEnemy", 2);
    }

    void SpawnEnemy()
    {
        GameObject a= op.Ins(transform,transform.position,transform.rotation);
        GameObject b= opgui.Ins( VCharactorCanvas.getins().transform);
        a.GetComponent<EnemyCon>().sethpbar(b,this);
    }
    void StopSpawning()
    {
        CancelInvoke();
    }

    // Use this for initialization
    void Start()
    {
        //test
        op = ObjPoolMgr.GetInstance().AddPool(prefab,transform,5);
        opgui = ObjPoolMgr.GetInstance().AddPool(enemygui, transform, 5);
        InvokeRepeating("SpawnEnemy", 0.1f,1f);
        Invoke("StopSpawning", 9);
    }
    //void OnGUI()
    //{
    //    GUILayout.Space(50);

    //    GUILayout.Label("Total DrawCall: " + UnityEditor.UnityStats.drawCalls, GUILayout.Width(500));
    //    GUILayout.Label("Batch: " + UnityEditor.UnityStats.batches, GUILayout.Width(500));
    //    GUILayout.Label("Static Batch DC: " + UnityEditor.UnityStats.staticBatchedDrawCalls, GUILayout.Width(500));
    //    GUILayout.Label("Static Batch: " + UnityEditor.UnityStats.staticBatches, GUILayout.Width(500));
    //    GUILayout.Label("DynamicBatch DC: " + UnityEditor.UnityStats.dynamicBatchedDrawCalls, GUILayout.Width(500));
    //    GUILayout.Label("DynamicBatch: " + UnityEditor.UnityStats.dynamicBatches, GUILayout.Width(500));

    //    GUILayout.Label("Tri: " + UnityEditor.UnityStats.triangles, GUILayout.Width(500));
    //    GUILayout.Label("Ver: " + UnityEditor.UnityStats.vertices, GUILayout.Width(500));
    //}

}
