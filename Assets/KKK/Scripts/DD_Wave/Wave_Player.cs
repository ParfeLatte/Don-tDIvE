using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Wave_Player : MonoBehaviour
{
    public float speed;
    public float speed2;
    public float time = 5f;
    bool touchborder;

    public Rigidbody2D rigid;

    public void OnEnable()
    {
        transform.position = new Vector2(0, -3);
        time = 5f;
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

        if ((Mathf.Abs(rigid.velocity.x) > 5) && (Mathf.Abs(rigid.velocity.x) < 5))
        {
            rigid.velocity = new Vector2(Mathf.Sign(rigid.velocity.x) * 5, rigid.velocity.y);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            //GameManager.instance.GameOver();
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
