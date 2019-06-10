using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Config {
    public static string _Android = "android";
    public static string _IOS = "ios";
    public static string _Windows = "windows";

#if UNITY_ANDROID
    public static string _AssetBundlePath = Path.Combine("Assets/StreamingAssets/Android", "Android").Replace("\\", "/");
#elif UNITY_IOS
    public static string _AssetBundlePath = Path.Combine("Assets/StreamingAssets/IOS", "IOS").Replace("\\", "/");
#else
    public static string _AssetBundlePath = Path.Combine("Assets/StreamingAssets/Windows", "Windows").Replace("\\", "/");
#endif

    public static string JsonFileName = "resources.json";
#if UNITY_ANDROID
    public static string _JsonFilePath = Path.Combine("Assets/StreamingAssets/Android", JsonFileName).Replace("\\", "/");
#elif UNITY_IOS
    public static string _JsonFilePath = Path.Combine("Assets/StreamingAssets/IOS", JsonFileName).Replace("\\", "/");
#else
    public static string _JsonFilePath = Path.Combine("Assets/StreamingAssets/Windows", JsonFileName).Replace("\\", "/");
#endif

}
