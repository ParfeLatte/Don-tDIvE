using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class TweezerMove : MonoBehaviour
{
    [SerializeField] private Transform Left;
    [SerializeField] private Transform Right;
    [SerializeField] private Transform Bottom;

    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;
    [SerializeField] private float curX;
    [SerializeField] private float curY;
    [SerializeField] private float xDir;
    [SerializeField] private float yDir;

    [SerializeField] private bool isCatch;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Start()
    {
        xSpeed = 10f;
        ySpeed = 5f;
        xDir = 1f;
        yDir = -1f;
        isCatch = false;
        transform.position = Left.position;
        curX = transform.position.x;
        curY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCatch)
        {
            MoveTweezer();
            CheckDir();
        }
        else
        {
            CatchMove();
            CheckYDir();
        }
        TryToCatchLastHair();
    }

    private void MoveTweezer()
    {
        curX += xSpeed * xDir * Time.deltaTime;
        transform.position = new Vector2(curX, curY);
    }

    private void CheckDir()
    {
        if (xDir == 1f && curX >= Right.position.x)
        {
            xDir = -1f;
        }
        else if (xDir == -1f && curX <= Left.position.x)
        {
            xDir = 1f;
        }
    }

    private void TryToCatchLastHair()
    {
        if(!isCatch && InputManager.instance.Space)
        {
            isCatch = true;
        }
    }

    private void CatchMove()
    {
        curY += yDir * ySpeed * Time.deltaTime;
        transform.position = new Vector2(curX, curY);
    }
    private void CheckYDir()
    {
        if (yDir == 1f && curY >= Left.position.y)
        {
            isCatch = false;
            yDir = -1f;
        }
        else if (yDir == -1f && curY <= Bottom.position.y)
        {
            yDir = 1f;
        }
    }

    //private IEnumerator SuccessToPullOutLastHair()
    //{
    //    yield return new WaitForSeconds(1f);
    //    GameManager.instance.ClearGame();
    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("LastHair"))
        {
            anim.SetBool("isCatch", true);
            col.GetComponentInParent<hair>().DestroySelf();
            col.transform.SetParent(transform);
            col.transform.localPosition = new Vector2(0.1f, 0);
            yDir = 1f;
            StartCoroutine(ChangeAnimation());
        }
    }

    private IEnumerator ChangeAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isCatch", false);
    }
}
