using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flappy_Spawner : MonoBehaviour
{
    public GameObject[] Enemys;
    public Transform[] Spawnpoints;

    float timer = 3.5f;
    float retimer = 3.5f;

    int count;
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 0;
        }
        if ((count == 7) && (timer == 0f))
        {
            Instantiate(Enemys[1], Spawnpoints[7].position, Spawnpoints[7].rotation);
            count++;
        }

        if ((count <= 6) && (timer == 0f))
        {
            int RanPoint = Random.Range(0, Spawnpoints.Length);
            Instantiate(Enemys[0], Spawnpoints[RanPoint].position, Spawnpoints[RanPoint].rotation);
            timer = retimer;
            count++;
        }
    }

    public void restart()
    {
        timer = retimer;
        count = 0;
    }
}
