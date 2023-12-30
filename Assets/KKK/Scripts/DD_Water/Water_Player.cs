using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Water_Player : MonoBehaviour
{
    public static Water_Player Instance;
    public Rigidbody2D rigid;
    public bool ispeal = false;
    public bool gameend = false;

    public Animator anim;
    private Vector2 SpawnPos;

    public void Awake()
    {
        Instance = this;
        SpawnPos = transform.position;
        anim = GetComponent<Animator>();
    }

    public void OnEnable()
    {
        transform.position = new Vector2(0, 4);
        gameend = false;
        ispeal = false;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
            rigid.AddForce(Vector2.up * 7f, ForceMode2D.Impulse);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            ispeal = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            transform.position = SpawnPos;
        }
        if (collision.gameObject.CompareTag("Target"))
        {
            if (ispeal == false)  
                return;
            gameend = true;
            GameManager.instance.ClearGame();
        }
    }
}
