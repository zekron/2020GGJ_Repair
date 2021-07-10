using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class AncientDetector : Detector
{
    public GameAncientState _CurAncientState;
    public GameObject _ShieldDoor;
    public ParticleSystem[] _CorrectParticles;

    public List<AncientKey> _Keys;
    private Vector3 _RebirthPoint;
    private AncientItem m_MyAncient;

    private int m_KeyNum;
    private float m_StayTime = 0;

    private void Start()
    {
        m_KeyNum = _Keys.Count;
        _RebirthPoint = new Vector3(transform.position.x, transform.position.y + 10, 1);
        m_MyAncient = GetComponent<AncientItem>();

        CharacterAbilities.Add_OnTimeLock(ResetAncientState);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (/*EnterDestroy && */!_CanBeDetected)
            {
                _CanBeDetected = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!_CanBeDetected || !StayDestroy) return;
            if (_CurAncientState == GameAncientState.StateFive) return;

            m_StayTime -= Time.deltaTime;

            if (m_StayTime <= 0f)
            {
                if (--_CurAncientState < GameAncientState.StateOne)
                {
                    _CanBeDetected = false;
                    _CurAncientState = GameAncientState.StateOne;
                    m_StayTime = 0;
                    return;
                }
                m_MyAncient.ChangeSprite();
                m_StayTime = StaticData.DestroyDuration;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (ExitDestroy)
            {
                m_StayTime = 0;
                if (_CurAncientState != GameAncientState.StateFour)
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
                m_MyAncient.ChangeSprite(i, false, 1);
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

        m_MyAncient.ChangeSprite(_Keys.Count, true, 2);
        _CurAncientState = GameAncientState.StateFive;
        _ShieldDoor.SetActive(false);
        CharacterAbilities.instance.RefreshRebirthPoint(_RebirthPoint);
    }

    public void ResetAncientState()
    {
        if (_CurAncientState == GameAncientState.StateFive) return;
        if (!_IsInTimeWalkBack) return;

        _CurAncientState = GameAncientState.StateOne;
        m_MyAncient.ChangeSprite();
        _IsInTimeWalkBack = false;
    }
}
