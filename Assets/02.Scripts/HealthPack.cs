using UnityEngine;

// 체력을 회복하는 아이템
public class HealthPack : MonoBehaviour{
    public float health = 50.0f; // 체력을 회복할 수치

    private void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            InGameUIManager.instance.UpdateState(2);
            coll.gameObject.GetComponent<PlayerCtrl>().health += health;
            if (coll.gameObject.GetComponent<PlayerCtrl>().health > 100)
                coll.gameObject.GetComponent<PlayerCtrl>().health = 100;
            Debug.Log(coll.gameObject.GetComponent<PlayerCtrl>().health);
            Destroy(gameObject);
        }
    }
}