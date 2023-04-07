using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ClaySpawner claySpawner;
    public Text scoreText;
    public int score;
    public bool isClicked = false;
    public bool isPauseClicked = false;
    public float curTime;
    public float coolTime = 2f;

    public GameObject UICanvas;
    public GameObject UICanvasPause;

    public Image loadingImg;
    public Image loadingImg_Pause;

    public void ScoreCounter()
    {
        score++;
        scoreText.text = $"SCORE : {score}";
    }

    public void StartGame()
    {
        claySpawner.spawnerSW = true;
        UICanvas.SetActive(false);
        UICanvasPause.SetActive(true);
        loadingImg.fillAmount = 0;
        score = 0;
        scoreText.text = "SCORE : 0";
    }

    public void PauseGame()
    {
        claySpawner.spawnerSW = false;
        UICanvas.SetActive(true);
        UICanvasPause.SetActive(false);
        loadingImg_Pause.fillAmount = 0;
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

    public void PauseBtnEnter()
    {
        isPauseClicked = true;
        loadingImg_Pause.enabled = true;
    }

    public void PauseBtnExit()
    {
        isPauseClicked = false;
        loadingImg_Pause.enabled = false;
        loadingImg_Pause.fillAmount = 0;
        curTime = 0;
    }

    void Update()
    {
        if (isClicked)
        {
            curTime += Time.deltaTime;
            loadingImg.fillAmount += (1f / coolTime * Time.deltaTime);
            if (curTime > coolTime)
            {
                StartGame();
                curTime = 0;
                isClicked = false;
            }
        }

        if (isPauseClicked)
        {
            curTime += Time.deltaTime;
            loadingImg_Pause.fillAmount += (1f / coolTime * Time.deltaTime);
            if (curTime > coolTime)
            {
                PauseGame();
                curTime = 0;
                isPauseClicked = false;
            }
        }
    }
}
