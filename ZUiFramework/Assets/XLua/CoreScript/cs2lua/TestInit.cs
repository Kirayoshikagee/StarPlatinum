using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class TestInit : MonoBehaviour {

    public string path = "Lua/TestLua.lua";
    LuaEnv _Env = new LuaEnv();
    //private LuaEnv _Env = new LuaEnv();
    // Use this for initialization
    void Awake () {
        Debug.LogError("测试：" +(1L << 32));

        LuaEnv.CustomLoader loader = CustomLoaderMethod;
        _Env.AddLoader(loader);

        _Env.DoString(@"require ('Common/Main')");
        //_Env.DoString(@"TestLua = require ('TestLua')");

        //_Env.DoString(@"TestLua:SayHello()");

        //Action<GameObject, Transform> func = _Env.Global.GetInPath<Action<GameObject, Transform>>("TestLua.Start");
        //func(gameObject, transform);
        //func = null;
        
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void OnDestroy()
    {
        if (_Env != null)
        {
            _Env.Dispose();
            _Env = null;
        }
    }

    private byte[] CustomLoaderMethod(ref string fileName) {
        fileName = string.Format("{0}/{1}.lua", Application.dataPath.Replace("Assets", "Lua"), fileName);
        if (File.Exists(fileName)) {
            return File.ReadAllBytes(fileName);
        }
        return null;
    }
}
