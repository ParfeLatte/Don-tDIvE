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
    public List<GameZip> gameList = new List<GameZip>();//���ӿ��� ����� �̴ϰ��� ����Ʈ
    //public List<GameObject> AllGames = new List<GameObject>();//��ü ���� ����Ʈ
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

    //����Ʈ�� ��� �̴ϰ��� ���� �� ���� ����
    //public void SetGame()
    //{
    //    CurgameList.Clear();
    //    for (int i = 0; i < AllGames.Count; i++)
    //    {
    //        CurgameList.Add(AllGames[i]);
    //        AllGames[i].SetActive(false);
    //    }
    //}

    //���� Ŭ����ÿ� ������ �����ֱ� -> ���� ��
    public IEnumerator ShowClearScene()
    {
        ClearScene.SetActive(true);
        PointUI.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        PointUI.SetActive(false);
        ClearScene.SetActive(false);
        SetNextGame();
    }
    
    //�̴ϰ��Ӱ���, ���� ���� ����
    public void SetNextGame()
    {
        int num;
        num = Random.Range(0, gameList.Count);
        CurrentGame = gameList[num];
        gameList.Remove(CurrentGame);
        GameScene = CurrentGame.Game;
        ClearScene = CurrentGame.ClearScene;
        explainScene = CurrentGame.Explain;
        StartCoroutine(ShowExplainScene()); //���� ���� ������ ����
    }

    //���� ���� �����ְ� �������� �Ѿ
    public IEnumerator ShowExplainScene() 
    {
        explainScene.SetActive(true);//���� 2�ʰ� �����ְ� �Ѿ
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

    //���� Ŭ���� ������ �޼��ҽ� Ŭ���� ȭ���� �����ְ� ���� �������� ����
    public void ClearGame()
    {
        Debug.Log("Clear!");
        GameScene.SetActive(false);
        isGame = false;
        point += 10;
        pointtext.text = "x" + point.ToString();
        if (point >= 80)
        {
            //Ŭ�����
        }
        else
        {
            StartCoroutine(ShowClearScene());//��� ����
        }
    }

}

//�̴� ���Ӻ��� ���� �������־���� -> UI �Ŵ���

//���Ӻ�