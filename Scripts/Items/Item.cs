using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : InteractiveObject
{
    public ItemState[] _AnimatedStates;
    public SpriteRenderer _AnimatorSprite;

    private ItemDetector m_Detector;
    public override void OnStart()
    {
        base.OnStart();
        m_Detector = GetComponent<ItemDetector>();
    }

    public void Update()
    {

        PlayItemAnimator(_AnimatedStates);
    }

    void PlayItemAnimator(ItemState[] states)
    {
        if (states.Length == 0) return;

        if (!_AnimatorSprite.enabled)
        {
            for (int i = 0; i < states.Length; i++)
            {
                if (m_Detector._CurItemState == states[i])
                {
                    _AnimatorSprite.GetComponent<Animator>().SetInteger("State", (int)m_Detector._CurItemState);
                    _AnimatorSprite.enabled = true;
                    return;
                }
            }
        }
        else
        {
            if (m_Detector._CurItemState != states[0])
            {
                _AnimatorSprite.GetComponent<Animator>().SetInteger("State", (int)m_Detector._CurItemState);
                _AnimatorSprite.enabled = false;
                return;
            }
        }
    }
}
