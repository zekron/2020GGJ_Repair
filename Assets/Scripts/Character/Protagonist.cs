using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protagonist : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private IntEventChannelSO _inflictHealingEvent;

    [Header("Controller")]
    [SerializeField] private Prime31.CharacterController2D _characterController;
    [SerializeField] private MoveController _moveController;
    [SerializeField] private CameraController _cameraController;

    [Header("Component")]
    [SerializeField] private CharacterAbilities _abilities;
    [SerializeField] private Damageable _damageable;

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

        _inflictHealingEvent.RaiseEvent(3);
    }
}
