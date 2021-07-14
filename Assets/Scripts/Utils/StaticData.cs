using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public static class StaticData
{
    public static string PackageName = "Calamoto";
    public static string PackageVer = "TestVer";
    public static string PackageTime = "2021/0713/01/1150";

    public static Color ColorFadeOut = new Color(1, 1, 1, 0);
    public static Color ColorFull = new Color(1, 1, 1, 1);
    public static Vector3 HalfScale = new Vector3(0.5f, 0.5f, 1);
    public static Vector3 DefaultRebirthVector = new Vector3(-50.8F, 15, 0);

    public const float DestroyDuration = 0.2f;

    public const int DEFAULT_WIDTH = 3840;
    public const int DEFAULT_HEIGHT = 2160;
    public const float DEFAULT_CAMERA_SIZE = 10.8f;
    public const float DEFAULT_CAMERA_SCALE = 1.25F;


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
    //BaseItem
    StateOne,
    StateTwo,
    StateThree,
    StateFour,

    //AncientItem
    StateFinished,

    //
}
public enum GameItemType
{
    Tulip,
    Rose,
    Tree,
    Tomb,
    Candle,

    PoisonousPool,
    Fire,

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
