using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class ItemDetector : MonoBehaviour
{
    public static ItemDetector instance = null;
    public ItemState _CurItemState = ItemState.eStateFour;
    private float m_StayTime = 0;
    private bool m_CanBeDetected = false;

    private void Awake()
    {
        instance = this;
        //    if (!GetComponent<BoxCollider2D>())
        //    {
        //        gameObject.AddComponent<BoxCollider2D>();

        //        BoxCollider2D collider2D = GetComponent<BoxCollider2D>();
        //        collider2D.size = Vector3.one;
        //        collider2D.isTrigger = true;
        //    }
        //    if (!GetComponent<Rigidbody2D>())
        //    {
        //        gameObject.AddComponent<Rigidbody2D>();
        //        GetComponent<Rigidbody2D>().isKinematic = true;
        //    }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //Debug.LogFormat("{0} enter here.", other.name);
    //    if (other.tag == "Player")
    //    {
    //        m_CanBeDetected = true;
    //        //Debug.LogFormat("{0} enter here.", other.name);
    //    }
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    //Debug.LogFormat("{0} stay here.", other.name);
    //    if (other.tag == "Player")
    //    {
    //        if (!m_CanBeDetected) return;

    //        m_StayTime -= Time.deltaTime;

    //        if (m_StayTime <= 0f)
    //        {
    //            if (--_CurItemState < ItemState.eStateOne)
    //            {
    //                _CurItemState = ItemState.eStateFour;
    //                m_CanBeDetected = false;
    //                //_OnDestroyDetectorTriggered.Invoke(_CurItemState);
    //                m_StayTime = 0;
    //                return;
    //            }
    //            //_OnDestroyDetectorTriggered.Invoke(_CurItemState);
    //            m_StayTime = 1;
    //        }
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    //Debug.LogFormat("{0} exit here.", other.name);
    //    if (other.tag == "Player")
    //    {
    //        m_StayTime = 0;
    //        if (_CurItemState != ItemState.eStateFour)
    //        { 
    //            //_OnDestroyDetectorTriggered.Invoke(_CurItemState = ItemState.eStateFour);
    //        }
    //    }
    //}

    public static MyItemStateEvent _OnDestroyDetectorTriggered = new MyItemStateEvent();
    public void Add_OnDestroyDetectorTriggered(UnityAction<ItemState> action)
    {
        Remove_OnDestroyDetectorTriggered(action);
        _OnDestroyDetectorTriggered.AddListener(action);
    }
    public void Remove_OnDestroyDetectorTriggered(UnityAction<ItemState> action)
    {
        _OnDestroyDetectorTriggered.RemoveListener(action);
    }
}
