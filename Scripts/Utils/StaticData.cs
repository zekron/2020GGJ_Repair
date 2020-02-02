using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public static class StaticData
{
    public static Color ColorFadeOut = new Color(1, 1, 1, 0);
    public static Color ColorFull = new Color(1, 1, 1, 1);
    public const float DestroyDuration = 0.2f;


    public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
    {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }
}

#region Enum
public enum ItemState
{
    eStateOne,
    eStateTwo,
    eStateThree,
    eStateFour,
}
public enum ItemType
{
    Tulip,
    Rose,
    Tree,
    Tomb,
    Candle,
}
public enum AncientState
{
    eStateOne,
    eStateTwo,
    eStateThree,
    eStateFour,
    eStateFive,
}
public enum AncientType
{
    TulipAncient,
    RoseAncient,
}
#endregion

#region Event
public class MyIntEvent : UnityEvent<int> { }
public class MyItemStateEvent : UnityEvent<ItemState> { }
#endregion
