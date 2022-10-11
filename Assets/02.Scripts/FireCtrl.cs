using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public GameObject missile;
    public Transform firePos_1;
    public Transform firePos_2;
    public Transform firePos_3;
    public AudioClip fireSfx;

    private float delayTime = 0.0f;
    public float attackDelay = 0.25f;
    public float tripleShootingGage = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<PlayerCtrl>().isStart == true)
        {
            delayTime += Time.deltaTime;

            if (Input.GetKey(KeyCode.Space) && delayTime >= attackDelay)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        CreateMissile();
        //source.PlayOneShot(fireSfx, 0.9f);
        //AudioManager.instance.PlayAudio("playerFire", "Sfx");
        AudioManager.instance.PlaySfx("playerFire", fireSfx, 0.5f);
    }

    void CreateMissile()
    {
        if (tripleShootingGage > 0.0f)
        {
            Instantiate(missile, firePos_1.position, firePos_1.rotation);
            Instantiate(missile, firePos_2.position, firePos_2.rotation);
            Instantiate(missile, firePos_3.position, firePos_3.rotation);

            tripleShootingGage -= 5.0f;
        }
        else
        {
            Instantiate(missile, firePos_3.position, firePos_3.rotation);
        }

        delayTime = 0.0f;
    }
}
