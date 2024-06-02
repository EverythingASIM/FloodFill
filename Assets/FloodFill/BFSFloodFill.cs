using System.Collections.Generic;
using UnityEngine;

public static partial class FloodFill
{
    ///// <summary>
    ///// Very Basic Recursive FloodFill BFS using a queue
    ///// </summary>
    public static void BFSFloodFill(int idx, int idy, Color32[] buffer, int SizeX, int SizeY,
        float threshold)
    {
        //1. If current node is not Inside return.
        if (!Inside(buffer, SizeX, SizeY, idx, idy, threshold)) return;

        //1. Color and Queue first node
        Queue<(int x, int y)> cellsToCheck = new();
        buffer[idx + idy * SizeX] = ColorToFill;
        cellsToCheck.Enqueue((idx, idy));

        while (cellsToCheck.Count > 0)
        {
            //2. Dequeue and find neighbours
            (int x, int y) currentCell = cellsToCheck.Dequeue();
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
                    //4. color and queue neighbour
                    buffer[x + y * SizeX] = ColorToFill;
                    cellsToCheck.Enqueue((x, y));
                }
            }

            //4. Continue loop until no more in queue - cellsToCheck
        }
    }

    

    ///// <summary>
    ///// Simple Span Filling
    ///// </summary>
    //IEnumerator SpanFloodFill(int x, int y, Color32[] buffer, int SizeX, int SizeY,
    //    float threshold, Color targetColor, Color32 colorToFill)
    //{
    //    isLogicPaused = true;

    //    if (!Inside(buffer, x, y, SizeX, SizeY)) yield break;

    //    //1. Add first node to stack
    //    Stack<(int x, int y)> cellsToCheck = new();
    //    cellsToCheck.Push((x, y));
    //    while (cellsToCheck.Count > 0)
    //    {
    //        //2. Pop from stack
    //        (x, y) = cellsToCheck.Pop();

    //        var Lx = x; //Span Min

    //        //3. Start from left of cell, color then walk left repeat until hit a wall
    //        while (Inside(buffer, Lx - 1, y, SizeX, SizeY))
    //        {
    //            buffer[Lx - 1 + y * SizeX] = colorToFill;
    //            UpdateImageTexture(FloodFillImage, buffer);
    //            isLogicPaused = true; yield return new WaitUntil(UnpauseLogic);

    //            Lx = Lx - 1;
    //        }
    //        while (Inside(buffer, x, y, SizeX, SizeY))
    //        {
    //            buffer[x + y * SizeX] = colorToFill;
    //            UpdateImageTexture(FloodFillImage, buffer);
    //            isLogicPaused = true; yield return new WaitUntil(UnpauseLogic);

    //            x = x + 1;
    //        }

    //        //5. Check Up && Down of "Span" to add to stack
    //        scan(Lx, x - 1, y + 1, cellsToCheck);
    //        scan(Lx, x - 1, y - 1, cellsToCheck);

    //        //6. Continue loop until no more in stack - cellsToCheck
    //    }

    //    bool Inside(Color32[] buffer, int x, int y, int SizeX, int SizeY)
    //    {
    //        bool outOfBounds = x < 0 || x > SizeX - 1 || y < 0 || y > SizeY - 1;
    //        if (outOfBounds) return false;

    //        var index = x + y * SizeX;
    //        Color32 currentColor = buffer[index];
    //        bool isVisited = ColorExtension.IsEqualTo(currentColor, colorToFill);
    //        bool isColorSimilar = CompareColor(currentColor, targetColor) <= threshold;

    //        return !isVisited && isColorSimilar;
    //    }

    //    void scan(int spanMin, int spanMax, int y, Stack<(int x, int y)> s)
    //    {
    //        bool span_added = false;
    //        for (int x = spanMin; x <= spanMax; x++)
    //        {
    //            if (!Inside(buffer, x, y, SizeX, SizeY))
    //            {
    //                span_added = false;
    //            }
    //            else if (!span_added)
    //            {
    //                cellsToCheck.Push((x, y));
    //                span_added = true;
    //            }
    //        }
    //    }
    //}

    ///// <summary>
    ///// Heckbert, Paul S (1990). "IV.10: A Seed Fill Algorithm". In Glassner, Andrew S (ed.). Graphics Gems. Academic Press. pp. 275–277.
    ///// </summary>
    //IEnumerator SpanAndFillFloodFill(int idx, int idy, Color32[] buffer, int SizeX, int SizeY,
    //    float threshold, Color targetColor, Color32 colorToFill)
    //{
    //    isLogicPaused = true;

    //    if (!Inside(buffer, idx, idy, SizeX, SizeY)) yield break;

    //    //1. Add first node
    //    Stack<(int x1, int x2, int y, int dy)> cellsToCheck = new();
    //    cellsToCheck.Push((idx, idx, idy, 1));
    //    cellsToCheck.Push((idx, idx, idy, -1));
    //    while (cellsToCheck.Count > 0)
    //    {
    //        //2. Pop from stack
    //        (int x1, int x2, int y, int dy) = cellsToCheck.Pop();

    //        var x = x1;

    //        if (Inside(buffer, x, y, SizeX, SizeY))
    //        {
    //            while (Inside(buffer, x - 1, y, SizeX, SizeY))
    //            {
    //                buffer[x - 1 + y * SizeX] = colorToFill;
    //                UpdateImageTexture(FloodFillImage, buffer);
    //                isLogicPaused = true; yield return new WaitUntil(UnpauseLogic);

    //                x = x - 1;
    //            }
    //            if (x < x1)
    //                cellsToCheck.Push((x, x1 - 1, y - dy, -dy));
    //        }
    //        while (x1 <= x2)
    //        {
    //            while (Inside(buffer, x1, y, SizeX, SizeY))
    //            {
    //                buffer[x1 + y * SizeX] = colorToFill;
    //                UpdateImageTexture(FloodFillImage, buffer);
    //                isLogicPaused = true; yield return new WaitUntil(UnpauseLogic);

    //                x1 = x1 + 1;
    //            }
    //            if (x1 > x)
    //                cellsToCheck.Push((x, x1 - 1, y + dy, dy));
    //            if (x1 - 1 > x2)
    //                cellsToCheck.Push((x2 + 1, x1 - 1, y - dy, -dy));
    //            x1 = x1 + 1;
    //            while (x1 < x2 && !Inside(buffer, x1, y, SizeX, SizeY))
    //            {
    //                x1 = x1 + 1;
    //            }
    //            x = x1;
    //        }
    //    }

    //    bool Inside(Color32[] buffer, int x, int y, int SizeX, int SizeY)
    //    {
    //        bool outOfBounds = x < 0 || x > SizeX - 1 || y < 0 || y > SizeY - 1;
    //        if (outOfBounds) return false;

    //        var index = x + y * SizeX;
    //        Color32 currentColor = buffer[index];
    //        bool isVisited = ColorExtension.IsEqualTo(currentColor, colorToFill);
    //        bool isColorSimilar = CompareColor(currentColor, targetColor) <= threshold;

    //        return !isVisited && isColorSimilar;
    //    }
    //}
}
