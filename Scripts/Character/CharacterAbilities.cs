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
    private bool m_HoldTimeFlag = false;
    private bool m_TimeWalkBackFlag = false;
    private bool m_FetchGameObjectFlag = false;
    private float m_HoldTime = 0f;

    private List<GameObject> m_TempStayDestroys;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        #region TimeWalkBack
        if (m_HoldTimeFlag)
        {
            m_HoldTime += Time.deltaTime;
        }
        if (Input.GetKeyDown(_KeyTimeWalkBack))
        {
            DOTween.Sequence().AppendInterval(5).AppendCallback(TimeLock);
        }
        if (Input.GetKey(_KeyTimeWalkBack))
        {
            m_HoldTimeFlag = true;

            if (m_HoldTime > 1f)
            {
                TimeWalkBack((int)m_HoldTime);
                m_HoldTimeFlag = false;
                m_HoldTime = 0;
            }
        }
        if (Input.GetKeyUp(_KeyTimeWalkBack))
        {
            m_HoldTimeFlag = false;
            DOTween.Sequence().AppendInterval(3).AppendCallback(TimeLock);
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

    void TimeWalkBack(int index)
    {
        Debug.Log("TimeWalkBack" + index);

        m_TempStayDestroys = DestroyDetector.instance._StayDestroys;
        if (m_TempStayDestroys.Count <= 0) return;

        for (int i = 0; i < m_TempStayDestroys.Count; i++)
        {
            Item itemHoldInHand = m_TempStayDestroys[i].GetComponent<Item>();
            ItemDetector detector = m_TempStayDestroys[i].GetComponent<ItemDetector>();
            ItemState itemState = detector._CurItemState;
            if (itemState < ItemState.eStateFour)
            {
                detector._CurItemState = itemState + 1;
                itemHoldInHand.ChangeSprite(detector._CurItemState, 1f);
            }
        }
    }

    void TimeLock()
    {
        Debug.Log("TimeLock");
        if (m_TempStayDestroys.Count <= 0) return;

        for (int i = 0; i < m_TempStayDestroys.Count; i++)
        {
            Item itemHoldInHand = m_TempStayDestroys[i].GetComponent<Item>();
            ItemDetector detector = m_TempStayDestroys[i].GetComponent<ItemDetector>();
            ItemState itemState = detector._CurItemState;
            if (itemState > ItemState.eStateOne)
            {
                detector._CurItemState = ItemState.eStateOne;
                itemHoldInHand.ChangeSprite(detector._CurItemState, 1f);
            }
        }
    }

    public void FetchGameObject(GameObject obj)
    {
        if (!m_FetchGameObjectFlag) return;
        if (_HoldInHand) return;

        Debug.LogError("FetchGameObject");
        _HoldInHand = true;
        ItemDetector holdInHand = obj.GetComponent<ItemDetector>();
        CharacterPackage.instance.SaveItem(holdInHand, obj);
    }
}
