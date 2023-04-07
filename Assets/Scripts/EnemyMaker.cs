using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPos;
    public bool isStarted = false;
    public float curTime;
    public float coolTime = 5f;


    void Start()
    {
        spawnPos = GameObject.Find("SpawnPos").GetComponentsInChildren<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            curTime += Time.deltaTime;
            if (curTime > coolTime)
            {
                curTime = 0;
                MakeEnemy();
            }
        }
    }

    public void MakeEnemy()
    {
        int rnd = Random.Range(1, spawnPos.Length);
        Instantiate(enemyPrefab, spawnPos[rnd].position, transform.rotation);
    }
}
