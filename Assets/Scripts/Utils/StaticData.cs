using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public static class StaticData
{
    public static string PackageName = "Calamoto";
    public static string PackageVer = "TestVer";
    public static string PackageTime = "2021/0711/01/0250";

    public static Color ColorFadeOut = new Color(1, 1, 1, 0);
    public static Color ColorFull = new Color(1, 1, 1, 1);
    public static Vector3 HalfScale = new Vector3(0.5f, 0.5f, 1);

    public const float DestroyDuration = 0.2f;

    public const int DEFAULT_WIDTH = 1920;
    public const int DEFAULT_HEIGHT = 1080;
    public const float DEFAULT_CAMERA_SIZE = 5.4f;


    public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
    {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }
}

[Serializable]
public class AncientKey
{
    public GameItemState _AncientKeyState;
    public GameItemState _ItemKeyState;
    public GameItemType _ItemKeyType;
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
    public GameHeartState _State;
}
#region Enum
public enum GameItemState
{
    StateOne,
    StateTwo,
    StateThree,
    StateFour,
    StateFinished,
}
public enum GameItemType
{
    Tulip,
    Rose,
    Tree,
    Tomb,
    Candle,
    PoisonousPool,
    Tile,
    Ancient,
}
public enum GameAncientState
{
    StateOne,
    StateTwo,
    StateThree,
    StateFour,
    StateFive,
}
public enum eAncientType
{
    TulipAncient,
    RoseAncient,
}
public enum GameState
{
    Null,
    InWelcome,
    InGameplay,
    InSetting,
}
public enum GameHeartState
{
    Full,
    Empty,
}
#endregion

#region Event
public class MyIntEvent : UnityEvent<int> { }
public class MyItemStateEvent : UnityEvent<GameItemState> { }
public class MyGameStateEvent : UnityEvent<GameState> { }
#endregion
