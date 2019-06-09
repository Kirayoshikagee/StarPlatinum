using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using FileMode = System.IO.FileMode;

public class BuildBundle {

    [MenuItem("Assets/Altas/Build Single AssetBundle", false, 80)]
    public static void BuildAltas()
    {
        GameObject selectGameObject = Selection.activeGameObject;
        if (selectGameObject == null)
        {
            Debug.LogError("请选中一个prefab");
        }

        string path = AssetDatabase.GetAssetPath(selectGameObject);
        string folderPath = Path.GetFileNameWithoutExtension(path);

        string spritePath = string.Format("{0}{1}", BundleConfig.SpritePath, Path.GetFileNameWithoutExtension(folderPath));

        DirectoryInfo spriteDir = new DirectoryInfo(spritePath);

        List<Texture2D> sprites = new List<Texture2D>();

        //整理Texture2D数组
        List<string> fileName = new List<string>();
        foreach (var fileInfo in spriteDir.GetFiles())
        {
            if (fileInfo.Name.EndsWith(BundleConfig.SpriteSuffix) && !fileInfo.Name.EndsWith(BundleConfig.MetaSuffix))
            {
                fileName.Add(Path.GetFileNameWithoutExtension(fileInfo.Name));
                using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    byte[] bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, (int) fs.Length);
                    Texture2D texture = new Texture2D(0, 0);
                    texture.LoadImage(bytes);
                    sprites.Add(texture);
                }
            }
        }
        //合图集
        int width = 0, height = 0;
        for (int i = 0; i < sprites.Count; i++)
        {
            width += sprites[i].width;
            if (i > 0)
            {
                if (sprites[i].height > sprites[i - 1].height)
                    height = sprites[i].height;
            }
            else
            {
                height = sprites[i].height;
            }
        }

        Texture2D mix = new Texture2D(width, height);

        //return rects
        Rect[] slices = mix.PackTextures(sprites.ToArray(), 2, 2048, false);
        mix.Apply();
        
        byte[] bt = mix.EncodeToPNG();
        string storePath = spritePath.Replace(BundleConfig.SpritePath, BundleConfig.PackingPath);
        if (!Directory.Exists(storePath))
        {
            Directory.CreateDirectory(storePath);
        }

        string targetFile = string.Format("{0}/{1}.png", storePath, folderPath).Replace(@"\", @"//");
        if (File.Exists(targetFile))
        {
            File.Delete(targetFile);
        }

        using (FileStream fs = new FileStream(targetFile, FileMode.OpenOrCreate))
        {
            fs.Write(bt, 0, bt.Length);   
            fs.Flush();
        }
        AssetDatabase.Refresh();
        TextureImporter importer = AssetImporter.GetAtPath(targetFile) as TextureImporter;
        if (importer != null)
        {
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Multiple;
            importer.mipmapEnabled = false;
            
            var blocks = new SpriteMetaData[slices.Length];
            int MIX_WIDTH = mix.width;
            int MIX_HEIGHT = mix.height;
            for (int i = 0; i < slices.Length; i++)
            {
                blocks[i] = new SpriteMetaData();
                blocks[i].name = fileName[i];
                blocks[i].rect = new Rect(MIX_WIDTH * slices[i].x, MIX_HEIGHT * slices[i].y, MIX_WIDTH * slices[i].width, MIX_HEIGHT * slices[i].height);
            }

            importer.spritesheet = blocks;
            importer.fadeout = true;
            importer.SaveAndReimport();
            importer.fadeout = false;
            importer.SaveAndReimport();
        }
        AssetDatabase.Refresh();

        string outputPath = Path.Combine(BundleConfig.StreamingAssets,
            ApplicationConfig.GetPlatform(EditorUserBuildSettings.activeBuildTarget));

        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        AssetBundleBuild[] bundleBuild = new AssetBundleBuild[2];

        bundleBuild[0].assetBundleName = path.Substring(0, path.IndexOf(".")).Replace("/", "$");
        bundleBuild[0].assetBundleVariant = "unity3d";
        bundleBuild[0].assetNames = new string[]
        {
            path
        };
        

        bundleBuild[1].assetBundleName = spritePath.Replace("/", "$");
        bundleBuild[1].assetBundleVariant = "unity3d";
        bundleBuild[1].assetNames = new string[]
        {
            targetFile
        };
        Debug.LogError("outputPath:"+outputPath);
        Debug.LogError("path资源路径："+path+";;;;"+ path.Substring(0, path.IndexOf(".")).Replace("/", "_"));
        Debug.LogError("targetFile资源路径：" + targetFile+";;;;;"+ spritePath.Replace("/", "_"));


        BuildPipeline.BuildAssetBundles(outputPath,bundleBuild, BuildAssetBundleOptions.None,
            EditorUserBuildSettings.activeBuildTarget);

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 清除之前设置过的AssetBundleName，避免产生不必要的资源也打包
    /// </summary>
    static void ClearAssetBundleName()
    {
        string[] assetBundleNames = AssetDatabase.GetAllAssetBundleNames();
        int length = assetBundleNames.Length;
        string[] oldAssetBundleNames = new string[length];

        for (int i = 0; i < length; i++)
        {
            oldAssetBundleNames[i] = assetBundleNames[i];
        }

        for (int i = 0; i < oldAssetBundleNames.Length; i++)
        {
            AssetDatabase.RemoveAssetBundleName(oldAssetBundleNames[i], true);
        }
    }

    static void PackFolder(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Debug.LogError("该路径不存在："+directoryPath);
            return;
        }
        DirectoryInfo folder = new DirectoryInfo(directoryPath);
        FileSystemInfo[] files = folder.GetFileSystemInfos();
        int length = files.Length;

        for (int i = 0; i < length; i++)
        {
            if (files[i] is DirectoryInfo)
            {
                PackFolder(files[i].FullName);
            }
            else
            {
                if (!files[i].Name.EndsWith(BundleConfig.MetaSuffix))
                {
                    PackFile(files[i].FullName);
                }
            }
        }
        
    }

    static void PackFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("该路径不存在："+filePath);
            return;
        }

        string formatFilePath = filePath.Replace("\\", "/");
        string assetPath = string.Format("{0}{1}", "Assets", formatFilePath.Substring(Application.dataPath.Length));
        Debug.LogError("Application.dataPath:" + formatFilePath.Substring(Application.dataPath.Length));
        string assetPath2 = formatFilePath.Substring(Application.dataPath.Length + 1);
        Debug.LogError("AssetPath2:"+ assetPath2);

        //在代码中给资源设置AssetBundleName
        AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
        string assetName = assetPath2.Substring(assetPath2.IndexOf("/") + 1);
        Debug.LogError("AssetName:"+assetName);
        assetName = assetName.Replace(Path.GetExtension(assetName), ".unity3d");

        assetImporter.assetBundleName = assetName;
    }
}
