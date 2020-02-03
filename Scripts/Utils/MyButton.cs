using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton : MonoBehaviour
{
    Action m_ButtonAction;
    public SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    public void AddListener(Action action)
    {
        m_ButtonAction += action;
    }

    public void RemoveListener(Action action)
    {
        m_ButtonAction -= action;
    }

    private void OnMouseDown()
    {
        m_ButtonAction.Invoke();
    }
}
