using UnityEngine;
using UnityEngine.Events;

public class ItemDetector : Detector
{
    public eItemType _ItemType;

    private eItemState m_CurItemState = eItemState.eStateFour;
    private float m_StayTime = 0;
    private Item m_MyItem;

    public eItemState CurItemState
    {
        get => m_CurItemState;
        set
        {
            m_CurItemState = value;
            _OnItemStateChanged.Invoke(m_CurItemState);
        }
    }
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
                if (--CurItemState < eItemState.eStateOne)
                {
                    _CanBeDetected = false;
                    CurItemState = eItemState.eStateOne;
                    m_StayTime = 0;
                    return;
                }
                m_MyItem.ChangeSprite(CurItemState);
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
                if (CurItemState != eItemState.eStateFour)
                {
                    //GetComponent<Item>().ChangeSprite(_CurItemState = ItemState.eStateFour);
                    //_OnDestroyDetectorTriggered.Invoke(_CurItemState = ItemState.eStateFour);
                }
            }
        }
    }

    private void OnMouseDown()
    {
        //Debug.LogFormat("{0} OnMouseDown.", name);
        if (StayDestroy && tag == "Item")
        {
            //Debug.LogFormat("{0} StayDestroy.", name);
            CharacterAbilities.instance.FetchItemObject(this.gameObject);
        }
    }

    public void ResetItemState()
    {
        if (!_InTimeWalkBack) return;

        CurItemState = eItemState.eStateOne;
        m_MyItem.ChangeSprite(CurItemState);
        _InTimeWalkBack = false;
    }

    public MyItemStateEvent _OnItemStateChanged = new MyItemStateEvent();

    public void Remove_OnItemStateChanged(UnityAction<eItemState> action)
    {
        _OnItemStateChanged.RemoveListener(action);
    }
    public void Add_OnItemStateChanged(UnityAction<eItemState> action)
    {
        Remove_OnItemStateChanged(action);
        _OnItemStateChanged.AddListener(action);
    }
}
