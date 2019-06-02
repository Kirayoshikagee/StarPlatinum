using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpriteImportSetting : AssetPostprocessor
{

    void OnPreprocessTexture()
    {
        TextureImporter importer = (TextureImporter) assetImporter;
        if (BundleConfig.IsSpriteImportPaths(importer.assetPath))
        {
            importer.textureType = TextureImporterType.Sprite;
            importer.mipmapEnabled = false;
            importer.spritePackingTag = BundleConfig.GetDefaultSpritePackingTag(importer.assetPath);
        }
    }


}
