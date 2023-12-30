using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Platform : MonoBehaviour
{
    private Vector2 SpawnPos;

    void Start()
    {
        SpawnPos = transform.position;
    }
    void Update()
    {
        transform.Translate(Vector2.left * 3f * Time.deltaTime);
    }

    public void restart()
    {
        transform.position = SpawnPos;
    }
}
