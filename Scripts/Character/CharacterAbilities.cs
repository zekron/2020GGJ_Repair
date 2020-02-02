using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterAbilities : MonoBehaviour
{
    public static CharacterAbilities instance = null;
    public KeyCode _KeyTimeWalkBack = KeyCode.Q;
    public KeyCode _KeyFetchGameObject = KeyCode.W;

    private bool m_HoldTimeFlag = false;
    private bool m_FetchGameObjectFlag = false;
    private float m_HoldTime = 0f;

    private GameObject _HoldInHand;
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
            m_HoldTime = 0;
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
        if (!_HoldInHand) return;

        Item itemHoldInHand = _HoldInHand.GetComponent<Item>();
        ItemDetector detector = _HoldInHand.GetComponent<ItemDetector>();
        ItemState itemState = detector._CurItemState;
        if (itemState < ItemState.eStateFour)
        {
            detector._CurItemState = itemState + index;
            itemHoldInHand.ChangeSprite(detector._CurItemState);
        }
    }

    public void FetchGameObject(GameObject obj)
    {
        if (!m_FetchGameObjectFlag) return;
        if (_HoldInHand) return;

        Debug.LogError("FetchGameObject");
        _HoldInHand = obj;
        _HoldInHand.transform.SetParent(transform.parent);
        _HoldInHand.transform.DOLocalMove(new Vector3(2, 1, 0), 0.3f);
    }
}
