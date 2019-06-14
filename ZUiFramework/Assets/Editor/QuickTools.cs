using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuickTools {

    [MenuItem("Assets/Tools/Copy path")]
    public static string CopyAssetPath() {
        Object selectedGO = Selection.activeObject;
        if (selectedGO == null) {
            Debug.LogError("请选择一个Object");
            return "";
        }

        string path = GUIUtility.systemCopyBuffer = AssetDatabase.GetAssetPath(selectedGO);
        Debug.LogError("拷贝成功："+path);
        return path;
    }
}
