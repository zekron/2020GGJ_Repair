using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHeart : MonoBehaviour
{
    [SerializeField] private HeartBarConfigSO _heartBarConfig;
    [SerializeField] private Image _imageHeart;
    private bool _isFull = false;

    public void SetSprite()
    {
        _isFull = !_isFull;

        _imageHeart.sprite = _heartBarConfig.HealthBarSprites[_heartBarConfig.GetHeartIndex(_isFull)];
    }
}
