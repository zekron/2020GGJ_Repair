using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private bool m_EnterDestroy;
    private bool m_StayDestroy;
    private bool m_ExitDestroy;
    /// <summary>
    /// 是否被瘟疫光环影响
    /// </summary>
    public bool _CanBeDetected = false;
    /// <summary>
    /// 是否被回到过去影响
    /// </summary>
    public bool _InTimeWalkBack = false;

    #region 属性
    /// <summary>
    /// 是否刚进入瘟疫光环
    /// </summary>
    public bool EnterDestroy
    {
        get { return m_EnterDestroy; }
        set
        {
            m_EnterDestroy = value;
            m_StayDestroy = !value;
            m_ExitDestroy = !value;
        }
    }
    /// <summary>
    /// 是否在瘟疫光环中
    /// </summary>
    public bool StayDestroy
    {
        get { return m_StayDestroy; }
        set
        {
            m_EnterDestroy = !value;
            m_StayDestroy = value;
            m_ExitDestroy = !value;
        }
    }
    /// <summary>
    /// 是否刚离开瘟疫光环
    /// </summary>
    public bool ExitDestroy
    {
        get { return m_ExitDestroy; }
        set
        {
            m_EnterDestroy = !value;
            m_StayDestroy = !value;
            m_ExitDestroy = value;
        }
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
