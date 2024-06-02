using System.Collections.Generic;
using UnityEngine;

public static partial class FloodFill
{
    /// <summary>
    /// Very Basic FloodFill DFS using a stack
    /// </summary>
    public static void DFSFloodFill(int idx, int idy, Color32[] buffer, int SizeX, int SizeY,
        float threshold)
    {
        //1. If current node is not Inside return.
        if (!Inside(buffer, SizeX, SizeY, idx, idy, threshold)) return;

        //1. Color and Push first node
        Stack<(int x, int y)> cellsToCheck = new();
        buffer[idx + idy * SizeX] = ColorToFill;
        cellsToCheck.Push((idx, idy));

        while (cellsToCheck.Count > 0)
        {
            //2. Pop and find neighbours
            (int x, int y) currentCell = cellsToCheck.Pop();
            (int, int)[] possibleNeighbours =
            {
                (currentCell.x-1,currentCell.y),
                (currentCell.x+1,currentCell.y),
                (currentCell.x,currentCell.y-1),
                (currentCell.x,currentCell.y+1),
            };
            foreach ((int x, int y) in possibleNeighbours)
            {
                if (Inside(buffer, SizeX, SizeY, x, y, threshold))
                {
                    //4. color and Push neighbour
                    buffer[x + y * SizeX] = ColorToFill;
                    cellsToCheck.Push((x, y));
                }
            }

            //4. Continue loop until no more in queue - cellsToCheck
        }
    }
}
