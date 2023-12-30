using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class thumbsup : MonoBehaviour
{
    private float dir;
    private float Speed;
    private float timer;

    private void Start()
    {
        dir = 1f;
        Speed = 5f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            dir *= -1;
            timer = 0f;
        }
        transform.position += new Vector3(0, dir) * Speed * Time.deltaTime;
    }
}
