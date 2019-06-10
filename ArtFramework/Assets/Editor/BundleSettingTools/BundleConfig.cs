using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class BundleConfig {
    
    public static string SceneSuffix = ".unity";
    public static string PanelRoot = "Canvas";
    public static string PanelPrefix = "Panel";

    public static string PrefabPath = "Assets/Prefab/";
    public static string SpritePath = "Assets/Texture/cn/";
    public static string PackingPath = "Assets/SpritePacking/cn/";

    public static string SpriteSuffix = ".png";
    public static string MetaSuffix = ".meta";

    public static string StreamingAssets = "Assets/StreamingAssets/";


    public static List<string> SpriteImportPaths = new List<string>()
    {
        "Assets/Texture/cn/"
    };

    public static bool IsSpriteImportPaths(string targetPath)
    {
        foreach (var path in SpriteImportPaths)
        {
            if (targetPath.Contains(path)) return true;
        }

        return false;
    }

    public static string GetDefaultSpritePackingTag(string targetPath)
    {
        foreach (var path in SpriteImportPaths)
        {
            if (targetPath.Contains(path))
            {
                string directoryName = Path.GetDirectoryName(targetPath);
                return directoryName.Replace(path, "").Replace("/", ".");
            }
        }

        return "Default";
    }

}
