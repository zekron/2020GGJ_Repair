using Prime31;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public static SmoothFollow instance = null;

    public Transform target;
    [SerializeField] private Material _Background;

    public float smoothDampTime = 0.2f;
    [HideInInspector]
    public new Transform transform;
    public Vector3 cameraOffset;
    public bool useFixedUpdate = false;
    public bool _ForcedView = false;

    private CharacterController2D _playerController;
    private Vector3 _smoothDampVelocity;
    private float m_CameraThreshold;
    private Vector3 m_DefaultTrans;

    void Awake()
    {
        instance = this;
        transform = gameObject.transform;
        m_DefaultTrans = transform.position;
        _playerController = target.GetComponent<CharacterController2D>();
    }

    private void Start()
    {
        updateCameraPosition(true);
    }


    void LateUpdate()
    {
        if (!useFixedUpdate)
            updateCameraPosition();
    }


    void FixedUpdate()
    {
        if (useFixedUpdate)
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
            transform.position = target.position - cameraOffset;
        }
        else
        {
            if (_playerController == null)
            {
                transform.position = Vector3.SmoothDamp(transform.position, target.position - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
                return;
            }

            if (_playerController.velocity.x > 0)
            {
                transform.position = Vector3.SmoothDamp(transform.position, target.position - cameraOffset, ref _smoothDampVelocity, smoothDampTime);
            }
            else
            {
                var leftOffset = cameraOffset;
                leftOffset.x *= -1;
                transform.position = Vector3.SmoothDamp(transform.position, target.position - leftOffset, ref _smoothDampVelocity, smoothDampTime);
            }

        }
        LoopBackground();
    }

    void LoopBackground()
    {
        Vector2 tempVec = new Vector2(((transform.position.x - m_DefaultTrans.x) / m_CameraThreshold) % 1, 0);
        _Background?.SetTextureOffset("_MainTex", tempVec);
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
