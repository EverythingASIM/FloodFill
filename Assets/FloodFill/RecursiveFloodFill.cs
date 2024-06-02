using UnityEngine;

public static partial class FloodFill
{
    ///// <summary>
    ///// Very Basic RecursiveFloodFill
    ///// Note method will cause stack overflow due to lots of recursive calls
    ///// </summary>
    public static void RecursiveFloodFill(int idx, int idy, Color32[] buffer, int SizeX, int SizeY,
         float threshold)
    {
        //1. If current node is not Inside return.
        if (!Inside(buffer, SizeX, SizeY, idx, idy, threshold)) return;

        //2. Set the node
        buffer[idx + idy * SizeX] = ColorToFill;

        //3. Perform Flood-fill on the neighbours
        RecursiveFloodFill(idx + 1, idy, buffer, SizeX, SizeY, threshold);
        RecursiveFloodFill(idx - 1, idy, buffer, SizeX, SizeY, threshold);
        RecursiveFloodFill(idx, idy + 1, buffer, SizeX, SizeY, threshold);
        RecursiveFloodFill(idx, idy - 1, buffer, SizeX, SizeY, threshold);
    }
}