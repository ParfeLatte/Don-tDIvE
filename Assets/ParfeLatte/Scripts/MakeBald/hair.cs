using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hair : MonoBehaviour
{
    public List<GameObject> hairs = new List<GameObject>();
    GameObject CurHair;
    float Timer;
    bool isUp;

    private void Start()
    {
        SetHair();
    }

    private void Update()
    {
        if (isUp) { UpHair(); }
    }

    private void SetHair()
    {
        if (hairs.Count == 0) { GameManager.instance.ClearGame(); return; }
        int num = Random.Range(0, hairs.Count);
        CurHair = hairs[num];
        isUp = true; 
    } 

    private void UpHair()
    {
        CurHair.SetActive(true);
        CurHair.transform.position += new Vector3(0, 5.65f) * Time.deltaTime;
        Timer += Time.deltaTime;
        if(Timer >= 1f) { isUp = false; Timer = 0f; }
    }

    public void DestroySelf()
    {
        StartCoroutine(WaitForDestroy());
    }
    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        hairs.Remove(CurHair);
        Destroy(CurHair);
        SetHair();

    }
}
