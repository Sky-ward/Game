using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using System.IO;

public class SpriteImportSmokeTests
{
    private const string TempPath = "Assets/Art/Placeholders/spr_temp.png";

    [Test]
    public void ImportedSpriteHasExpectedSettings()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(TempPath));

        var tex = new Texture2D(2, 2);
        tex.SetPixels(new[] { Color.white, Color.white, Color.white, Color.white });
        tex.Apply();
        File.WriteAllBytes(TempPath, tex.EncodeToPNG());
        Object.DestroyImmediate(tex);

        AssetDatabase.ImportAsset(TempPath, ImportAssetOptions.ForceUpdate);
        var importer = (TextureImporter)AssetImporter.GetAtPath(TempPath);

        Assert.AreEqual(TextureImporterType.Sprite, importer.textureType);
        Assert.AreEqual(TextureImporterCompression.Compressed, importer.textureCompression);
        Assert.IsFalse(importer.mipmapEnabled);

        AssetDatabase.DeleteAsset(TempPath);
    }
}
