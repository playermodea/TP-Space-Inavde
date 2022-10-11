using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Ctrl : MonoBehaviour
{
    private float delayTime = 0.0f;
    private float patrolTime = 0.0f;
    private Transform enemyTr;
    private RaycastHit coll;
    private RaycastHit collPlayer;

    public int score = 0;
    public int hp = 100;
    public float collDist = 30.0f;
    public float attackDelay = 1.0f;
    public float rotSpeed = 100.0f;
    public float attackDist = 500.0f;
    public float moveSpeed = 50.0f;
    public GameObject enemyMissile;
    public GameObject expEffect;
    public Transform playerTr;
    public Transform firePos_1;
    public Transform firePos_2;
    public Transform firePos_3;
    public AudioClip fireSfx;
    public enum EnemyState { idle, attack };
    public EnemyState enemyState = EnemyState.idle;

    // Start is called before the first frame update
    void Start()
    {
        enemyTr = GetComponent<Transform>();
        playerTr = GameObject.Find("targetTr").GetComponent<Transform>();
        InGameUIManager.instance.UpdateEnemyText(0, 1);
        StartCoroutine(this.CheckEnemyState());
        StartCoroutine(this.EnemyAction());
    }

    // Update is called once per frame
    void Update()
    {
        delayTime += Time.deltaTime;
        patrolTime += Time.deltaTime;

        if (InGameUIManager.instance.clearUI.activeSelf == true)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator CheckEnemyState()
    {
        while (GameManager.instance.isGameover == false)
        {
            yield return new WaitForSeconds(0.2f);

            Vector3 relativePos;
            float dist = Vector3.Distance(playerTr.position, enemyTr.position);

            if (dist <= attackDist)
            {
                relativePos = GameObject.Find("Player").GetComponent<Transform>().position - enemyTr.transform.position;
                relativePos = relativePos.normalized;

                if (Physics.Raycast(enemyTr.transform.position, relativePos, out collPlayer))
                {
                    if (collPlayer.collider.tag == "Player")
                    {
                        enemyState = EnemyState.attack;
                    }
                }
            }
            else
            {
                enemyState = EnemyState.idle;
                //Debug.Log("Check idle");
            }
        }
    }

    IEnumerator EnemyAction()
    {
        while (GameManager.instance.isGameover == false)
        {
            Vector3 relativePos;
            Quaternion targetRotation;

            switch (enemyState)
            {
                case EnemyState.idle:
                    onPatrol();
                    break;
                case EnemyState.attack:
                    relativePos = playerTr.position - firePos_1.transform.position;
                    targetRotation = Quaternion.LookRotation(relativePos);
                    transform.rotation = Quaternion.RotateTowards(enemyTr.rotation, targetRotation, Time.deltaTime * rotSpeed);
                    if (delayTime > attackDelay)
                        onFire();
                    break;
            }
            yield return null;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "MISSILE")
        {
            hp -= coll.gameObject.GetComponent<MissileCtrl>().Damage;

            if (hp <= 0)
            {
                InGameUIManager.instance.UpdateState(4);
                InGameUIManager.instance.UpdateEnemyText(1, -1);
                GameManager.instance.AddScore(score);
                Instantiate(expEffect, enemyTr.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    void OnPlayerDie()
    {
        StopAllCoroutines();
    }

    void onFire()
    {
        //source.PlayOneShot(fireSfx, 0.3f);
        //AudioManager.instance.PlayAudio("enemyFire", "Sfx");
        AudioManager.instance.PlaySfx("enemyFire", fireSfx, 0.3f);
        Instantiate(enemyMissile, firePos_1.position, firePos_1.rotation);
        Instantiate(enemyMissile, firePos_2.position, firePos_2.rotation);
        Instantiate(enemyMissile, firePos_3.position, firePos_3.rotation);
        delayTime = 0;
    }

    void onPatrol()
    {
        if (Physics.Raycast(firePos_1.position, firePos_1.transform.forward, out coll, collDist))
        {
            if (coll.collider.tag == "ROCK" || coll.collider.tag == "ENEMY" || coll.collider.tag == "PLANET")
            {
                enemyTr.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                //Debug.Log("Reverse!");
            }
        }

        if (patrolTime > 10.0f)
        {
            enemyTr.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            patrolTime = 0.0f;
        }
        enemyTr.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
