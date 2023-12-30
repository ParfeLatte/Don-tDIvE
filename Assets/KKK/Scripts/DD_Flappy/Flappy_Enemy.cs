using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Flappy_Enemy : MonoBehaviour
{
    float timer = 10f;

    private void Start()
    {
        StartCoroutine(DestroySelf());
    }

    void Update()
    {
        timer -= Time.deltaTime;
        transform.Translate(Vector2.left * 3f * Time.deltaTime);
        if (timer < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }
}
