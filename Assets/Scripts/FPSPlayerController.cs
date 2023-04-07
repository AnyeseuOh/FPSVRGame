using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FPSPlayerController : MonoBehaviour
{
    public GameObject gun;
    [Range(0, 2f)]
    public float duration;
    [Range(0, 2f)]
    public float strength;
    [Range(0, 5)]
    public int vib;
    [Range(0, 2f)]
    public float rnd;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveGun()
    {
        gun.transform.DOShakePosition(duration, strength, vib, rnd, false, true);
    }
}
