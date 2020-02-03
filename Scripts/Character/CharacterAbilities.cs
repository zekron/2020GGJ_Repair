using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterAbilities : MonoBehaviour
{
    public static CharacterAbilities instance = null;

    public KeyCode _KeyTimeWalkBack = KeyCode.Q;
    public KeyCode _KeyFetchGameObject = KeyCode.W;
    public bool _HoldInHand = false;

    private Vector3 _CurRebitrhPoint;
    private List<GameObject> m_TempStayDestroys = new List<GameObject>();
    private bool m_HoldTimeFlag = false;
    private bool m_TimeWalkBackLock = false;
    private bool m_FetchGameObjectFlag = false;
    private float m_HoldTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _CurRebitrhPoint = transform.position;
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

            if (m_HoldTime > 1f)
            {
                TimeWalkBack();
                m_HoldTimeFlag = false;
                m_HoldTime = 0;
            }
        }
        if (Input.GetKeyUp(_KeyTimeWalkBack))
        {
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
            InteractiveObject tempObject = m_TempStayDestroys[i].GetComponent<InteractiveObject>();
            ItemDetector itemDetector = m_TempStayDestroys[i].GetComponent<ItemDetector>();
            AncientDetector ancientDetector = m_TempStayDestroys[i].GetComponent<AncientDetector>();
            if (itemDetector)
            {
                if (itemDetector._CurItemState < ItemState.eStateFour)
                {
                    itemDetector._CurItemState++;
                    tempObject.ChangeSprite(itemDetector._CurItemState, 1f);
                }
                else
                {
                    m_TimeWalkBackLock = true;
                    itemDetector._CurItemState = ItemState.eStateOne;
                    tempObject.ChangeSprite(itemDetector._CurItemState, 1f);
                }
            }
            else if (ancientDetector)
            {
                if (ancientDetector._CurAncientState < AncientState.eStateFour)
                {
                    ancientDetector._CurAncientState++;
                    tempObject.ChangeSprite(ancientDetector._CurAncientState, 1f);
                }
                else if (ancientDetector._CurAncientState == AncientState.eStateFour)
                {
                    m_TimeWalkBackLock = true;
                    ancientDetector._CurAncientState = AncientState.eStateOne;
                    tempObject.ChangeSprite(ancientDetector._CurAncientState, 1f);
                }
            }
        }
    }

    void TimeLock()
    {
        m_TimeWalkBackLock = true;
        if (m_TempStayDestroys.Count <= 0) return;

        TryUnlockingAncient();

        DOTween.Sequence().AppendInterval(3).AppendCallback(() =>
        {
            for (int i = 0; i < m_TempStayDestroys.Count; i++)
            {
                InteractiveObject tempObject = m_TempStayDestroys[i].GetComponent<InteractiveObject>();
                ItemDetector itemDetector = m_TempStayDestroys[i].GetComponent<ItemDetector>();
                AncientDetector ancientDetector = m_TempStayDestroys[i].GetComponent<AncientDetector>();
                if (itemDetector)
                {
                    if (itemDetector._CurItemState > ItemState.eStateOne)
                    {
                        itemDetector._CurItemState = ItemState.eStateOne;
                        tempObject.ChangeSprite(itemDetector._CurItemState, 1f);
                    }
                }
                else if (ancientDetector)
                {
                    if (ancientDetector._CurAncientState > AncientState.eStateOne
                    && ancientDetector._CurAncientState != AncientState.eStateFive)
                    {
                        ancientDetector._CurAncientState = AncientState.eStateOne;
                        tempObject.ChangeSprite(ancientDetector._CurAncientState, 1f);
                    }
                }
            }

        });
    }

    public void FetchItemObject(GameObject obj)
    {
        if (!m_FetchGameObjectFlag) return;

        ItemDetector holdInHand = obj.GetComponent<ItemDetector>();
        CharacterPackage.instance.SaveItem(holdInHand, obj);
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
                Debug.LogError(ancientDetector._CurAncientState);
                if (ancientDetector._CurAncientState
                    == ancientDetector._AncientKeyState
                    && ancientDetector._ItemKeyState == holdInHand._PackageItemState
                    && ancientDetector._ItemKeyType == holdInHand._PackageItemType)
                {
                    ancientDetector.UnLockDoor();
                }
                break;
            }
        }

    }

    public void RebirthCharacter()
    {
        transform.parent.position = _CurRebitrhPoint;
    }

    public void RefreshRebirthPoint(Vector3 newPoint)
    {
        _CurRebitrhPoint = newPoint;
    }
}
