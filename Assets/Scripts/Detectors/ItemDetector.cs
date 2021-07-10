﻿using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ItemDetector : Detector
{
    [SerializeField] private Item _myItem;

    private float _stayTime = 0;

    private void OnEnable()
    {
        _myItem = GetComponent<Item>();
    }
    private void Start()
    {
        CharacterAbilities.Add_OnTimeLock(ResetItemState);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log($"{name} {EnterDestroy} {_CanBeDetected} {Time.frameCount}");
            if (/*EnterDestroy && */!_CanBeDetected)
            {
                _CanBeDetected = true;
            }
            //Debug.LogFormat("{0} enter here.", other.name);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.LogFormat("{0} stay here.", other.name);
        if (other.tag == "Player")
        {
            if (!_CanBeDetected || !StayDestroy) return;

            _stayTime -= Time.deltaTime;

            if (_stayTime <= 0f)
            {
                ItemStatus itemStatus = _myItem.GetDetectedItemStatus();
                if (itemStatus.ItemState - 1 < GameItemState.StateOne)
                {
                    _CanBeDetected = false;
                    _stayTime = 0;
                    return;
                }
                _myItem.SetItemState(itemStatus.ItemState - 1);
                _myItem.ChangeSprite();
                _stayTime = StaticData.DestroyDuration;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.LogFormat("{0} exit here.", other.name);
        if (other.tag == "Player")
        {
            if (ExitDestroy)
            {
                _stayTime = 0;
                if (_myItem.GetDetectedItemStatus().ItemState != GameItemState.StateFour)
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
            IFetched fetched = _myItem as IFetched;
            fetched.Fetch();
        }
    }

    public void ResetItemState()
    {
        if (!_IsInTimeWalkBack) return;

        _myItem.SetItemState(GameItemState.StateOne);
        _myItem.ChangeSprite();
        _IsInTimeWalkBack = false;
    }
}
