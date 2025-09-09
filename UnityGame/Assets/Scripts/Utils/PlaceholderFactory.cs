using UnityEngine;
using TMPro;

public static class PlaceholderFactory
{
    public static Sprite CreateSolidSprite(Color color, int width = 64, int height = 64)
    {
        Texture2D tex = new Texture2D(width, height);
        var pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++) pixels[i] = color;
        tex.SetPixels(pixels);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
    }

    public static Material CreateSolidMaterial(Color color)
    {
        var mat = new Material(Shader.Find("Sprites/Default"));
        mat.color = color;
        return mat;
    }

    public static TMP_FontAsset PlaceholderFont()
    {
        return Resources.GetBuiltinResource<TMP_FontAsset>("Arial SDF.asset");
    }
}
