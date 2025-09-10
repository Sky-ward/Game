using UnityEditor;
using UnityEngine;
using System.IO;

public class SpriteImportPostprocessor : AssetPostprocessor
{
    private const string Prefix = "spr_";

    void OnPreprocessTexture()
    {
        var importer = (TextureImporter)assetImporter;
        string fileName = Path.GetFileNameWithoutExtension(assetPath);

        if (!fileName.StartsWith(Prefix))
        {
            Debug.LogError($"Sprite '{assetPath}' must start with '{Prefix}'.");
        }

        importer.textureType = TextureImporterType.Sprite;
        importer.textureCompression = TextureImporterCompression.Compressed;
        importer.mipmapEnabled = false;
    }
}
