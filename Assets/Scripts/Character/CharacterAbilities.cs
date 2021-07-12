using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAbilities : MonoBehaviour
{
    public static CharacterAbilities instance = null;

    public KeyCode _KeyTimeWalkBack = KeyCode.Q;
    public KeyCode _KeyFetchGameObject = KeyCode.W;
    public bool _HoldInHand = false;

    private Vector3 m_CurRebitrhPoint;
    private List<GameObject> m_TempStayDestroys = new List<GameObject>();
    private bool m_HoldTimeFlag = false;
    private bool m_TimeWalkBackLock = false;
    private bool m_FetchGameObjectFlag = false;
    private float m_HoldTime = 0f;

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
        #endregion

        #region FetchGameObject
        if (Input.GetKey(_KeyFetchGameObject))
        {
            m_FetchGameObjectFlag = true;
        }
        if (Input.GetKeyUp(_KeyFetchGameObject))
        {
            m_FetchGameObjectFlag = false;
        }
        #endregion
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
                    if (itemStatus.ItemState < GameItemState.StateFour)
                    {
                        item.SetItemState(itemStatus.ItemState + 1);
                        item.ChangeSprite(1f);
                    }
                    else
                    {
                        m_TimeWalkBackLock = true;
                        DOTween.Sequence().AppendInterval(3).AppendCallback(() => m_TimeWalkBackLock = false);
                        item.SetItemState(GameItemState.StateOne);
                        item.ChangeSprite(1f);
                    }
                    break;
                    //AncientDetector ancientDetector = m_TempStayDestroys[i].GetComponent<AncientDetector>();
                    //if (ancientDetector._CurAncientState < GameAncientState.StateFour)
                    //{
                    //    ancientDetector._CurAncientState++;
                    //    item.ChangeSprite(1f);
                    //}
                    //else if (ancientDetector._CurAncientState == GameAncientState.StateFour)
                    //{
                    //    m_TimeWalkBackLock = true;
                    //    DOTween.Sequence().AppendInterval(3).AppendCallback(() => m_TimeWalkBackLock = false);
                    //    ancientDetector._CurAncientState = GameAncientState.StateOne;
                    //    item.ChangeSprite(1f);
                    //}
                    //break;
                default:
                    break;
            }

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
        }
    }

    void TimeLock()
    {
        m_TimeWalkBackLock = true;
        if (m_TempStayDestroys.Count <= 0) return;

        SoundMgr.instance.StopEff(2);
        TryUnlockingAncient();

        DOTween.Sequence().AppendInterval(3).AppendCallback(() =>
        {
            _OnTimeLock.Invoke();
        });
    }

    public void FetchItemObject(IFetched fetched)
    {
        if (!m_FetchGameObjectFlag) return;

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
