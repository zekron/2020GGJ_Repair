using DG.Tweening;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public Sprite[] _ObjectSprites;

    public SpriteRenderer _CurSprite;
    public SpriteRenderer _NewSprite;

    public void Start()
    {
        if (_ObjectSprites.Length > 0)
        {
            _CurSprite.sprite = _ObjectSprites[_ObjectSprites.Length - 1];
            _NewSprite.sprite = _ObjectSprites[_ObjectSprites.Length - 2];
        }
        OnStart();
    }

    public virtual void OnStart() { }

    public virtual void ChangeSprite(eItemState state, float duration = StaticData.DestroyDuration)
    {
        if (_ObjectSprites.Length <= 0) return;

        _CurSprite.DOComplete();
        _NewSprite.DOComplete();

        _NewSprite.sprite = _ObjectSprites[(int)state];

        //_CurSprite.sprite = _NewSprite.sprite;
        //if (state > ItemState.eStateOne)
        //    _NewSprite.sprite = _ObjectSprites[(int)state - 1];

        _NewSprite.DOFade(1, duration)
            .OnComplete(
            () =>
            {
                _NewSprite.color = StaticData.ColorFadeOut;
            });
        _CurSprite.DOFade(0, duration)
            .OnComplete(
            () =>
            {
                _CurSprite.sprite = _NewSprite.sprite;
                if (state > eItemState.eStateOne)
                    _NewSprite.sprite = _ObjectSprites[(int)state - 1];
                _CurSprite.color = StaticData.ColorFull;
            });
    }
    public void ChangeSprite(eAncientState state, float duration = StaticData.DestroyDuration)
    {
        _CurSprite.DOComplete();
        _NewSprite.DOComplete();

        _NewSprite.sprite = _ObjectSprites[(int)state];

        _NewSprite.DOFade(1, duration)
            .OnComplete(
            () =>
            {
                _NewSprite.color = StaticData.ColorFadeOut;
            });
        _CurSprite.DOFade(0, duration)
            .OnComplete(
            () =>
            {
                _CurSprite.sprite = _NewSprite.sprite;
                if (state > eAncientState.eStateOne)
                    _NewSprite.sprite = _ObjectSprites[(int)state - 1];
                _CurSprite.color = StaticData.ColorFull;
            });
    }
}
