using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBulletCon : PoolPrefab {

    private void FixedUpdate()
    {       
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * 55);
    }

    private void OnTriggerEnter(Collider collider)
    {
        print(collider.gameObject);
        Recycle();
    }
}
