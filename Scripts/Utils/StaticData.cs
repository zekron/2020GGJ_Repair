using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaticData : MonoBehaviour
{
    public static Color ColorFadeOut = new Color(1, 1, 1, 0);
    public static Color ColorFull = new Color(1, 1, 1, 1);
    public static float DestroyDuration = 0.5f;
}

#region Enum
public enum ItemState
{
    eStateOne,
    eStateTwo,
    eStateThree,
    eStateFour
}
#endregion

#region Event
public class MyIntEvent : UnityEvent<int> { }
public class MyItemStateEvent : UnityEvent<ItemState> { }
#endregion
