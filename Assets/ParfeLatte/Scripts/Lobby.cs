using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public GameObject Intro;
    public GameObject Canvas;
    public bool isLobby;
    // Start is called before the first frame update
    void Awake()
    {
        isLobby = false;
        StartCoroutine(TurnOffIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if(isLobby && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadSceneAsync("MainGame");
        }
        else if(isLobby && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
     
    private IEnumerator TurnOffIntro()
    {
        yield return new WaitForSeconds(3f);
        isLobby = true;
        Intro.SetActive(false);
        Canvas.SetActive(true);
    }
}
