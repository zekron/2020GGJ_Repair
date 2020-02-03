using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientDetector : Detector
{
    public AncientState _CurAncientState;

    [Header("Key")]
    public AncientState _AncientKeyState;

    public ItemState _ItemKeyState;
    public ItemType _ItemKeyType;

    public GameObject _ShieldDoor;
    private Vector3 _RebirthPoint;
    private Ancient m_MyAncient;

    private int m_LockNum = 1;
    private float m_StayTime = 0;

    private void Start()
    {
        _RebirthPoint = transform.position;
        m_MyAncient = GetComponent<Ancient>();

        CharacterAbilities.Add_OnTimeLock(ResetAncientState);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (EnterDestroy && !_CanBeDetected)
            {
                _CanBeDetected = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!_CanBeDetected || !StayDestroy) return;
            if (_CurAncientState == AncientState.eStateFive) return;

            m_StayTime -= Time.deltaTime;

            if (m_StayTime <= 0f)
            {
                if (--_CurAncientState < AncientState.eStateOne)
                {
                    _CanBeDetected = false;
                    _CurAncientState = AncientState.eStateOne;
                    m_StayTime = 0;
                    return;
                }
                m_MyAncient.ChangeSprite(_CurAncientState);
                m_StayTime = StaticData.DestroyDuration;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (ExitDestroy)
            {
                m_StayTime = 0;
                if (_CurAncientState != AncientState.eStateFour)
                {
                    //GetComponent<Item>().ChangeSprite(_CurItemState = ItemState.eStateFour);
                    //_OnDestroyDetectorTriggered.Invoke(_CurItemState = ItemState.eStateFour);
                }
            }
        }
    }

    public void UnLockDoor()
    {
        m_LockNum--;
        m_MyAncient.ChangeAncientSprite(m_LockNum, 1);
        if (m_LockNum > 0) return;

        _CurAncientState = AncientState.eStateFive;
        _ShieldDoor.SetActive(false);
        CharacterAbilities.instance.RefreshRebirthPoint(_RebirthPoint);
    }

    public void ResetAncientState()
    {
        if (_CurAncientState == AncientState.eStateFive) return;
        if (!_InTimeWalkBack) return;

        _CurAncientState = AncientState.eStateOne;
        m_MyAncient.ChangeSprite(_CurAncientState);
        _InTimeWalkBack = false;
    }
}
