using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaySpawner : MonoBehaviour
{
    public GameObject clayPrefab;
    public Transform spawnPos;
    public float curTime = 0;
    public float coolTime = 3f;
    public bool spawnerSW = false;

    void Update()
    {
        if (spawnerSW)
        {
            curTime += Time.deltaTime;
            if (curTime > coolTime)
            {
                Instantiate(clayPrefab, spawnPos.position, spawnPos.rotation);
                curTime = 0;
            }
        }
    }

}
