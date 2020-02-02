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
                if (--_CurItemState < ItemState.eStateOne)
                {
                    _CanBeDetected = false;
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
            CharacterAbilities.instance.FetchItemObject(this.gameObject);
        }
    }
}
