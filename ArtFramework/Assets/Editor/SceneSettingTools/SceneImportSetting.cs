using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneImportSetting : AssetModificationProcessor {

    static public void OnWillSaveAssets(string[] names)
    {
        foreach (var name in names)
        {
            if (name.EndsWith(BundleConfig.SceneSuffix))
            {
                Scene scene = SceneManager.GetSceneByPath(name);
                GameObject[] roots = scene.GetRootGameObjects();
                foreach (var gameObject in roots)
                {
                    if (gameObject.name.Equals(BundleConfig.PanelRoot))
                    {
                        Transform panelRoot = gameObject.transform;
                        int childCount = panelRoot.childCount;
                        for (int i = 0; i < childCount; i++)
                        {
                            Transform child = panelRoot.GetChild(i);
                            if (child.name.StartsWith(BundleConfig.PanelPrefix))
                            {
                                PrefabUtility.CreatePrefab(
                                    string.Format("{0}{1}.prefab", BundleConfig.PrefabPath, child.name),
                                    child.gameObject, ReplacePrefabOptions.ConnectToPrefab);
                                string spriteTargePath = string.Format("{0}{1}", BundleConfig.SpritePath, child.name);
                                if (!Directory.Exists(spriteTargePath))
                                {
                                    Directory.CreateDirectory(spriteTargePath);
                                }
                                AssetDatabase.Refresh();
                            }
                        }
                    }
                }
            }
        }

    }
}
