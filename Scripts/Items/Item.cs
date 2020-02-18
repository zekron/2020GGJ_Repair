using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : InteractiveObject
{
    public eItemState[] _AnimatedStates;
    public SpriteRenderer _AnimatorSprite;

    private Animator m_Animator;
    private ItemDetector m_Detector;
    public override void OnStart()
    {
        base.OnStart();
        m_Detector = GetComponent<ItemDetector>();
        if (_AnimatedStates.Length != 0)
            m_Animator = _AnimatorSprite.GetComponent<Animator>();
    }

    public void Update()
    {

        PlayItemAnimator(_AnimatedStates);
    }

    void PlayItemAnimator(eItemState[] states)
    {
        if (states.Length == 0) return;

        if (!_AnimatorSprite.enabled)
        {
            for (int i = 0; i < states.Length; i++)
            {
                if (m_Detector._CurItemState == states[i])
                {
                    m_Animator.SetInteger("State", (int)m_Detector._CurItemState);
                    _AnimatorSprite.sprite = _CurSprite.sprite;
                    _AnimatorSprite.enabled = true;
                    _CurSprite.enabled = false;
                    return;
                }
            }
        }
        else
        {
            if (m_Detector._CurItemState != states[0])
            {
                _CurSprite.enabled = true;
                m_Animator.SetInteger("State", (int)m_Detector._CurItemState);
                _AnimatorSprite.enabled = false;
                return;
            }
        }
    }
}
