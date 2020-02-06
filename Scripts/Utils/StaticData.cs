using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public static class StaticData
{
    public static string PackageName = "Calamoto";
    public static string PackageTime = "20/0206/01 17:20";
    public static string PackageVer = "TestVer";

    public static Color ColorFadeOut = new Color(1, 1, 1, 0);
    public static Color ColorFull = new Color(1, 1, 1, 1);
    public const float DestroyDuration = 0.2f;


    public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
    {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }
}

[Serializable]
public class AncientKey
{
    public AncientState _AncientKeyState;
    public ItemState _ItemKeyState;
    public ItemType _ItemKeyType;
}

[Serializable]
public class BrokenAncientSprite
{
    public Sprite[] _Sprites;
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
    PoisonousPool,
    Tile,
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
