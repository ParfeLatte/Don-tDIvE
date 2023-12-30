using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    private Animator anim;
    [Header("Enemy")]
    public SpriteRenderer EnemySpr;
    public Sprite EnemyIdle;
    public Sprite EnemyPyon;
    [Header("player")]
    public SpriteRenderer PlayerSpr;
    public Sprite PlayerIdle;
    public Sprite PlayerPyon;

    private int CurSleep;
    private float Timer;
    private float SleepTime;

    private int point;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        CurSleep = 1;
        anim.SetInteger("Sleep", CurSleep);
        Timer = 0;
        point = 0;
        ChangeSleepTime();
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (CurSleep == 3 && InputManager.instance.Space)
        {
            WakeUpByMe();
        }
        if(Timer >= SleepTime)
        {
            CheckTime();
        }
    }

    private void ChangeSleepTime()
    {
        switch (CurSleep)
        {
            case 1:
                SleepTime = 1.5f;
                break;
            case 2:
                SleepTime = Random.Range(0.5f, 4f);
                break;
            case 3:
                SleepTime = 0.7f;
                Debug.Log("지금이니");
                break;
        }
    }

    private void CheckTime()
    {
        switch (CurSleep)
        {
            case 1:
                CurSleep = 2;
                Timer = 0f;
                break;
            case 2:
                CurSleep = 3;
                Timer = 0f;
                break;
            case 3:
                WakeUpByOther();
                break;
        }
        anim.SetInteger("Sleep", CurSleep);

        ChangeSleepTime();
    }

    private void WakeUpByOther()
    {
        EnemySpr.sprite = EnemyPyon;
        StartCoroutine(EnemySprReturn());
        Debug.Log("허접");
        CurSleep = 1;
        Timer = 0f;
        
    }

    private IEnumerator EnemySprReturn()
    {
        yield return new WaitForSeconds(0.5f);
        EnemySpr.sprite = EnemyIdle;
    }
    
    private void WakeUpByMe()
    {
        point++;
        if(point == 5)
        {
            GameManager.instance.ClearGame();
            return;
        }
        //애니메이션
        PlayerSpr.sprite = PlayerPyon;
        StartCoroutine(PlayerSprReturn());
        Debug.Log("니가 깨웠어");
        CurSleep = 1;
        anim.SetInteger("Sleep", CurSleep);
        Timer = 0f;
        ChangeSleepTime();
    }

    private IEnumerator PlayerSprReturn()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerSpr.sprite = PlayerIdle;
    }
}
