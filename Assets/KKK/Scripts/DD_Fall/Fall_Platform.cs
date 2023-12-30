using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Platform : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector2.left * 3f * Time.deltaTime);
    }
}
