using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour
{
    private Animator anim;
    private int Point;
    public List<GameObject> Heart = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();  
    }

    public void FellinLove()
    {
        if (Heart.Count == Point)
        {
            for (int i = 0; i < Heart.Count; i++) { Heart[i].SetActive(false); }
        }
        else { Heart[Point].SetActive(true); }
        Point++;
        if (Point == 5)
        {
            anim.SetTrigger("FellinLove");
            StartCoroutine(Clear());
        }
    }

    private IEnumerator Clear()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.ClearGame();
    }
}
