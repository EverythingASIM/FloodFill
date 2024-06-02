using System;
using UnityEngine;

public static partial class FloodFill
{
    static Color32 TargetColor;
    static Color32 ColorToFill;
    static Func<Color32, Color32, float> ColorComparisonFunc;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="TargetColor">Set the target color replace</param>
    /// <param name="ColorToFill">Set the color to fill as</param>
    /// <param name="ColorComparisonFunc">Set function to use as color comparion</param>
    public static void SetTargetColor(Color32 TargetColor, Color32 ColorToFill, Func<Color32, Color32, float> ColorComparisonFunc = null)
    {
        FloodFill.TargetColor = TargetColor;
        FloodFill.ColorToFill = ColorToFill;
        FloodFill.ColorComparisonFunc = ColorComparisonFunc;
    }

    /// <summary>
    /// 0 - no difference
    /// 1 - very different
    /// </summary>
    static float ColorDifference(Color color, Color other)
    {
        if (ColorComparisonFunc == null) return 0;

        return ColorComparisonFunc(color, other);
    }

    public static bool Inside(Color32[] buffer, int SizeX, int SizeY, int x, int y, float threshold)
    {
        bool outOfBounds = x < 0 || x > SizeX - 1 || y < 0 || y > SizeY - 1;
        if (outOfBounds) return false;

        var index = x + y * SizeX;
        Color32 currentColor = buffer[index];
        bool isVisited = ColorExtension.IsEqualTo(currentColor, ColorToFill);
        bool isColorSimilar = ColorDifference(currentColor, TargetColor) <= threshold;

        return !isVisited && isColorSimilar;
    }
}
