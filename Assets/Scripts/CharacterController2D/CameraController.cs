using Prime31;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float smoothDampTime = 0.2f;
    [HideInInspector]
    public Transform _cameraTransform;
    public Vector3 cameraOffset;
    public bool useFixedUpdate = false;
    public bool _ForcedView = false;

    [SerializeField] private Material _Background;

    private CharacterController2D _playerController;
    private Transform _target;
    private Vector3 _smoothDampVelocity;
    private float m_CameraThreshold;
    private Vector3 m_DefaultTrans;

    void Awake()
    {
        _cameraTransform = gameObject.transform;
        m_DefaultTrans = _cameraTransform.position;
    }

    private void Start()
    {
    }


    void LateUpdate()
    {
        if (_target && !useFixedUpdate)
            updateCameraPosition();
    }


    void FixedUpdate()
    {
        if (_target && useFixedUpdate)
            updateCameraPosition();
    }

    private void OnDestroy()
    {
        _Background.SetTextureOffset("_MainTex", Vector2.zero);
    }

    void updateCameraPosition(bool force = false)
    {
        //if (_playerController.velocity.x == 0 && !force) return;

        if (_ForcedView)
        {
            _cameraTransform.position = _target.position - cameraOffset;
        }
        else
        {
            if (_playerController == null)
            {
                _cameraTransform.position = Vector3.SmoothDamp(_cameraTransform.position, _target.position - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
                return;
            }

            if (_playerController.velocity.x > 0)
            {
                _cameraTransform.position = Vector3.SmoothDamp(_cameraTransform.position, _target.position - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
            }
            else
            {
                var leftOffset = cameraOffset;
                leftOffset.x *= -1;
                _cameraTransform.position = Vector3.SmoothDamp(_cameraTransform.position, _target.position - leftOffset, ref _smoothDampVelocity, smoothDampTime);
            }

        }
        LoopBackground();
    }

    void LoopBackground()
    {
        Vector2 tempVec = new Vector2(((_cameraTransform.position.x - m_DefaultTrans.x) / m_CameraThreshold) % 1, 0);
        _Background?.SetTextureOffset("_MainTex", tempVec);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _playerController = _target.GetComponent<CharacterController2D>();
        updateCameraPosition(true);
    }
    public void SetCameraOffset(Vector3 scale)
    {
        cameraOffset = new Vector3(
            cameraOffset.x * scale.x,
            cameraOffset.y * scale.y,
            cameraOffset.z * scale.z);

        if (_Background)
            m_CameraThreshold = StaticData.DEFAULT_WIDTH * scale.x * 0.01f * StaticData.DEFAULT_CAMERA_SCALE;
    }
}
