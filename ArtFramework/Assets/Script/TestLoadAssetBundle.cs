using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TestLoadAssetBundle : MonoBehaviour {

    void Awake()
    {
        StartCoroutine(Load());
    }

    // Use this for initialization
    IEnumerator Load()
    {
        //assets$prefab$panelloading.unity3d
        string streamingAssetPath = Path.Combine("Assets/StreamingAssets/", "Windows/assets$prefab$panelloading.unity3d");
        AssetBundle bundleLoadRequest = AssetBundle.LoadFromFile(streamingAssetPath);
        yield return bundleLoadRequest;

        Debug.LogError("streamingAsset:"+streamingAssetPath);
        var testLoadBundle = bundleLoadRequest.mainAsset;
        
        if (testLoadBundle == null)
        {
            Debug.LogError("Fail to load assetbundle");
            yield break;
        }
        Debug.LogError("mainAsset:" + testLoadBundle.name);


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
