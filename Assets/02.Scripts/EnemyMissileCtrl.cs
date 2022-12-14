using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileCtrl : MonoBehaviour
{
    public int Damage = 20;
    public float speed = 30000.0f;
    public GameObject expEffect;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        Invoke("DestroyMissile", 1.0f);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag != "ENEMY")
        {
            DestroyMissile();
        }
    }

    void DestroyMissile()
    {
        Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
