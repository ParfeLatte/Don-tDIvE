using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flappy_Enemy : MonoBehaviour
{
    float timer = 10f;

    void Update()
    {
        timer -= Time.deltaTime;
        transform.Translate(Vector2.left * 3f * Time.deltaTime);
        if (timer < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
