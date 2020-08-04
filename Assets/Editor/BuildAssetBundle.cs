using System.IO;
using UnityEditor;


/// <summary>
/// 打包脚本 —— 放在 Editor 文件夹下(规范)
/// </summary>
public class ChinarAssetBundle
{
    [MenuItem("Editor Build/Build AssetsBundle")] //菜单栏添加按钮
    static void BuildAllAssetsBundles()
    {
        string folder = "CalamotoAssetBundles";                                                               //定义文件夹名字
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);                                     //文件夹不存在，则创建
        BuildPipeline.BuildAssetBundles(folder, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows); //创建AssetBundle
    }
}