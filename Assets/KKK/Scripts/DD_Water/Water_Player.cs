using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Water_Player : MonoBehaviour
{
    public static Water_Player Instance;
    public Rigidbody2D rigid;
    public bool gameend = false;
    bool ispeal;
    public Animator anim;

    private Vector2 SpawnPos;

    public GameObject Goal;

    public void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
        SpawnPos = transform.position;
    }

    public void OnEnable()
    {
        transform.position = new Vector2(0, 4);
        gameend = false;
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            transform.position = SpawnPos;
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            if (!ispeal)
            {
                ispeal = true;
                Goal.transform.position = SpawnPos;
                SpawnPos = new Vector2(transform.position.x, transform.position.y -1f);
            }
            else if (ispeal && !gameend)
            {
                gameend = true;
                GameManager.instance.ClearGame();
            }
        }
    }
}
