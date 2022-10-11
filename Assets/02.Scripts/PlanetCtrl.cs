using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCtrl : MonoBehaviour
{
    public float HP = 600.0f;
    public int score = 50;

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            GameObject.Find("Enemy Spawner").SetActive(false);
            GameManager.instance.ClearGame();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "MISSILE")
        {
            GameManager.instance.AddScore(score);
            HP -= coll.gameObject.GetComponent<MissileCtrl>().Damage;
            Debug.Log(HP);
        }
    }
}