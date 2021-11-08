using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAbilities : MonoBehaviour
{
    public static CharacterAbilities instance = null;

    public bool _HoldInHand = false;

    [SerializeField] private InGameInputEventSO inputEvents;

    private Vector3 m_CurRebitrhPoint;
    private List<GameObject> m_TempStayDestroys = new List<GameObject>();
    /// <summary>
    /// 键是否在被长按
    /// </summary>
    private bool m_KeyHoldingFlag = false;
    /// <summary>
    /// 技能是否在冷却
    /// </summary>
    private bool m_CanTimeWalkBack = true;
    private bool m_FetchItemFlag = false;
    /// <summary>
    /// 长按计时器
    /// </summary>
    private float m_HoldingTimer = 0f;
    private float m_HoldingInterval = 1f;

    private void Awake()
    {
        inputEvents.OnFetchEvent += SetFetchItemFlag;
        inputEvents.OnTimeWalkbackEvent += CheckTimeWalkback;
        inputEvents.OnStopTimeWalkbackEvent += StopTimeWalkback;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        m_CurRebitrhPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        #region TimeWalkBack
        if (m_KeyHoldingFlag && m_CanTimeWalkBack)
        {
            m_HoldingTimer += Time.deltaTime;
        }
        if (m_HoldingTimer > m_HoldingInterval)
        {
            TimeWalkBack();
            m_HoldingTimer -= m_HoldingInterval;
        }
        /*
         if (m_HoldTimeFlag && !m_TimeWalkBackLock)
        {
            m_HoldTime += Time.deltaTime;
        }
        if (Input.GetKey(_KeyTimeWalkBack))
        {
            if (m_TimeWalkBackLock) return;

            m_HoldTimeFlag = true;
            if (!SoundMgr.instance.IsPlaying(1, 2))
                SoundMgr.instance.PlayEff(SoundMgr.instance._Effect._SkillTimeWalkBack, 2);

            if (m_HoldTime > 1f)
            {
                TimeWalkBack();
                m_HoldTimeFlag = false;
                m_HoldTime = 0;
            }
        }
        if (Input.GetKeyUp(_KeyTimeWalkBack))
        {
            if (!m_HoldTimeFlag) return;

            m_HoldTimeFlag = false;
            TimeLock();

            DOTween.Sequence().AppendInterval(3).AppendCallback(() => m_TimeWalkBackLock = false);
        }
        */
        #endregion
    }

    private void CheckTimeWalkback()
    {
        if (!m_CanTimeWalkBack) return;

        m_KeyHoldingFlag = true;
        if (!SoundMgr.instance.IsPlaying(1, 2))
            SoundMgr.instance.PlayEff(SoundMgr.instance._Effect._SkillTimeWalkBack, 2);

        TimeWalkBack();
        m_HoldingTimer = 0;
    }

    private void StopTimeWalkback()
    {
        if (!m_KeyHoldingFlag) return;

        m_KeyHoldingFlag = false;
        m_HoldingTimer = 0;
        TimeLock();

        DOTween.Sequence().AppendInterval(3).AppendCallback(() => m_CanTimeWalkBack = true);
    }

    void TimeWalkBack()
    {
        m_TempStayDestroys = DestroyDetector.instance._StayDestroys;
        if (m_TempStayDestroys.Count <= 0) return;

        for (int i = 0; i < m_TempStayDestroys.Count; i++)
        {
            Item item = m_TempStayDestroys[i].GetComponent<Item>();
            ItemStatus itemStatus = item.GetDetectedItemStatus();

            item.MyDetector._IsInTimeWalkBack = true;
            switch (itemStatus.ItemType)
            {
                case GameItemType.Tulip:
                case GameItemType.Rose:
                case GameItemType.Tree:
                case GameItemType.Tomb:
                case GameItemType.Candle:
                case GameItemType.PoisonousPool:
                case GameItemType.Tile:
                case GameItemType.Ancient:
                    if (itemStatus.ItemState == GameItemState.StateFinished) continue;
                    else if (itemStatus.ItemState < GameItemState.StateFour)
                    {
                        item.SetItemState(itemStatus.ItemState + 1);
                        item.ChangeSprite(1f);
                    }
                    else
                    {
                        m_CanTimeWalkBack = false;
                        DOTween.Sequence().AppendInterval(3).AppendCallback(() => m_CanTimeWalkBack = true);
                        item.SetItemState(GameItemState.StateOne);
                        item.ChangeSprite(1f);
                    }
                    break;
                default:
                    break;
            }

            #region Obsolete
            //ItemDetector itemDetector = m_TempStayDestroys[i].GetComponent<ItemDetector>();
            //AncientDetector ancientDetector = m_TempStayDestroys[i].GetComponent<AncientDetector>();
            //if (itemDetector)
            //{
            //    itemDetector._IsInTimeWalkBack = true;
            //    if (itemDetector.CurItemState < GameItemState.StateFour)
            //    {
            //        itemDetector.CurItemState++;
            //        item.ChangeSprite(itemDetector.CurItemState, 1f);
            //    }
            //    else
            //    {
            //        m_TimeWalkBackLock = true;
            //        DOTween.Sequence().AppendInterval(3).AppendCallback(() => m_TimeWalkBackLock = false);
            //        itemDetector.CurItemState = GameItemState.StateOne;
            //        item.ChangeSprite(itemDetector.CurItemState, 1f);
            //    }
            //}
            //else if (ancientDetector)
            //{
            //    ancientDetector._IsInTimeWalkBack = true;
            //    if (ancientDetector._CurAncientState < GameAncientState.StateFour)
            //    {
            //        ancientDetector._CurAncientState++;
            //        item.ChangeSprite(ancientDetector._CurAncientState, 1f);
            //    }
            //    else if (ancientDetector._CurAncientState == GameAncientState.StateFour)
            //    {
            //        m_TimeWalkBackLock = true;
            //        DOTween.Sequence().AppendInterval(3).AppendCallback(() => m_TimeWalkBackLock = false);
            //        ancientDetector._CurAncientState = GameAncientState.StateOne;
            //        item.ChangeSprite(ancientDetector._CurAncientState, 1f);
            //    }
            //}
            #endregion
        }
    }

    void TimeLock()
    {
        m_CanTimeWalkBack = false;
        if (m_TempStayDestroys.Count <= 0) return;

        SoundMgr.instance.StopEff(2);
        TryUnlockingAncient();

        DOTween.Sequence().AppendInterval(3).AppendCallback(() =>
        {
            _OnTimeLock.Invoke();
        });
    }

    private void SetFetchItemFlag(bool flag)
    {
        m_FetchItemFlag = flag;
    }

    public void FetchItemObject(IFetched fetched)
    {
        if (!m_FetchItemFlag) return;

        CharacterPackage.instance.SaveItem(fetched);
    }

    public void TryUnlockingAncient()
    {
        if (!CharacterPackage.instance._HoldingItem) return;

        PackageItem holdInHand = CharacterPackage.instance._HoldingItem;
        CharacterPackage.instance.UseItem();
        for (int i = 0; i < m_TempStayDestroys.Count; i++)
        {
            AncientDetector ancientDetector = m_TempStayDestroys[i].GetComponent<AncientDetector>();
            if (ancientDetector != null)
            {
                ancientDetector.UnLockDoor(holdInHand);
                break;
            }
        }

    }

    public void RebirthCharacter()
    {
        transform.parent.position = m_CurRebitrhPoint;
    }

    public void SetRebirthPoint(Vector3 newPoint)
    {
        m_CurRebitrhPoint = newPoint;
    }

    #region UnityEvent
    public static UnityEvent _OnTimeLock = new UnityEvent();
    public static void Remove_OnTimeLock(UnityAction action)
    {
        _OnTimeLock.RemoveListener(action);
    }
    public static void Add_OnTimeLock(UnityAction action)
    {
        Remove_OnTimeLock(action);
        _OnTimeLock.AddListener(action);
    }
    #endregion
}
