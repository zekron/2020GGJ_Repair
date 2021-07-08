using UnityEngine;

[CreateAssetMenu(fileName = "New SpriteAtlasManagerSO", menuName = "Scriptable Object/SpriteAtlasManagerSO")]
public class SpriteAtlasMgr : ScriptableObject
{
    [SerializeField] UnityEngine.U2D.SpriteAtlas[] _winSpriteAtlas, _webGLSpriteAtlas;
#if UNITY_EDITOR
    private void OnEnable()
    {
#if UNITY_STANDALONE_WIN
        foreach (var atlas in _webGLSpriteAtlas)
            UnityEditor.U2D.SpriteAtlasExtensions.SetIncludeInBuild(atlas, true);
        foreach (var atlas in _winSpriteAtlas)
            UnityEditor.U2D.SpriteAtlasExtensions.SetIncludeInBuild(atlas, false);
#elif UNITY_WEBGL
        foreach (var atlas in _winSpriteAtlas)
            UnityEditor.U2D.SpriteAtlasExtensions.SetIncludeInBuild(atlas, true);
        foreach (var atlas in _webGLSpriteAtlas)
            UnityEditor.U2D.SpriteAtlasExtensions.SetIncludeInBuild(atlas, false);
#endif
    }
#endif
}
