using UnityEngine;
using System.Collections;
using Prime31;


public class DemoScene : MonoBehaviour
{
    public static DemoScene instance = null;
    // movement config
    public float gravity = -25f;
    public float runSpeed = 8f;
    public float groundDamping = 20f; // how fast do we change direction? higher means faster
    public float inAirDamping = 5f;
    public float jumpHeight = 3f;

    [HideInInspector]
    private float normalizedHorizontalSpeed = 0;

    private CharacterController2D _controller;
    private Animator _animator;
    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 _velocity;


    void Awake()
    {
        instance = this;
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();
    }


    #region Event Listeners

    void onControllerCollider(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they arent very interesting
        if (hit.normal.y == 1f)
            return;

        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
        //Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
    }

    #endregion


    // the Update loop contains a very simple example of moving the character around and controlling the animation
    void Update()
    {
        if (FindObjectOfType<GameMgr>() && GameMgr.instance.GameState != eGameState.InGame) return;
        if (_controller.isGrounded)
            _velocity.y = 0;

        if (Input.GetKey(KeyCode.D))
        {
            normalizedHorizontalSpeed = 1;
            if (transform.localScale.x < 0f)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            if (_controller.isGrounded)
            {
                _animator.Play(Animator.StringToHash("Run"));
                //if (!SoundMgr.instance.IsPlaying(1, 0))
                //    SoundMgr.instance.PlayEff(SoundMgr.instance._Effect._Walk, 0);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            normalizedHorizontalSpeed = -1;
            if (transform.localScale.x > 0f)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            if (_controller.isGrounded)
            {
                _animator.Play(Animator.StringToHash("Run"));
                //if (!SoundMgr.instance.IsPlaying(1, 0))
                //    SoundMgr.instance.PlayEff(SoundMgr.instance._Effect._Walk, 0);
            }
        }
        else
        {
            normalizedHorizontalSpeed = 0;

            if (_controller.isGrounded)
            {
                _animator.Play(Animator.StringToHash("Idle"));
                //SoundMgr.instance.StopEff(0);
            }
        }


        // we can only jump whilst grounded
        if (_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            _animator.Play(Animator.StringToHash("Jump"));
            //SoundMgr.instance.PlayEff(SoundMgr.instance._Effect._Jump, 0);
        }


        // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
        var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
        _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);

        // apply gravity before moving
        _velocity.y += gravity * Time.deltaTime;

        // if holding down bump up our movement amount and turn off one way platform detection for a frame.
        // this lets us jump down through one way platforms
        if (_controller.isGrounded && Input.GetKey(KeyCode.S))
        {
            //_velocity.y *= 3f;
            //_controller.ignoreOneWayPlatformsThisFrame = true;
        }

        _controller.move(_velocity * Time.deltaTime);

        // grab our current _velocity to use as a base for all calculations
        _velocity = _controller.velocity;
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
