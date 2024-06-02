# Flood Fill / Seed Fill Algorithm Implementation in Unity
Created using Psudo Code Reference: [From Wikipedia](https://en.wikipedia.org/wiki/Flood_fill)

## Flood Fill Algo
- Recursive Flood Fill (can cause stack overflow) 
- DFS FloodFill (using stack)
- BFS FloodFill (using queue)
- Span FloodFill
- Span and Fill FloodFill (Span FloodFill Optimized) - [^1]
- Uses 1D array Buffer
- Uses a ColorDifference Threshold

## Example Usage
- Call SetTargetColor() first, Set the choosen color to replace "fill" to another color, and set the color comparision method call
- Comparison is in float [0-1]
- Call your choosen algorithm
  
- ## TODO
- Implement New Algo?


[^1]: Heckbert, Paul S (1990). "IV.10: A Seed Fill Algorithm". In Glassner, Andrew S (ed.). Graphics Gems. Academic Press. pp. 275–277. ISBN 0122861663
