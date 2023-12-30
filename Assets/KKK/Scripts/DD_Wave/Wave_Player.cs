using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Wave_Player : MonoBehaviour
{
    public float speed;
    public float speed2;
    public float time = 15f;
    bool touchborder;

    public Rigidbody2D rigid;

    private Vector2 SpawnPos;

    public Wave_BackGround wave_BackGround;
    public Wave_BackGround wave_BackGround2;
    public Wave_Wave wave_wave;

    public void Awake()
    {
        SpawnPos = transform.position;
    }

    void Update()
    {
        time -= Time.deltaTime;
        rigid.AddForce(Vector2.left * speed2);
        if (Input.GetKeyDown(KeyCode.Space) && touchborder == false)
        {
            speed = Random.Range(1.5f, 2);
            rigid.AddForce(Vector2.right *speed , ForceMode2D.Impulse);
        }

        if (time == 0f)
        {
            GameManager.instance.ClearGame();
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            transform.position = SpawnPos;
            rigid.velocity = new Vector2(0f, 0f);
            time = 15f;
            wave_BackGround.restart();
            wave_BackGround2.restart();
            wave_wave.restart();
        }
        if (col.gameObject.CompareTag("Border"))
        {
            touchborder = true;
        }


    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Border"))
        {
            touchborder=false;
        }
    }
}
