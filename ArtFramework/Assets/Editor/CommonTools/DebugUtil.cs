using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUtil : MonoBehaviour {


    public static void LogError(string msg)
    {
        Debug.LogError(msg);
    }

    public static void Log(string msg)
    {
        Debug.Log(msg);
    }

    public static void LogWarning(string msg)
    {
        Debug.LogWarning(msg);
    }
}
