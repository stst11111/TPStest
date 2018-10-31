using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPlayerSetting  {
    private static MPlayerSetting ins;
    public static MPlayerSetting GetIns()
    {
        if (ins==null)
            ins = new MPlayerSetting();        
        return ins;
    }

    public float GyroMul = 2f;
    public float horizontalAimingSpeed = 0.3f;
    public float verticalAimingSpeed = 0.3f;
}
