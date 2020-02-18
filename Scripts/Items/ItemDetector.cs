using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class ItemDetector : Detector
{
    public eItemState _CurItemState = eItemState.eStateFour;
    public eItemType _ItemType;

    private float m_StayTime = 0;
    private Item m_MyItem;

    private void Start()
    {
        m_MyItem = GetComponent<Item>();
        CharacterAbilities.Add_OnTimeLock(ResetItemState);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogFormat("{0} enter here.", other.name);
        if (other.tag == "Player")
        {
            if (EnterDestroy && !_CanBeDetected)
            {
                _CanBeDetected = true;
            }
            //Debug.LogFormat("{0} enter here.", other.name);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.LogFormat("{0} stay here.", other.name);
        if (other.tag == "Player")
        {
            if (!_CanBeDetected || !StayDestroy) return;

            m_StayTime -= Time.deltaTime;

            if (m_StayTime <= 0f)
            {
                if (--_CurItemState < eItemState.eStateOne)
                {
                    _CanBeDetected = false;
                    _CurItemState = eItemState.eStateOne;
                    m_StayTime = 0;
                    return;
                }
                m_MyItem.ChangeSprite(_CurItemState);
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
                if (_CurItemState != eItemState.eStateFour)
                {
                    //GetComponent<Item>().ChangeSprite(_CurItemState = ItemState.eStateFour);
                    //_OnDestroyDetectorTriggered.Invoke(_CurItemState = ItemState.eStateFour);
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (StayDestroy && tag == "Item")
        {
            CharacterAbilities.instance.FetchItemObject(this.gameObject);
        }
    }

    public void ResetItemState()
    {
        if (!_InTimeWalkBack) return;

        _CurItemState = eItemState.eStateOne;
        m_MyItem.ChangeSprite(_CurItemState);
        _InTimeWalkBack = false;
    }

    public static MyItemStateEvent _OnItemStateChanged = new MyItemStateEvent();
    public static void Remove_OnItemStateChanged(UnityAction<eItemState> action)
    {
        _OnItemStateChanged.RemoveListener(action);
    }
    public static void Add_OnItemStateChanged(UnityAction<eItemState> action)
    {
        Remove_OnItemStateChanged(action);
        _OnItemStateChanged.AddListener(action);
    }
}
