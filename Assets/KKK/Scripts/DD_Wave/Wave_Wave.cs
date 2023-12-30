using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_Wave : MonoBehaviour
{
    public float wavepoint;
    float position;

    public float waveduration = 1f;
    public float nextduration = 1f;

    void OnEnable()
    {
        transform.position = new Vector2(-5, -1);
    }

    void Update()
    {
            waveduration -= Time.deltaTime;
            transform.Translate(Vector2.right* position * Time.deltaTime);
            if (waveduration <= 0)
            {
                position = Random.Range(-wavepoint, wavepoint);
                waveduration = nextduration;
            }
    }
}
