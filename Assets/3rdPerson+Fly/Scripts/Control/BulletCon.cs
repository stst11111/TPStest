using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCon : PoolPrefab {
    //Vector3 lastposition;
    //RaycastHit hit;
    GameObject target = null;
    float FollowDelay = 0;
    float LautchTime;
    float destroyradius = 0.5f;


    public void SetTarget(GameObject a =null,float delay=0f)
    {
        FollowDelay = delay;
    }
	// Use this for initialization
	void Start () {
        LautchTime = Time.time;
	}

    private void FixedUpdate()
    {
        //跟踪
        if (target && Time.time > LautchTime + FollowDelay)
        {
            transform.LookAt(target.transform);
        }
           
        //lastposition = transform.position;
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * 55);

        //if (Physics.Raycast(lastposition, transform.position, out hit, Vector3.Distance(lastposition, transform.position)))
        //{
        //    //print(hit.collider.gameObject);
        //    if (hit.collider.gameObject.layer == 9)
        //    {
        //        hit.collider.gameObject.GetComponent<EnemyCon>().data.damage(5);
        //    }
        //    if(target)
        //        target = null;
        //    if(hit.collider.gameObject.layer != 8)
        //        Recycle();
        //}

        if (target && Vector3.Distance(target.transform.position, transform.position) < destroyradius)
        {
            target.GetComponent<EnemyCon>().data.damage(5);
            if (target)
                target = null;
            Recycle();
        }
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 9)
        {
            if (collider.gameObject.GetComponent<EnemyCon>())
                collider.gameObject.GetComponent<EnemyCon>().data.damage(5);
            else
            {
                lock (UDPCNS.getins().dpsend)
                {
                    UDPCNS.getins().dpsend.damage = 5;
                    UDPCNS.getins().dpsend.damageIP = collider.gameObject.GetComponent<OtherPlayer>().IP;
                    UDPCNS.getins().SocketSendHurt(collider.gameObject.GetComponent<OtherPlayer>().IP);                          
                }

            }
        }
        if (target)
            target = null;
        if (collider.gameObject.layer != 8)
            Recycle();
    }

}
