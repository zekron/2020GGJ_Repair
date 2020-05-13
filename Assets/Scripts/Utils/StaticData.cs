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
    public static string PackageTime = "2020/0311/01/2316";

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

[Serializable]
public class Heart
{
    public SpriteRenderer _Heart;
    public eHeartState _State;
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
    eTulip,
    eRose,
    eTree,
    eTomb,
    eCandle,
    ePoisonousPool,
    eTile,
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
    eInWelcome,
    eInGame,
    eInSetting,
}
public enum eHeartState
{
    eFull,
    eEmpty,
}
#endregion

#region Event
public class MyIntEvent : UnityEvent<int> { }
public class MyItemStateEvent : UnityEvent<eItemState> { }
public class MyGameStateEvent : UnityEvent<eGameState> { }
#endregion
