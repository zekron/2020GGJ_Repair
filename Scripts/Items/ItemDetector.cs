using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class ItemDetector : Detector
{
    public ItemState _CurItemState = ItemState.eStateFour;
    public ItemType _ItemType;

    private float m_StayTime = 0;
    /// <summary>
    /// 是否被瘟疫光环影响
    /// </summary>
    private bool m_CanBeDetected = false;
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogFormat("{0} enter here.", other.name);
        if (other.tag == "Player")
        {
            if (EnterDestroy && !m_CanBeDetected)
            {
                m_CanBeDetected = true;
            }
            //Debug.LogFormat("{0} enter here.", other.name);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.LogFormat("{0} stay here.", other.name);
        if (other.tag == "Player")
        {
            if (!m_CanBeDetected || !StayDestroy) return;

            m_StayTime -= Time.deltaTime;

            if (m_StayTime <= 0f)
            {
                if (--_CurItemState < ItemState.eStateOne)
                {
                    m_CanBeDetected = false;
                    _CurItemState = ItemState.eStateOne;
                    //GetComponent<Item>().ChangeSprite(_CurItemState);
                    //_OnDestroyDetectorTriggered.Invoke(_CurItemState);
                    m_StayTime = 0;
                    return;
                }
                GetComponent<Item>().ChangeSprite(_CurItemState);
                //_OnDestroyDetectorTriggered.Invoke(_CurItemState);
                m_StayTime = StaticData.DestroyDuration;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.LogFormat("{0} exit here.", other.name);
        if (other.tag == "Player")
        {
            if (ExitDestroy)
            {
                m_StayTime = 0;
                if (_CurItemState != ItemState.eStateFour)
                {
                    //GetComponent<Item>().ChangeSprite(_CurItemState = ItemState.eStateFour);
                    //_OnDestroyDetectorTriggered.Invoke(_CurItemState = ItemState.eStateFour);
                }
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.LogErrorFormat("{0} state {1}", this.name, StayDestroy);
        if (StayDestroy)
        {
            Debug.LogError("OnMouseDown");
            CharacterAbilities.instance.FetchGameObject(this.gameObject);
        }
    }

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
