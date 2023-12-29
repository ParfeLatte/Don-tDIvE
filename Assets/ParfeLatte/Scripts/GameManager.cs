using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float Timer;
    public float OverTime;
    public float GameSpeed;
    public int GameLevel;

    public GameObject ClearScene;
    public GameObject explainScene;
    public GameObject GameOverScene;
    public GameObject CurrentGame;
    public List<GameObject> CurgameList = new List<GameObject>();//게임에서 사용할 미니게임 리스트
    public List<GameObject> AllGames = new List<GameObject>();//전체 게임 리스트
    public int Life;

    public short point;
    public bool isGame;

    protected void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        isGame = false;
        OverTime = 5f;
        GameReset();
        SetGame();
        SetNextGame();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimer();
        if (isGame && Input.GetKeyDown(KeyCode.Space))
        {
            ClearGame();
        }
    }

    public void GameReset()
    {
        GameLevel = 0;
        GameSpeed = 1f;
    }

    //리스트에 모든 미니게임 삽입 및 게임 시작
    public void SetGame()
    {
        CurgameList.Clear();
        for (int i = 0; i < AllGames.Count; i++)
        {
            CurgameList.Add(AllGames[i]);
            AllGames[i].SetActive(false);
        }
    }

    //게임 클리어시에 성공씬 보여주기 -> 게임 셋
    public IEnumerator ShowClearScene()
    {
        ClearScene.SetActive(true);
        yield return new WaitForSeconds(1f);
        ClearScene.SetActive(false);
        SetNextGame();
    }
    
    //미니게임결정, 다음 게임 실행
    public void SetNextGame()
    {
        int num;
        num = Random.Range(0, CurgameList.Count);
        CurrentGame = CurgameList[num];
        CurgameList.Remove(CurrentGame);
        //CurrentGame.SetActive(true);
        StartCoroutine(ShowExplainScene()); //게임 결정 했으니 설명
    }

    //게임 설명 보여주고 게임으로 넘어감
    public IEnumerator ShowExplainScene() 
    {
        explainScene.SetActive(true);//설명 2초간 보여주고 넘어감
        yield return new WaitForSeconds(2f);
        explainScene.SetActive(false);
        StartGame();
    }
   
    public void StartGame()
    {
        Timer = 0f;
        CurrentGame.SetActive(true);
        isGame = true;
    }

    public void CheckTimer()
    {
        if (!isGame) return;
        Timer += Time.deltaTime;
        if(Timer >= OverTime)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        CurrentGame.SetActive(false);
        isGame = false;
        GameOverScene.SetActive(true);
        Debug.Log("Fail");
    }

    //게임 클리어 조건을 달성할시 클리어 화면을 보여주고 다음 게임으로 진행
    public void ClearGame()
    {
        Debug.Log("Clear!");
        CurrentGame.SetActive(false);
        isGame = false;
        point += 5;
        if (point >= 87)
        {
            //클리어엔딩
        }
        else
        {
            if (CurgameList.Count == 0)
            {
                UpdateGameLevel();
            }//게임 리스트가 비어있으면 모든 미니게임을 클리어 한 것 이므로 리스트 초기화 및 게임 선택
            StartCoroutine(ShowClearScene());//계속 진행
        }
    }

    public void UpdateGameLevel()
    {
        GameLevel++; 
        GameSpeed += 0.05f * GameLevel;
        SetGame();
    }
}

//미니 게임별로 설명 갱신해주어야함 -> UI 매니저