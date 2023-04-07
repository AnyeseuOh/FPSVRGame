using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        None = -1,
        Idle = 0,
        Walk,
        Attack,
        Damage,
        Dead
    }
    [Header("에너미 상태")]
    public EnemyState enemyState;
    private NavMeshAgent nvAgent;
    public Animator anim;

    [Header("Enemy - Robot")]
    [Range(0, 5f)]
    public float enemySpeed = 2f;
    public float stateTime;
    public float idleStateTime = 2f;
    public float attackStateTime = 1.5f;
    public float damageStateTime = 1.5f;
    public float atkRange = 3.5f;
    public int enemyHp = 2;

    [Header("LockOn")]
    public bool isLockedOn = false;
    public float curTime;
    public float coolTime = 2f;
   

    [Header("타겟 플레이어")]
    public Transform target;

    [Header("이펙트, 아이템")]
    public GameObject hitEffect;
    public GameObject item;

    public FPSPlayerController fpsPController;

    void Start()
    {
        nvAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        fpsPController = GameObject.Find("GUN").GetComponent<FPSPlayerController>();

        enemyState = EnemyState.Idle;

    }

    
    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.None:
                break;

            case EnemyState.Idle:
                anim.SetInteger("EnemyState", 0);

                stateTime += Time.deltaTime;
                if (stateTime > idleStateTime)
                {
                    stateTime = 0;
                    enemyState = EnemyState.Walk;
                }

                
                break;

            case EnemyState.Walk:
                anim.SetInteger("EnemyState", 1);

                nvAgent.speed = enemySpeed;
                float distance = Vector3.Distance(target.position, transform.position);
                if (distance < atkRange)
                {
                    enemyState = EnemyState.Attack;
                }
                else
                {
                    nvAgent.speed = enemySpeed;
                }

                nvAgent.SetDestination(target.position);                
                break;

            case EnemyState.Attack:
                anim.SetInteger("EnemyState", 2);

                nvAgent.speed = 0;
                stateTime += Time.deltaTime;
                if (stateTime > attackStateTime)
                {
                    stateTime = 0;
                    Debug.Log("ATTACK!");
                }

                break;

            case EnemyState.Damage:
                anim.SetInteger("EnemyState", 3);

                nvAgent.speed = 0;
                stateTime += Time.deltaTime;
                if (stateTime > damageStateTime)
                {
                    stateTime = 0;
                    enemyState = EnemyState.Walk;
                }
                if (enemyHp <= 0)
                {
                    enemyState = EnemyState.Dead;
                }
                break;

            case EnemyState.Dead:
                Debug.Log("Dead 모션 실행");
                anim.SetTrigger("Dead");
                enemyState = EnemyState.None;
                Destroy(gameObject, 2f);
                break;
        }

        if (isLockedOn)
        {
            curTime += Time.deltaTime;
            if (curTime > coolTime)
            {
                curTime = 0;
                Debug.Log("DamageByPlayer 함수 호출");
                DamageByPlayer();
                fpsPController.MoveGun();
                Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(hitEffect, 3f);
                enemyState = EnemyState.Damage;
            }
        }

    }

    public void AimEnter()
    {
        isLockedOn = true;
        Debug.Log("조준점 진입");
    }

    public void AimExit()
    {
        isLockedOn = false;
        curTime = 0;
        Debug.Log("조준점 벗어남");
    }

    void DamageByPlayer()
    {
        --enemyHp;
        if (enemyHp <= 0)
        {
            enemyState = EnemyState.Dead;
            Destroy(gameObject, 3f);
        }
    }
}
