using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCtrl : MonoBehaviour
{
    private Transform tr;
    private int hitCount = 0;

    public int hit = 4;
    public int score = 40;
    public float damage = 40.0f;
    public GameObject[] items;
    public GameObject expEffect;

    // Start is called before the first frame update

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InGameUIManager.instance.clearUI.activeSelf == true)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "MISSILE" || coll.collider.tag == "ENEMY_MISSILE")
        {
            //Debug.Log("HIT");
            //Debug.Log(hitCount);
            Destroy(coll.gameObject);

            if (++hitCount == hit)
            {
                ExpRock();

                if (coll.collider.tag == "MISSILE")
                    GameManager.instance.AddScore(score);
            }
        }
        else if (coll.collider.tag == "Player")
        {
            InGameUIManager.instance.UpdateState(5);
            coll.gameObject.GetComponent<PlayerCtrl>().health -= damage;
            Instantiate(expEffect, tr.position, Quaternion.identity);
            //Debug.Log(coll.gameObject.GetComponent<PlayerCtrl>().health);
            Destroy(gameObject);
        }
        else if (coll.collider.tag == "ENEMY")
        {
            Instantiate(expEffect, tr.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void ExpRock()
    {
        Instantiate(expEffect, tr.position, Quaternion.identity);
        GameObject selectedItem = items[Random.Range(0, items.Length)];
        GameObject item = Instantiate(selectedItem, tr.transform.position, Quaternion.identity);
        Debug.Log("Destroy");

        Destroy(gameObject);
    }
}
