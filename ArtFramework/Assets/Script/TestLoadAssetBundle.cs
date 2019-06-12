using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TestLoadAssetBundle : MonoBehaviour {

    GameObject loadingPrefab;
    GameObject copyLoadingPrefab;
    public GameObject copyGameObject;
    public Material copyMaterial;

    void Awake()
    {
        //StartCoroutine(LoadSpriteAssets());
        //StartCoroutine(LoadStandardAsset());
        LoadMaterialDemo();
    }

    public void LoadMaterialDemo() {
        Image img = transform.GetComponent<Image>();
        img.material = copyMaterial;
    }

    /// <summary>
    /// 加载得到的打图集是texture2D，小图是Sprite
    /// 不加载图集batches是3，加载图集batches是5
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadSpriteAssets() {
        //assets$texture$cn$panellogin.unity3d
        //onlyoneimage.unity3d
        string streamingAssetPath = Path.Combine("Assets/StreamingAssets/Windows", "assets$texture$cn$panellogin.unity3d");
        streamingAssetPath = streamingAssetPath.Replace("\\", "/");
        AssetBundle bundleLoadRequest = AssetBundle.LoadFromFile(streamingAssetPath);
        yield return bundleLoadRequest;

        Debug.LogError("streamingAsset:" + streamingAssetPath);

        if (bundleLoadRequest == null)
        {
            Debug.LogError("Failed to load assetBundel");
            yield break;
        }

        AssetBundleRequest request = bundleLoadRequest.LoadAllAssetsAsync<Object>();
        yield return request;
        
        
        if (request.isDone) {
            Object[] objs = request.allAssets;
            for (int i = 0; i < objs.Length; i++) {
                if (objs[i] is Sprite) {
                    GameObject copy = GameObject.Instantiate(copyGameObject);
                    copy.transform.SetParent(transform);
                    copy.transform.localPosition = Vector3.zero;
                    Image copyImage = copy.GetComponent<Image>();
                    if (copyImage == null) {
                        copyImage = copy.AddComponent<Image>();
                    }
                    
                    copyImage.sprite = objs[i] as Sprite;
                    copyImage.SetNativeSize();

                    //Sprite sprite = objs[i] as Sprite;
                    Debug.LogError("Sprite@d:" + objs[i].name+"; Type:"+ objs[i].GetType().Name);
                }
                

                //Debug.LogError("Texture@d:" + objs[i].name+":Type:"+objs[i].GetType().Name);
            }
            bundleLoadRequest.Unload(false);
        }
        

        /*
        string[] allAssets = bundleLoadRequest.GetAllAssetNames();

        GameObject template = null;

        for (int i = 0; i < allAssets.Length; i++)
        {
            
            Texture2D sprite = bundleLoadRequest.LoadAsset<Texture2D>(allAssets[i]);
            this.GetComponent<Image>().sprite = Sprite.Create(sprite, new Rect(0, 0, sprite.width, sprite.height), Vector2.zero);
            Debug.LogError(allAssets[i] +":Assets:" + sprite.name +" width:"+sprite.width + " height:"+sprite.height);
            
        }
        */

        if (bundleLoadRequest != null)
        {
            bundleLoadRequest.Unload(false);
            bundleLoadRequest = null;
        }

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
    /// <summary>
    /// 直接把图附在prefab上会增加dc,最好是把图都扒下来达成一张图集
    /// </summary>
    /// <returns></returns>
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
        /*
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
        */
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
