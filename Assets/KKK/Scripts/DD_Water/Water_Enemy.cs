using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Enemy : MonoBehaviour
{
     public bool isleft;
     public int speed;

    public void FixedUpdate()
    {
        if (isleft == true)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.activeSelf) { }
    }
}
