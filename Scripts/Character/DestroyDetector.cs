﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDetector : MonoBehaviour
{
    public Transform _MainChatacter;
    public int _MoveSpeed = 10;

    private bool m_IsMoving = false;
    private float m_StayTime;
    private const float HOLD_THRESHOLD = 1f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    m_StayTime += Time.deltaTime;
    //    Debug.LogFormat("{0} Stay here {1}.", other.tag, m_StayTime);

    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    m_StayTime = 0;
    //    Debug.LogFormat("{0} Exit here {1}.", other.tag, m_StayTime);
    //}
}
