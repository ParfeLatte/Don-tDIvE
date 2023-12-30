using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_BackGround : MonoBehaviour
{
    private Vector2 SpawnPos;

    void Start()
    {
        SpawnPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * 3f *Time.deltaTime);
        if (transform.position.x < -26f)
        {
            transform.position = new Vector2(40f, 0.73f);
        }
    }

    public void restart()
    {
        transform.position = SpawnPos;
    }
}
