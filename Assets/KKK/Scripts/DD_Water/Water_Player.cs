using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Water_Player : MonoBehaviour
{
    public Rigidbody2D rigid;



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector2.up * 6f, ForceMode2D.Impulse);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
            return;
        //게임 실패
    }
}
