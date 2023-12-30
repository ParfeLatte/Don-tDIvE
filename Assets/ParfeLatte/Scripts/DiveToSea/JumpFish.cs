using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class JumpFish : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer spr;

    public Sprite Idle;
    public Sprite Charge;
    public Sprite Jump;

    [SerializeField] private float JumpPower;
    [SerializeField] private float MaxPower;
    [SerializeField] private float MinPower;
    [SerializeField] private float Deg;
    [SerializeField] private float Dir;
    private Vector2 DirVec;
    [SerializeField] private float Speed;

    private Vector2 SpawnPos;

    private bool isCharge;
    private bool isJump;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();

        JumpPower = MinPower;
        SpawnPos = transform.position;
        float rad = Deg * Mathf.Deg2Rad;
        float x = Mathf.Cos(rad);
        float y = Mathf.Sin(rad);
        Dir = 1f;
        DirVec = new Vector2(x, y).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCharge) { CheckSpace(); }
        else if (isCharge)
        {
            ControlPower();
        }
        if (isJump)
        {
            CheckPlatform();
        }
    } 
    
    private void CheckPlatform()
    {
        if(rigid.velocity.y == 0f)
        {
            rigid.velocity = Vector2.zero;
            isJump = false;
            spr.sprite = Idle;
        }
    }

    private void CheckSpace()
    {
        if (InputManager.instance.Space && !isJump)
        {
            isCharge = true;
        }
    }

    private void ControlPower()
    {
        if (InputManager.instance.LongSpace)
        {
            JumpPower += Speed* Time.deltaTime;
            spr.sprite = Charge;
            CheckPower();
        }
        else if(!InputManager.instance.LongSpace && isCharge)
        {
            FishJump();
        }
    }

    private void CheckPower()
    {
        if (JumpPower >= MaxPower) { JumpPower = MaxPower; }
    }

    private void FishJump()
    {
        if (isJump) return;
        spr.sprite = Jump;
        isJump = true;
        rigid.velocity = DirVec * JumpPower;
        JumpPower = MinPower;
        isCharge = false;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Fail"))
        {
            transform.position = SpawnPos;
        }
        else if (col.CompareTag("Sea"))
        {
            //局聪皋捞记 贸府
            StartCoroutine(Clear());
        }
    }
    
    private IEnumerator Clear()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.ClearGame();
    }
}
