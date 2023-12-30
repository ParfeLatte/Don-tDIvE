using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Water_Enemy : MonoBehaviour
{
     public bool isleft;
     public float speed;

    public SpriteRenderer sprite;

    public void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void FixedUpdate()
    {
        if (isleft == true)
        {
            sprite.flipX = true;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            sprite.flipX = false;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if (Water_Player.Instance.gameend == true)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Border_left":
                    isleft = true;
                    break;
                case "Border_right":
                    isleft = false;
                    break;
            }
        }
    }
}
