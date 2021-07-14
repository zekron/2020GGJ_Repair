using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class AncientDetector : Detector
{
    public GameObject _ShieldDoor;
    public ParticleSystem[] _CorrectParticles;

    public List<AncientKey> _Keys;
    private Vector3 _RebirthPoint;
    private AncientItem _myAncient;

    private int m_KeyNum;
    private float m_StayTime = 0;

    private void Start()
    {
        m_KeyNum = _Keys.Count;
        _RebirthPoint = new Vector3(transform.position.x, transform.position.y + 10, 1);
        _myAncient = GetComponent<AncientItem>();

        CharacterAbilities.Add_OnTimeLock(ResetAncientState);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DestroyDetector"))
        {
            if (/*EnterDestroy && */!_CanBeDetected)
            {
                _CanBeDetected = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("DestroyDetector"))
        {
            if (!_CanBeDetected || !StayDestroy) return;

            ItemStatus itemStatus = _myAncient.GetDetectedItemStatus();
            if (itemStatus.ItemState == GameItemState.StateFinished) return;

            m_StayTime -= Time.deltaTime;

            if (m_StayTime <= 0f)
            {
                if (itemStatus.ItemState - 1 < GameItemState.StateOne)
                {
                    _CanBeDetected = false;
                    m_StayTime = 0;
                    return;
                }
                _myAncient.SetItemState(itemStatus.ItemState - 1);
                _myAncient.ChangeSprite();
                m_StayTime = StaticData.DestroyDuration;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DestroyDetector"))
        {
            if (ExitDestroy)
            {
                m_StayTime = 0;
                if (_myAncient.GetDetectedItemStatus().ItemState != GameItemState.StateFour)
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
            if (_Keys[i]._AncientKeyState == _myAncient.GetDetectedItemStatus().ItemState
                && _Keys[i]._ItemKeyState == item._PackageItemState
                && _Keys[i]._ItemKeyType == item._PackageItemType)
            {
                //_AncientKeyState.Remove(_CurAncientState);
                //_ItemKeyState.Remove(item._PackageItemState);
                //_ItemKeyType.Remove(item._PackageItemType);
                //_Keys.RemoveAt(i);
                _myAncient.ChangeSprite(i, false, 1);
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

        _myAncient.ChangeSprite(_Keys.Count, true, 2);
        _myAncient.SetItemState(GameItemState.StateFinished);
        _ShieldDoor.SetActive(false);
        CharacterAbilities.instance.SetRebirthPoint(_RebirthPoint);
    }

    public void ResetAncientState()
    {
        if (_myAncient.GetDetectedItemStatus().ItemState == GameItemState.StateFinished) return;
        if (!_IsInTimeWalkBack) return;

        _myAncient.SetItemState(GameItemState.StateOne);
        _myAncient.ChangeSprite();
        _IsInTimeWalkBack = false;
    }
}
