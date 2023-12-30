using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flappy_Player : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator anim;

    public Flappy_Spawner flappy_Spawner;

    private Vector2 SpawnPos;
    GameObject ForDestroy;

    void Start()
    {
        SpawnPos = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Fly");
            rigid.AddForce(Vector2.up * 7f, ForceMode2D.Impulse);
        }

        if (Mathf.Abs(rigid.velocity.y) > 5)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, Mathf.Sign(rigid.velocity.y) * 5);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if ((collider.gameObject.CompareTag("Enemy")) || (collider.gameObject.CompareTag("Border")))
        {
            transform.position = SpawnPos;
            flappy_Spawner.restart();
            ForDestroy = GameObject.Find("Enemy(Clone)");
            Destroy(ForDestroy);
        }
        if (collider.gameObject.CompareTag("Goal"))
        {
            GameManager.instance.ClearGame();
        }
    }
}
