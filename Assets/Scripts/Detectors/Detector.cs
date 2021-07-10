using UnityEngine;

[RequireComponent(typeof(BoxCollider2D),typeof(Rigidbody2D))]
public class Detector : MonoBehaviour
{
    private bool _enterDestroy;
    private bool _stayDestroy;
    private bool _exitDestroy;
    /// <summary>
    /// 是否被瘟疫光环影响
    /// </summary>
    public bool _CanBeDetected = false;
    /// <summary>
    /// 是否被回到过去影响
    /// </summary>
    public bool _IsInTimeWalkBack = false;

    #region 属性
    /// <summary>
    /// 是否刚进入瘟疫光环
    /// </summary>
    public bool EnterDestroy
    {
        get { return _enterDestroy; }
        set
        {
            _enterDestroy = value;
            _stayDestroy = !value;
            _exitDestroy = !value;
        }
    }
    /// <summary>
    /// 是否在瘟疫光环中
    /// </summary>
    public bool StayDestroy
    {
        get { return _stayDestroy; }
        set
        {
            _enterDestroy = !value;
            _stayDestroy = value;
            _exitDestroy = !value;
        }
    }
    /// <summary>
    /// 是否刚离开瘟疫光环
    /// </summary>
    public bool ExitDestroy
    {
        get { return _exitDestroy; }
        set
        {
            _enterDestroy = !value;
            _stayDestroy = !value;
            _exitDestroy = value;
        }
    }

    #endregion
}
