using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SkillSprint : MonoBehaviour {
    float speed=66f;
    float time = 0.1f;
    bool sprint = false;
	// Use this for initialization
	void Start () {
        PlayerInputManager.getins().But1Down += Sprint;
	}
	
	// Update is called once per frame
	void Update () {
        if (sprint)
            GetComponent<Rigidbody>().AddForce(transform.forward * speed,ForceMode.VelocityChange);
    }
    void Sprint(object a,EventArgs o)
    {
        sprint = true;
        Invoke("finish", time);
    }
    void finish()
    {       
        sprint = false;
    }
}
