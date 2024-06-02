using UnityEngine;
using UnityEngine.UI;

public enum FloodfillAlgo
{
    RecursiveFloodFill,
    DFSFloodFill,
    BFSFloodFill,
    SpanFloodFill,
    SpanAndFillFloodFill,
}

public class FloodFill_Example : MonoBehaviour
{
    [SerializeField] Image FloodFillImage;
    [SerializeField] Color ColorToFill;

    /// <summary>
    /// Threshold value to check if colors are different or not
    /// 0 - not different
    /// 1 - very different
    /// Compare threshold cannot be 0, otherwise algos will add visited cells/pixels to itself
    /// </summary>
    [SerializeField, Range(0f, 1f)] float DifferenceThreshold = 0f;
    [SerializeField] FloodfillAlgo FloodfillType;

    RectTransform RT;

    void Awake()
    {
        RT = FloodFillImage.GetComponent<RectTransform>();
    }

    void Update()
    {
        var mousePos = Input.mousePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(RT, mousePos, null, out Vector2 localPos);
        Vector2 imageSize = RT.sizeDelta;
        Vector2 normalizedimagePos = new Vector2((localPos.x + imageSize.x * 0.5f) / imageSize.x, (localPos.y + imageSize.y * 0.5f) / imageSize.y);
        bool isMouseWithin = normalizedimagePos.x >= 0 && normalizedimagePos.y >= 0;

        if (isMouseWithin && Input.GetMouseButtonDown(0))
        {
            var texture = FloodFillImage.sprite.texture;
            var buffer = texture.GetPixels32();
            int idx = (int)(normalizedimagePos.x * texture.width);
            int idy = (int)(normalizedimagePos.y * texture.height);

            Fill(buffer, idx, idy, texture.width, texture.height);

            UpdateImageTexture(FloodFillImage, buffer);
        }
    }

    void Fill(Color32[] buffer, int idx, int idy, int SizeX, int SizeY)
    {
        var index = idx + idy * SizeX;
        var targetColor = buffer[index];

        FloodFill.SetTargetColor(targetColor, ColorToFill, CompareColor);

        switch (FloodfillType)
        {
            case FloodfillAlgo.RecursiveFloodFill:
                FloodFill.RecursiveFloodFill(idx, idy, buffer, SizeX, SizeY, DifferenceThreshold); return;
            case FloodfillAlgo.DFSFloodFill:
                FloodFill.DFSFloodFill(idx, idy, buffer, SizeX, SizeY, DifferenceThreshold); return;
            case FloodfillAlgo.BFSFloodFill:
                FloodFill.BFSFloodFill(idx, idy, buffer, SizeX, SizeY, DifferenceThreshold); return;
            case FloodfillAlgo.SpanFloodFill:
                FloodFill.SpanFloodFill(idx, idy, buffer, SizeX, SizeY, DifferenceThreshold); return;
            case FloodfillAlgo.SpanAndFillFloodFill:
                FloodFill.SpanAndFillFloodFill(idx, idy, buffer, SizeX, SizeY, DifferenceThreshold); return;
        }

        float CompareColor(Color32 color, Color32 other)
        {
            return ColorExtension.Compare_Euclidean(color,other);
        }
    }

    void UpdateImageTexture(Image image, Color32[] pixels)
    {
        if (image == null | image.sprite == null) return;

        Texture2D texture = image.sprite.texture;
        var newTexture = Texture2DExtension.Clone(texture);

        newTexture.SetPixels32(pixels);
        newTexture.Apply();
        image.sprite = Sprite.Create(newTexture, image.sprite.rect, Vector2.zero);
    }
}