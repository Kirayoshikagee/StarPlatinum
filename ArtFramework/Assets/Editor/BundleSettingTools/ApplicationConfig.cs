using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public static class ApplicationConfig  {

    public static string GetPlatform(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.Android:
                return "Android";
            case BuildTarget.iOS:
                return "IOS";
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return "Windows";
            default:
                return null;
        }
    }

}
