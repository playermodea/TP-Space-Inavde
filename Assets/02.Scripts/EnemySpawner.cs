using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int spawnCount = 3;
    public float spawnHP = 400;
    public GameObject enemyPrefab_1;
    public Transform[] spawnPoints;

    private void Update()
    {
        // 게임 오버 상태일때는 생성하지 않음
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        if (GameObject.Find("Planet").GetComponent<PlanetCtrl>().HP <= spawnHP)
        {
            SpawnWave();
            Debug.Log("Spawn Wave!");
            spawnHP -= 190;
        }
    }

    // 현재 웨이브에 맞춰 적을 생성
    private void SpawnWave()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            CreateEnemy();
        }
    }

    // 적을 생성
    private void CreateEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(enemyPrefab_1, spawnPoint.position, spawnPoint.rotation);
    }
}
