using UnityEngine;

// 총알을 충전하는 아이템
public class AmmoPack : MonoBehaviour {
    public float charging = 100.0f;

    private void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            InGameUIManager.instance.UpdateState(3);
            coll.gameObject.GetComponent<FireCtrl>().tripleShootingGage = charging;
            Debug.Log(coll.gameObject.GetComponent<FireCtrl>().tripleShootingGage);
            Destroy(gameObject);
        }
    }
}