using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSGameMgr : MonoBehaviour
{
    public EnemyMaker enemyMaker;
    public bool isClicked = false;
    public Image loadingImg;
    public float curTime;
    public float coolTime = 2f;

    public GameObject startCanvas;

    void Start()
    {
        enemyMaker = GameObject.Find("SpawnPos").GetComponent<EnemyMaker>();
    }

    void Update()
    {
        if (isClicked)
        {
            curTime += Time.deltaTime;
            loadingImg.fillAmount += (1f / coolTime * Time.deltaTime);

            if (curTime > coolTime)
            {
                curTime = 0;
                StartGame();
                isClicked = false;
            }
        }
    }

    public void StartGame()
    {
        enemyMaker.isStarted = true;
        startCanvas.SetActive(false);
        loadingImg.fillAmount = 0;
    }

    public void StartBtnEnter()
    {
        isClicked = true;
        loadingImg.enabled = true;
    }

    public void StagtBtnExit()
    {
        isClicked = false;
        loadingImg.enabled = false;
        loadingImg.fillAmount = 0;
        curTime = 0;
    }
}
