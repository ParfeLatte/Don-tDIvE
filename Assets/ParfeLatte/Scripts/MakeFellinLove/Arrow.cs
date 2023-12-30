using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Vector2 Dir;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroySelf());
    }
    // Update is called once per frame
    void Update()
    {
        transform.right = rigid.velocity; 
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Girl"))
        {
            col.GetComponent<Girl>().FellinLove();
            Destroy(gameObject);
        }
    }
}
