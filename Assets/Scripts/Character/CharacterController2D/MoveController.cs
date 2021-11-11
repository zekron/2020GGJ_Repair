using Prime31;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CharacterController2D))]
public class MoveController : MonoBehaviour
{
    public static MoveController instance = null;
    // movement config
    [SerializeField] public float gravity = -25f;
    [SerializeField] public float runSpeed = 8f;
    [SerializeField] public float groundDamping = 20f; // how fast do we change direction? higher means faster
    [SerializeField] public float inAirDamping = 5f;
    [SerializeField] public float jumpHeight = 3f;
    [SerializeField] private float normalizedHorizontalSpeed = 0;
    [SerializeField] private InGameInputEventSO inputEvents;

    private CharacterController2D _controller;
    private Animator _animator;
    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 _velocity;

    private void OnEnable()
    {
        inputEvents.OnMoveEvent += OnMove;
        inputEvents.OnStopMoveEvent += OnStopMove;
    }

    private void OnDisable()
    {
        inputEvents.OnMoveEvent -= OnMove;
        inputEvents.OnStopMoveEvent -= OnStopMove;
    }

    private void Awake()
    {
        instance = this;
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();
    }

    private void Start()
    {
        _velocity.y = gravity * Time.deltaTime;
    }

    #region Event Listeners

    void onControllerCollider(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they are not very interesting
        if (hit.normal.y == 1f)
            return;

        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
        //Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
    }

    #endregion


    // the Update loop contains a very simple example of moving the character around and controlling the animation
    void Update()
    {
        if (GameMgr.Instance.GetGameStateMgr().GameState != GameState.InGameplay) return;
        if (_velocity.x == 0 && _velocity.y == 0) return;

        if (_controller.isGrounded)
        {
            if (Mathf.Abs( _velocity.x) <= 1e-3)
            {
                _velocity.y = 0;
                _animator.Play(Animator.StringToHash("Idle"));
                SoundMgr.instance.StopEff(0);
            }
            else
            {
                _animator.Play(Animator.StringToHash("Run"));
                if (!SoundMgr.instance.IsPlaying(1, 0))
                    SoundMgr.instance.PlayEff(SoundMgr.instance._Effect._Walk, 0);
            }
        }

        // apply horizontal speed smoothing it. don't really do this with Lerp. Use SmoothDamp or something that provides more control
        var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
        _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);

        // apply gravity before moving
        _velocity.y += gravity * Time.deltaTime;

        _controller.move(_velocity * Time.deltaTime);

        // grab our current _velocity to use as a base for all calculations
        _velocity = _controller.velocity;
    }

    private void OnMove(Vector2 moveInput)
    {
        if (GameMgr.Instance.GetGameStateMgr().GameState != GameState.InGameplay) return;
        Vector2 moveAmount = moveInput * runSpeed;
        normalizedHorizontalSpeed = moveInput.x;

        if (_controller.isGrounded && moveAmount.y > 0)
        {
            _velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            _animator.Play(Animator.StringToHash("Jump"));
            SoundMgr.instance.PlayEff(SoundMgr.instance._Effect._Jump, 0);
        }

        if (moveInput.x != 0)
        {
            transform.localScale = new Vector3(moveInput.x > 0 ? 1 : -1,
                                                transform.localScale.y,
                                                transform.localScale.z);
        }
        _controller.move(_velocity * Time.deltaTime);
    }

    private void OnStopMove()
    {
        normalizedHorizontalSpeed = 0;

        if (_controller.isGrounded)
        {
            _velocity.x = 0;
            _velocity.y += gravity * Time.deltaTime;
        }


        if (_controller.isGrounded)
        {
            _animator.Play(Animator.StringToHash("Idle"));
            SoundMgr.instance.StopEff(0);
        }
    }

    public void SetCharacterConfigScale(Vector3 scale)
    {
        gravity *= scale.x;
        runSpeed *= scale.x;
        groundDamping *= scale.x;
        inAirDamping *= scale.x;
        jumpHeight *= scale.x;
    }
}
