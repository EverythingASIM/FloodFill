using System.Collections.Generic;
using UnityEngine;

public static partial class FloodFill
{
    /// <summary>
    /// Simple Span Filling
    /// </summary>
    public static void SpanFloodFill(int x, int y, Color32[] buffer, int SizeX, int SizeY,
        float threshold)
    {
        if (!Inside(buffer, SizeX, SizeY, x, y, threshold)) return;

        //1. Add first node to stack
        Stack<(int x, int y)> cellsToCheck = new();
        cellsToCheck.Push((x, y));
        while (cellsToCheck.Count > 0)
        {
            //2. Pop from stack
            (x, y) = cellsToCheck.Pop();

            var Lx = x; //Span Min

            //3. Start from left of cell, color then walk left repeat until hit a wall
            while (Inside(buffer, SizeX, SizeY, Lx - 1, y, threshold))
            {
                buffer[Lx - 1 + y * SizeX] = ColorToFill;

                Lx = Lx - 1;
            }
            while (Inside(buffer, SizeX, SizeY, x, y, threshold))
            {
                buffer[x + y * SizeX] = ColorToFill;

                x = x + 1;
            }

            //5. Check Up && Down of "Span" to add to stack
            scan(Lx, x - 1, y + 1, cellsToCheck);
            scan(Lx, x - 1, y - 1, cellsToCheck);

            //6. Continue loop until no more in stack - cellsToCheck
        }

        void scan(int spanMin, int spanMax, int y, Stack<(int x, int y)> s)
        {
            bool span_added = false;
            for (int x = spanMin; x <= spanMax; x++)
            {
                if (!Inside(buffer, SizeX, SizeY, x, y, threshold))
                {
                    span_added = false;
                }
                else if (!span_added)
                {
                    s.Push((x, y));
                    span_added = true;
                }
            }
        }
    }

    /// <summary>
    /// Heckbert, Paul S (1990). "IV.10: A Seed Fill Algorithm". In Glassner, Andrew S (ed.). Graphics Gems. Academic Press. pp. 275–277.
    /// </summary>
    public static void SpanAndFillFloodFill(int idx, int idy, Color32[] buffer, int SizeX, int SizeY,
        float threshold)
    {
        if (!Inside(buffer, SizeX, SizeY, idx, idy, threshold)) return;

        //1. Add first node
        Stack<(int x1, int x2, int y, int dy)> cellsToCheck = new();
        cellsToCheck.Push((idx, idx, idy, 1));
        cellsToCheck.Push((idx, idx, idy, -1));
        while (cellsToCheck.Count > 0)
        {
            //2. Pop from stack
            (int x1, int x2, int y, int dy) = cellsToCheck.Pop();

            var x = x1;

            if (Inside(buffer, SizeX, SizeY, x, y, threshold))
            {
                while (Inside(buffer, SizeX, SizeY, x - 1, y, threshold))
                {
                    buffer[x - 1 + y * SizeX] = ColorToFill;

                    x = x - 1;
                }
                if (x < x1)
                    cellsToCheck.Push((x, x1 - 1, y - dy, -dy));
            }
            while (x1 <= x2)
            {
                while (Inside(buffer, SizeX, SizeY, x1, y, threshold))
                {
                    buffer[x1 + y * SizeX] = ColorToFill;

                    x1 = x1 + 1;
                }
                if (x1 > x)
                    cellsToCheck.Push((x, x1 - 1, y + dy, dy));
                if (x1 - 1 > x2)
                    cellsToCheck.Push((x2 + 1, x1 - 1, y - dy, -dy));
                x1 = x1 + 1;
                while (x1 < x2 && !Inside(buffer, SizeX, SizeY, x1, y, threshold))
                {
                    x1 = x1 + 1;
                }
                x = x1;
            }
        }
    }
}
