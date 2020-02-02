using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour
{
    public KeyCode _KeyTimeWalkBack = KeyCode.Q;

    private bool m_HoldTimeFlag = false;
    private float m_HoldTime = 0f;
    private GameObject _HoldInHand;
    // Start is called before the first frame update
    void Start()
    {

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

    void FetchGameObject()
    {
        if (_HoldInHand) return;


    }
}
