using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(name);
    }
}
