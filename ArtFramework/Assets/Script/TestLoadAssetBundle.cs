using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestLoadAssetBundle : MonoBehaviour {

    GameObject loadingPrefab;
    GameObject copyLoadingPrefab;

    void Awake()
    {
        //StartCoroutine(LoadAllAssets());
    }

    /// <summary>
    /// 使用AssetBundleManifest
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadAllAssets() {
        AssetBundle allBundles = AssetBundle.LoadFromFile(Config._AssetBundlePath);
        yield return allBundles;

        if (allBundles == null) {
            Debug.LogError("fail to load Assets");
            yield break;
        }

        AssetBundleManifest manifest = allBundles.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        string[] bundles = manifest.GetAllAssetBundles();
        for (int i = 0; i < bundles.Length; i++) {
            Debug.LogError("bundle：" + bundles[i] + "; dependencies:");
            string[] dependencies = manifest.GetAllDependencies(bundles[i]);
            for (int j = 0; j < dependencies.Length; j++) {
                Debug.LogError("denpendency:" + dependencies[j]);
            }
            Debug.LogError("move to next bundle!!!");
        }

        if (allBundles != null) {
            allBundles.Unload(true);
            allBundles = null;
        }
    }

    // Use this for initialization
    IEnumerator LoadStandardAsset()
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

        GameObject template = null;

        for (int i = 0; i < allAssets.Length; i++) {
            Debug.LogError("Assets:" + allAssets[i]);
            template = bundleLoadRequest.LoadAsset<GameObject>(allAssets[i]);
            loadingPrefab = GameObject.Instantiate(template) as GameObject;
        
            loadingPrefab.transform.SetParent(transform);
            loadingPrefab.transform.localPosition = Vector3.zero;
            RectTransform loadingRect = loadingPrefab.GetComponent<RectTransform>();
            loadingRect.anchorMax = Vector2.zero;
            loadingRect.anchorMin = Vector2.zero;
            loadingRect.sizeDelta = new Vector2(100, 200);
        }


        if (bundleLoadRequest != null) {
            bundleLoadRequest.Unload(false);
            bundleLoadRequest = null;
        }

        if (template != null) {
            Debug.LogError("template is not null");
            loadingPrefab = GameObject.Instantiate(template) as GameObject;
            loadingPrefab.name = "copyLoading";

            loadingPrefab.transform.SetParent(transform);
            loadingPrefab.transform.localPosition = Vector3.zero;
            RectTransform loadingRect = loadingPrefab.GetComponent<RectTransform>();
            loadingRect.anchorMax = Vector2.zero;
            loadingRect.anchorMin = Vector2.zero;
            loadingRect.sizeDelta = new Vector2(200, 200);
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
