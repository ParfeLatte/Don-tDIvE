using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Player : MonoBehaviour
{
    public bool isJump;

    public Animator anim;
    public Rigidbody2D rigid;

    private Vector2 SpawnPos;

    public Fall_Platform fall_Platform;

    void Awake()
    {
        isJump = false;
        SpawnPos = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("isjumping"))
        {
            rigid.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            anim.SetBool("isjumping", true);
        }
;
    }

    void FixedUpdate()
    {
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1f)
                {
                    anim.SetBool("isjumping", false);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            transform.position = SpawnPos;
            fall_Platform.restart();
        }
        if (collider.gameObject.CompareTag("Goal"))
            GameManager.instance.ClearGame();
    }
}
