using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VplayerSetting : MonoBehaviour {

    MPlayerSetting setting;
    // Use this for initialization
    void Start()
    {
        setting = MPlayerSetting.GetIns();
    }
    public void SetGyroMul(float a) { setting.GyroMul = a; }
    public void SetTurnMul(float a) { setting.horizontalAimingSpeed = a; setting.verticalAimingSpeed = a; }
}
