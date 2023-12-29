using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputManager : Singleton<InputManager>
{
    //그냥 누르기
    public bool Space {  get; private set; }
    //꾹 누르기
    public bool LongSpace { get; private set; }

    protected void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        InputButton();
    }

    private void InputButton()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Space = true;
            Debug.Log("True");
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            LongSpace = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space)){
            Space = false;
            LongSpace = false;
        }
    }
}
