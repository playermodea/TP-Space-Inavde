using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerCtrl : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    //private AudioSource source = null;
    private AudioSource moveSource; // 움직일 때 효과음
    private AudioSource bgmSource;
    private Transform tr;
    private RaycastHit coll;

    public bool isStart = false;
    public float collDist = 3.0f;
    public float health = 100.0f;
    public float moveSpeed = 0.0f;
    public float rotSpeed = 100.0f;
    public GameObject expEffect;
    public GameObject[] particle;
    public Transform see;
    public AudioClip healthSound;
    public AudioClip ammoSound;

    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        moveSource = GetComponent<AudioSource>();
        bgmSource = AudioManager.instance.GetComponent<AudioSource>();
        moveSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Sfx")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            GameObject.Find("Planet").GetComponent<PlanetCtrl>().HP = 100;
        }

        if (isStart == false && Input.GetKey(KeyCode.F))
        {
            isStart = true;
            moveSource.Play(); // 움직이기 시작할 때 효과음 재생
            for (int i = 0; i < 4; i++)
            {
                particle[i].SetActive(true);
            }
            InGameUIManager.instance.UpdateState(0);
            moveSpeed = 100.0f;
        }
        else if (isStart == false && moveSpeed == 0.0f)
        {
            InGameUIManager.instance.UpdateState(6);
        }

        // UI가 활성화 상태일 때 배경음악 또는 플레이어가 움직일 때의 효과음 일시 정지
        if (InGameUIManager.instance.gameoverUI.activeSelf == true || InGameUIManager.instance.pauseUI.activeSelf == true || InGameUIManager.instance.clearUI.activeSelf == true)
        {
            if(InGameUIManager.instance.pauseUI.activeSelf == true)
            {
                bgmSource.Pause();
            }
            if (InGameUIManager.instance.clearUI.activeSelf == true)
            {
                moveSpeed = 0.0f;
                for (int i = 0; i < 4; i++)
                {
                    particle[i].SetActive(false);
                }
            }

            moveSource.Pause();
        }
        else if (!moveSource.isPlaying)
        {
            if (!bgmSource.isPlaying)
            {
                bgmSource.Play();
            }
            moveSource.Play();
        }

        if (Input.GetKey(KeyCode.Escape) && InGameUIManager.instance.clearUI.activeSelf == false)
            GameManager.instance.PauseGame();

        tr.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * h);
        tr.Rotate(Vector3.left * Time.deltaTime * rotSpeed * v);

        if (Input.GetKey(KeyCode.Q))
            tr.Rotate(Vector3.back * Time.deltaTime * rotSpeed);
        else if (Input.GetKey(KeyCode.E))
            tr.Rotate(Vector3.forward * Time.deltaTime * rotSpeed);

        if (Physics.Raycast(tr.position, tr.transform.forward, out coll, collDist))
        {
            if (coll.collider.tag == "ENEMY" || coll.collider.tag == "PLANET")
            {
                onDeath();
            }
        }

        if (health <= 0)
        {
            onDeath();
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "ENEMY_MISSILE")
        {
            health -= coll.gameObject.GetComponent<EnemyMissileCtrl>().Damage;
            InGameUIManager.instance.UpdateState(5);
            Debug.Log(health);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "HEALTHPACK")
        {
            //source.PlayOneShot(healthSound, 1.5f);
            AudioManager.instance.PlaySfx("healthSound", healthSound, 0.8f);
        }
        else if (coll.tag == "AMMOPACK")
        {
            //source.PlayOneShot(ammoSound, 1.5f);
            AudioManager.instance.PlaySfx("ammoSound", ammoSound, 0.8f);
        }
    }

    void onDeath()
    {
        moveSource.Stop();
        Instantiate(expEffect, tr.transform.position, Quaternion.identity);
        GameManager.instance.EndGame();
        Destroy(gameObject);
    }
}
