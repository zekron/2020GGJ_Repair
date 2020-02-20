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

        m_Detector.Add_OnItemStateChanged(PlayItemAnimator);
    }

    void PlayItemAnimator(eItemState state)
    {
        if (_AnimatedStates.Length == 0) return;

        for (int i = 0; i < _AnimatedStates.Length; i++)
        {
            if (state == _AnimatedStates[i])
            {
                m_Animator.SetInteger("State", (int)state);
                _AnimatorSprite.sprite = _CurSprite.sprite;
                _AnimatorSprite.enabled = true;
                _CurSprite.enabled = false;
                return;
            }
        }

        _CurSprite.enabled = true;
        m_Animator.SetInteger("State", (int)state);
        _AnimatorSprite.enabled = false;
        return;
    }
}
