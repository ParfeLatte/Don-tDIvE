using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    //public float Timer;
    //public float OverTime;
    //public float GameSpeed;
    //public int GameLevel;
    public GameObject PointUI;
    public Text pointtext;

    public GameObject GameScene;
    public GameObject ClearScene;
    public GameObject explainScene;
    public GameObject GameOverScene;
    public GameZip CurrentGame;
    public List<GameZip> gameList = new List<GameZip>();//게임에서 사용할 미니게임 리스트
    //public List<GameObject> AllGames = new List<GameObject>();//전체 게임 리스트
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
        PointUI.SetActive(false);
        isGame = false;
        //OverTime = 10f;
        GameReset();
        //SetGame();
        SetNextGame();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckTimer();
        //if (isGame && Input.GetKeyDown(KeyCode.Space))
        //{
        //    ClearGame();
        //}
    }

    public void GameReset()
    {
        point = 0;
        pointtext.text = "x" + point.ToString();
    }

    //리스트에 모든 미니게임 삽입 및 게임 시작
    //public void SetGame()
    //{
    //    CurgameList.Clear();
    //    for (int i = 0; i < AllGames.Count; i++)
    //    {
    //        CurgameList.Add(AllGames[i]);
    //        AllGames[i].SetActive(false);
    //    }
    //}

    //게임 클리어시에 성공씬 보여주기 -> 게임 셋
    public IEnumerator ShowClearScene()
    {
        ClearScene.SetActive(true);
        PointUI.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        PointUI.SetActive(false);
        ClearScene.SetActive(false);
        SetNextGame();
    }
    
    //미니게임결정, 다음 게임 실행
    public void SetNextGame()
    {
        int num;
        num = Random.Range(0, gameList.Count);
        CurrentGame = gameList[num];
        gameList.Remove(CurrentGame);
        GameScene = CurrentGame.Game;
        ClearScene = CurrentGame.ClearScene;
        explainScene = CurrentGame.Explain;
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
        //Timer = 0f;
        GameScene.SetActive(true);
        isGame = true;
    }

    //public void CheckTimer()
    //{
    //    if (!isGame) return;
    //    Timer += Time.deltaTime;
    //    if(Timer >= OverTime)
    //    {
    //        GameOver();
    //    }
    //}

    //public void GameOver()
    //{
    //    CurrentGame.SetActive(false);
    //    isGame = false;
    //    GameOverScene.SetActive(true);
    //    Debug.Log("Fail");
    //}

    //게임 클리어 조건을 달성할시 클리어 화면을 보여주고 다음 게임으로 진행
    public void ClearGame()
    {
        Debug.Log("Clear!");
        GameScene.SetActive(false);
        isGame = false;
        point += 10;
        pointtext.text = "x" + point.ToString();
        if (point >= 80)
        {
            //클리어엔딩
        }
        else
        {
            StartCoroutine(ShowClearScene());//계속 진행
        }
    }

}

//미니 게임별로 설명 갱신해주어야함 -> UI 매니저

//게임별