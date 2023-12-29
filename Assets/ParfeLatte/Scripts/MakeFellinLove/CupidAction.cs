    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CupidAction : MonoBehaviour
{
    public GameObject Arrow;
    [SerializeField] private float Deg;
    [SerializeField] private float Speed;
    private float Dir;
    

    [SerializeField] private bool isAim;
    [SerializeField] private bool canTry;
    // Start is called before the first frame update
    void Start()
    {
        Deg = 0f;
        Dir = 1f;
        Speed = 5f;
        isAim = false;
        canTry = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAim)
        {
            CheckSpace();
        }
        if (isAim && canTry)
        {
            ControlDegree();
        }
    }

    private void CheckSpace()
    {
       if (!canTry) return;
       if(!isAim && InputManager.instance.Space)
       {
            isAim = true;
       }
    }

    private void ControlDegree()
    {
        if (InputManager.instance.LongSpace)
        {
            DirCheck();
            Deg += Speed * Dir * Time.deltaTime;
        }
        else if (!InputManager.instance.LongSpace && canTry)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        float rad = Deg * Mathf.Deg2Rad;
        float x = Mathf.Cos(rad);
        float y = Mathf.Sin(rad);
        Vector2 DirVec = new Vector2(x, y).normalized;
        GameObject arrow = Instantiate(Arrow, transform.position, transform.rotation);
        arrow.GetComponent<Rigidbody2D>().velocity = DirVec * 13f;
        isAim = false;
        canTry = false;
    }

    private void DirCheck()
    {
        if(Dir == 1f && Deg >= 50)
        {
            Dir = -1f;
        }
        else if(Dir == -1f && Deg <= 0)
        {
            Dir = 1f;
        }
    }
}
