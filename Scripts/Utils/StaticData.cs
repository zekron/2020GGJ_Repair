using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public static class StaticData
{
    public static string PackageName = "Calamoto";
    public static string PackageVer = "TestVer";
    public static string PackageTime = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
        Convert.ToString(2020,  16),
        Convert.ToString('/',   16),
        Convert.ToString(0224,  16),
        Convert.ToString('/',   16),
        Convert.ToString(1080,  16),
        Convert.ToString(01,    16),
        Convert.ToString('/',   16),
        Convert.ToString(' ',   16),
        Convert.ToString(23,    16),
        Convert.ToString(':',   16),
        Convert.ToString(31,    16));

    public static Color ColorFadeOut = new Color(1, 1, 1, 0);
    public static Color ColorFull = new Color(1, 1, 1, 1);
    public static Vector3 HalfScale = new Vector3(0.5f, 0.5f, 1);
    public const float DestroyDuration = 0.2f;


    public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
    {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }
}

[Serializable]
public class AncientKey
{
    public eAncientState _AncientKeyState;
    public eItemState _ItemKeyState;
    public eItemType _ItemKeyType;
}

[Serializable]
public class BrokenAncientSprite
{
    public Sprite[] _Sprites;
}

#region Enum
public enum eItemState
{
    eStateOne,
    eStateTwo,
    eStateThree,
    eStateFour,
}
public enum eItemType
{
    Tulip,
    Rose,
    Tree,
    Tomb,
    Candle,
    PoisonousPool,
    Tile,
}
public enum eAncientState
{
    eStateOne,
    eStateTwo,
    eStateThree,
    eStateFour,
    eStateFive,
}
public enum eAncientType
{
    TulipAncient,
    RoseAncient,
}
public enum eGameState
{
    InWelcome,
    InGame,
    InSetting,
}
#endregion

#region Event
public class MyIntEvent : UnityEvent<int> { }
public class MyItemStateEvent : UnityEvent<eItemState> { }
public class MyGameStateEvent : UnityEvent<eGameState> { }
#endregion
