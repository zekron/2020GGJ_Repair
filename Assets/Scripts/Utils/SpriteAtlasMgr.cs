using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.U2D;
using UnityEngine.U2D;

[CreateAssetMenu(fileName = "New SpriteAtlasManagerSO", menuName = "Scriptable Object/SpriteAtlasManagerSO")]
public class SpriteAtlasMgr : ScriptableObject
{
    [SerializeField] SpriteAtlas[] _winSpriteAtlas;
    [SerializeField] SpriteAtlas[] _webGLSpriteAtlas;
#if UNITY_EDITOR
    private void OnEnable()
    {
#if UNITY_STANDALONE_WIN
        foreach (var atlas in _webGLSpriteAtlas)
            SpriteAtlasExtensions.SetIncludeInBuild(atlas, true);
        foreach (var atlas in _winSpriteAtlas)
            SpriteAtlasExtensions.SetIncludeInBuild(atlas, false);
#elif UNITY_WEBGL
        foreach (var atlas in _winSpriteAtlas)
            SpriteAtlasExtensions.SetIncludeInBuild(atlas, true);
        foreach (var atlas in _webGLSpriteAtlas)
            SpriteAtlasExtensions.SetIncludeInBuild(atlas, false);
#endif
    }
#endif
}
