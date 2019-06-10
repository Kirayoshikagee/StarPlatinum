using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TestLoadAssetBundle : MonoBehaviour {

    GameObject loadingPrefab;

    void Awake()
    {
        StartCoroutine(Load());
    }

    // Use this for initialization
    IEnumerator Load()
    {
        //assets$prefab$panelloading.unity3d
        string streamingAssetPath = Path.Combine("Assets/StreamingAssets/Windows", "assets$prefab$panelloading.unity3d");
        streamingAssetPath = streamingAssetPath.Replace("\\", "/");
        AssetBundle bundleLoadRequest = AssetBundle.LoadFromFile(streamingAssetPath);
        yield return bundleLoadRequest;

        Debug.LogError("streamingAsset:"+streamingAssetPath);

        if (bundleLoadRequest == null) {
            Debug.LogError("Failed to load assetBundel");
            yield break;
        }

        string[] allAssets = bundleLoadRequest.GetAllAssetNames();

        for (int i = 0; i < allAssets.Length; i++) {
            Debug.LogError("Assets:" + allAssets[i]);
            GameObject asset = bundleLoadRequest.LoadAsset<GameObject>(allAssets[i]);

            loadingPrefab = GameObject.Instantiate(asset) as GameObject;
            loadingPrefab.transform.SetParent(transform);
        }


        if (bundleLoadRequest != null) {
            bundleLoadRequest.Unload(false);
            bundleLoadRequest = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        if (loadingPrefab != null) {
            GameObject.Destroy(loadingPrefab);
            loadingPrefab = null;
        }
    }
}
