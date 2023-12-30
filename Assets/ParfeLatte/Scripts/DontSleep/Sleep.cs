using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    private int CurSleep;
    private float Timer;
    private float SleepTime;

    private int point;
    
    // Start is called before the first frame update
    void Start()
    {
        CurSleep = 1;
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
                SleepTime = 1f;
                break;
            case 2:
                SleepTime = Random.Range(0.5f, 3f);
                break;
            case 3:
                SleepTime = 1f;
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
        ChangeSleepTime();
    }

    private void WakeUpByOther()
    {
        //애니메이션
        Debug.Log("허접");
        CurSleep = 1;
        Timer = 0f;
    }
    
    private void WakeUpByMe()
    {
        point++;
        if(point == 5)
        {
            GameManager.instance.ClearGame();
        }
        //애니메이션
        Debug.Log("니가 깨웠어");
        CurSleep = 1;
        Timer = 0f;
        ChangeSleepTime();
    }
}
