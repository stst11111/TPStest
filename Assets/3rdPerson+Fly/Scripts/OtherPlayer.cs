using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class OtherPlayer : MonoBehaviour
{
    public GameObject gobj;
    public MOtherPlayerInfo Info;
    public string IP;
    public Vector3 pos, rot;
    //ani
    Animator ani;
    private int hFloat, vFloat,groundedBool;
    public Vector2 LeftJoy;
    public bool isgrounded=true;
    private Vector3 colExtents;                           // Collider extents for ground test. 
    public float speed;
    int speedFloat;

    //gun
    public GameObject gun;
    bool Shooting=false;
    Vector3 gunlocalrot;
    //OnGUI
    GameObject hpbar;
    // Use this for initialization
    void Awake()
    {
        Info = new MOtherPlayerInfo(100);
        ani = this.GetComponent<Animator>();
        hFloat = Animator.StringToHash("H");
        vFloat = Animator.StringToHash("V");
        groundedBool = Animator.StringToHash("Grounded");
        colExtents = GetComponent<Collider>().bounds.extents;
        speedFloat = Animator.StringToHash("Speed");
        gobj = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, pos, 0.2f);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rot),0.2f);
        ani.SetFloat(hFloat, LeftJoy.x, 0.1f, Time.deltaTime);
        ani.SetFloat(vFloat, LeftJoy.y, 0.1f, Time.deltaTime);
        ani.SetBool(groundedBool, IsGrounded());
        if(IsGrounded())
        { 
            ani.SetFloat(speedFloat, speed, 0.1f, Time.deltaTime);
        }
        if (Shooting)
        {
            if (!gun.activeInHierarchy)
                gun.SetActive(true);
            gun.transform.localEulerAngles = gunlocalrot;
        }
        else if(gun.activeInHierarchy)
        {
            gun.SetActive(false);
        }
    }
    public bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 2 * colExtents.x, Vector3.down);
        return Physics.SphereCast(ray, colExtents.x, colExtents.x + 0.2f);
    }
    public void UpdateHp(float f)
    {
        Info.sethp(f);
    }
    public void sethpbar(GameObject bar)
    {
        //绑定view model
        hpbar = bar;
        bar.SetActive(true);
        bar.GetComponent<VOtherPlayerOnGUI>().setdata(Info, gameObject);
        //Info.Die += die;
    }
    public void GunManage(Vector3 gunrot,bool shoot)
    {
        gunlocalrot = gunrot;
        Shooting = shoot;
    }

}
