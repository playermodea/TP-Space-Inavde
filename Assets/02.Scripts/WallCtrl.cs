using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCtrl : MonoBehaviour
{
    public Transform see;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {  
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "ENEMY" || coll.tag == "ROCK")
        {
            coll.transform.LookAt(see);
        }
        else if (coll.tag == "Player")
        {
            InGameUIManager.instance.UpdateState(1);
            coll.transform.LookAt(see);
        }
    }
}
