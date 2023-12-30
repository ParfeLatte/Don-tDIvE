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
    public Transform Bow;
    public GameObject BowDir;
    public Animator bowAnim;
    [SerializeField] private float Deg;
    [SerializeField] private float Speed;
    private float Dir;
    private Vector2 DirVec;
    [SerializeField] private float ArrowPower;
    
    [SerializeField] private bool isAim;
    [SerializeField] private bool canTry;
    // Start is called before the first frame update
    void Start()
    {
        Deg = 0f;
        Dir = 1f;
        Speed = 40f;
        ArrowPower = 13f;
        isAim = false;
        canTry = true;
        BowDir.SetActive(false); 
        bowAnim.SetBool("isShoot", false);
        Bow.rotation = Quaternion.Euler(0, 0, Deg - 16f);
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
            BowDir.SetActive(true);
        }
    }

    private void ControlDegree()
    {
        if (InputManager.instance.LongSpace)
        {
            DirCheck();
            Deg += Speed * Dir * Time.deltaTime;
            CheckDegree();
        }
        else if (!InputManager.instance.LongSpace && canTry)
        {
            Shoot();
        }
    }

    private void CheckDegree()
    {
        float rad = Deg * Mathf.Deg2Rad;
        float x = Mathf.Cos(rad);
        float y = Mathf.Sin(rad);
        DirVec = new Vector2(x, y).normalized;
        Bow.rotation = Quaternion.Euler(0, 0, Deg-16f);
    }

    private void Shoot()
    {
        GameObject arrow = Instantiate(Arrow, transform.position, Quaternion.Euler(0, 0, Deg));
        arrow.GetComponent<Rigidbody2D>().velocity = DirVec * ArrowPower;
        isAim = false;
        canTry = false;
        BowDir.SetActive(false);
        bowAnim.SetBool("isShoot", true);
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        canTry = true;
        bowAnim.SetBool("isShoot", false);
    }

    private void DirCheck()
    {
        if(Dir == 1f && Deg >= 60)
        {
            Dir = -1f;
        }
        else if(Dir == -1f && Deg <= 0)
        {
            Dir = 1f;
        }
    }
}
