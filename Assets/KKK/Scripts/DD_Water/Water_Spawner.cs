using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Water_Spawner : MonoBehaviour
{
    public GameObject[] Enemys;
    public Transform[] Spawnpoints;

    public int num;
    List<int> usednum = new List<int>();


    public void OnEnable()
    {
        usednum.Clear();
        for (int i = 0; i < 3; i++)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        int RanEnemy = Random.Range(0, 3);
        
        GetRandomInt();

        Instantiate(Enemys[RanEnemy], Spawnpoints[num].position, Spawnpoints[num].rotation);
    }

    public void GetRandomInt()
    {
        num = Random.Range(0, Spawnpoints.Length);
        if (!usednum.Contains(num))
        {
            usednum.Add(num);
        }
        else
        {
            GetRandomInt();
        }
    }
}
