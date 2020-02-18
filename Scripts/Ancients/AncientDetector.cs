using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientDetector : Detector
{
    public eAncientState _CurAncientState;
    public GameObject _ShieldDoor;
    public ParticleSystem[] _CorrectParticles;

    public List<AncientKey> _Keys;
    private Vector3 _RebirthPoint;
    private Ancient m_MyAncient;

    private int m_KeyNum;
    private float m_StayTime = 0;

    private void Start()
    {
        m_KeyNum = _Keys.Count;
        _RebirthPoint = new Vector3(transform.position.x, transform.position.y + 10, 1);
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
            if (_CurAncientState == eAncientState.eStateFive) return;

            m_StayTime -= Time.deltaTime;

            if (m_StayTime <= 0f)
            {
                if (--_CurAncientState < eAncientState.eStateOne)
                {
                    _CanBeDetected = false;
                    _CurAncientState = eAncientState.eStateOne;
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
                if (_CurAncientState != eAncientState.eStateFour)
                {
                    //GetComponent<Item>().ChangeSprite(_CurItemState = ItemState.eStateFour);
                    //_OnDestroyDetectorTriggered.Invoke(_CurItemState = ItemState.eStateFour);
                }
            }
        }
    }

    public void UnLockDoor(PackageItem item)
    {
        for (int i = 0; i < _Keys.Count; i++)
        {
            if (_Keys[i]._AncientKeyState == _CurAncientState
                && _Keys[i]._ItemKeyState == item._PackageItemState
                && _Keys[i]._ItemKeyType == item._PackageItemType)
            {
                //_AncientKeyState.Remove(_CurAncientState);
                //_ItemKeyState.Remove(item._PackageItemState);
                //_ItemKeyType.Remove(item._PackageItemType);
                //_Keys.RemoveAt(i);
                m_MyAncient.ChangeAncientSprite(i, false, 1);
                m_KeyNum--;
                _CorrectParticles[i].Play();
                SoundMgr.instance.PlayEff(SoundMgr.instance._Effect._Correct, 4);
                break;
            }
            else if (i == _Keys.Count - 1)
            {
                SoundMgr.instance.PlayEff(SoundMgr.instance._Effect._Incorrect, 4);
            }
        }

        if (m_KeyNum > 0) return;

        m_MyAncient.ChangeAncientSprite(_Keys.Count, true, 2);
        _CurAncientState = eAncientState.eStateFive;
        _ShieldDoor.SetActive(false);
        CharacterAbilities.instance.RefreshRebirthPoint(_RebirthPoint);
    }

    public void ResetAncientState()
    {
        if (_CurAncientState == eAncientState.eStateFive) return;
        if (!_InTimeWalkBack) return;

        _CurAncientState = eAncientState.eStateOne;
        m_MyAncient.ChangeSprite(_CurAncientState);
        _InTimeWalkBack = false;
    }
}
