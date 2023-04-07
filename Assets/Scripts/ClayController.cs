using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class ClayController : MonoBehaviour
{
    private Rigidbody rigd;
    public float pwd = 100;
    private bool isLockedOn = false;
    public float curTime = 0;
    public float coolTime = 0.5f;
    public GameObject explosionEffect;

    public Text scoreText;
    public ClaySpawner claySpawner;

    public GameManager gameMgr;
    
    void Start()
    {
        rigd = GetComponent<Rigidbody>();
        rigd.velocity = transform.forward * pwd;
        gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
        Destroy(gameObject, 10f);
    }

    public void AimEnter()
    {
        //Debug.Log("조준점 진입");
        isLockedOn = true;
    }

    public void AimExit()
    {
        //Debug.Log("조준점 벗어남");
        isLockedOn = false;
    }
    
    void Update()
    {
        if (isLockedOn)
        {
            curTime += Time.deltaTime;
            if (curTime > coolTime)
            {
                Instantiate(explosionEffect, transform.position, transform.rotation);
                gameMgr.ScoreCounter();
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
