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
    public List<GameObject> CurgameList = new List<GameObject>();//���ӿ��� ����� �̴ϰ��� ����Ʈ
    public List<GameObject> AllGames = new List<GameObject>();//��ü ���� ����Ʈ
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

    //����Ʈ�� ��� �̴ϰ��� ���� �� ���� ����
    public void SetGame()
    {
        CurgameList.Clear();
        for (int i = 0; i < AllGames.Count; i++)
        {
            CurgameList.Add(AllGames[i]);
            AllGames[i].SetActive(false);
        }
    }

    //���� Ŭ����ÿ� ������ �����ֱ� -> ���� ��
    public IEnumerator ShowClearScene()
    {
        ClearScene.SetActive(true);
        yield return new WaitForSeconds(1f);
        ClearScene.SetActive(false);
        SetNextGame();
    }
    
    //�̴ϰ��Ӱ���, ���� ���� ����
    public void SetNextGame()
    {
        int num;
        num = Random.Range(0, CurgameList.Count);
        CurrentGame = CurgameList[num];
        CurgameList.Remove(CurrentGame);
        //CurrentGame.SetActive(true);
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

    //���� Ŭ���� ������ �޼��ҽ� Ŭ���� ȭ���� �����ְ� ���� �������� ����
    public void ClearGame()
    {
        Debug.Log("Clear!");
        CurrentGame.SetActive(false);
        isGame = false;
        point += 5;
        if (point >= 87)
        {
            //Ŭ�����
        }
        else
        {
            if (CurgameList.Count == 0)
            {
                UpdateGameLevel();
            }//���� ����Ʈ�� ��������� ��� �̴ϰ����� Ŭ���� �� �� �̹Ƿ� ����Ʈ �ʱ�ȭ �� ���� ����
            StartCoroutine(ShowClearScene());//��� ����
        }
    }

    public void UpdateGameLevel()
    {
        GameLevel++; 
        GameSpeed += 0.05f * GameLevel;
        SetGame();
    }
}

//�̴� ���Ӻ��� ���� �������־���� -> UI �Ŵ���