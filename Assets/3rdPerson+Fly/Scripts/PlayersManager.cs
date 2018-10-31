using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayersManager : MonoBehaviour {


    private static PlayersManager ins;
    public static PlayersManager getins()
    {
        return ins;
    }
    public GameObject otherplayergui;
    //test
    public GameObject prefab;

    // Use this for initialization
    void Start () {
        ins = this;
        //test
        //NewPlayer(Instantiate(prefab));
	}

    public void NewPlayer(GameObject player)
    {
        player.GetComponent<OtherPlayer>().sethpbar(Instantiate(otherplayergui, VCharactorCanvas.getins().transform));
    }
	// Update is called once per frame
	void Update () {
		//if(Input.GetKeyDown(KeyCode.Q))
  //      {
  //          MPlayerInfo.getins().damage(5);
  //      }
	}
}
