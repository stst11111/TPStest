using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCharactorCanvas : MonoBehaviour {
    public static Canvas getins() { return ins.GetComponent<Canvas>(); }
    private static VCharactorCanvas ins;
    private void Awake()
    {
        ins = this;
    }
}
