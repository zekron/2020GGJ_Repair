using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protagonist : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private IntEventChannelSO _inflictDamageEvent;
    [SerializeField] private IntEventChannelSO _inflictHealingEvent;

    [Header("Controller")]
    [SerializeField] private Prime31.CharacterController2D _characterController;
    [SerializeField] private MoveController _moveController;
    [SerializeField] private CameraController _cameraController;

    [Header("Component")]
    [SerializeField] private CharacterAbilities _abilities;
    [SerializeField] private Damageable _damageable;
    [SerializeField] private GameObject _hands;
    [SerializeField] private SpriteRenderer _carrying;

    private Vector3 _rebirthVector;
    public CameraController CamController => _cameraController;

    private void OnEnable()
    {
        //DontDestroyOnLoad(gameObject);
    }
    private void SetCameraController(CameraController controller)
    {
        _cameraController = controller;
        _cameraController.SetTarget(transform);
    }
    public void Init(CameraController controller, Vector3 rebirthVector)
    {
        SetCameraController(controller);
        _abilities.SetRebirthPoint(_rebirthVector = rebirthVector);
        _abilities.RebirthCharacter();

        _inflictDamageEvent.OnEventRaised += ReceiveDamage;
        _inflictHealingEvent.RaiseEvent(3);
    }
    public void SetHands(Sprite sprite)
    {
        _carrying.sprite = sprite;
        _carrying.DOComplete();
        if (sprite)
        {
            _carrying.DOFade(1, 1).OnStart(() =>
            {
                _hands.SetActive(sprite);
            });
        }
        else
        {
            _carrying.DOFade(0, 1).OnComplete(() =>
            {
                _hands.SetActive(sprite);
            });
        }
    }
    void ReceiveDamage(int damage)
    {
        GetComponent<SpriteRenderer>().DOFade(0, 0.2f).OnComplete(() => GetComponent<SpriteRenderer>().DOFade(1, 0.2f)).SetLoops(2);
    }
}
