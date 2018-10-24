using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHelper : MonoBehaviour {

    public static int w = 15;
    public static int h = 15;
    public static Cell[,] cells = new Cell[w, h];

    void Start()
    {

    }

    // For GameOver
    public static void UncoverAllTheMines()
    {
        foreach(Cell c in cells)
        {
            if (c.hasMine)
            {
                c.LoadTexture(0);
            }
        }
    }

    public static int CountAdjacentMines(int x, int y)
    {
        int count = 0;

        if (HasMineAt(x - 1, y - 1)) count++;
        if (HasMineAt(x - 1, y    )) count++;
        if (HasMineAt(x - 1, y + 1)) count++;
        if (HasMineAt(x    , y - 1)) count++;
        if (HasMineAt(x    , y + 1)) count++;
        if (HasMineAt(x + 1, y - 1)) count++;
        if (HasMineAt(x + 1, y    )) count++;
        if (HasMineAt(x + 1, y + 1)) count++;

        return count;
    }

    public static int VisualCount(int x, int y)
    {
        int count = 0;

        if (HasMineAt(x - 1, y + 1))count++;if(HasMineAt(x, y + 1))count++;if(HasMineAt(x + 1, y + 1))count++;
        if (HasMineAt(x - 1, y    ))count++; /*         [My Cell]       */ if(HasMineAt(x + 1, y    ))count++;
        if (HasMineAt(x - 1, y - 1))count++;if(HasMineAt(x, y - 1))count++;if(HasMineAt(x + 1, y - 1))count++;

        return count;
    }

    public static bool HasMineAt(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < w && y < h)
        {
            // Estoy dentro de la parrilla
            Cell cell = cells[x, y];
            return cell.hasMine;

        } else
        {
            // Estoy fuera de la parrilla
            return false;
        }
    }

    public static void FloodFillUncover(int x, int y, bool [,] visited)
    {
        if (IsCellInBoard(x, y))
        {
            if (visited[x, y])
            {
                return;
            }

            int adjacentMines = CountAdjacentMines(x, y);

            cells[x, y].LoadTexture(adjacentMines);

            if (adjacentMines > 0)
            {
                return;
            }

            visited[x, y] = true;

            PropagateFloodFillUncover8Directions(x, y, visited);
        }
    }

    private static bool IsCellInBoard(int x, int y)
    {
        return x >= 0 && y >= 0 && x < w && y < h;
    }

    private static void PropagateFloodFillUncover4Directions(int x, int y, bool [,] visited)
    {
        /*          [Ignored]               */ FloodFillUncover(x, y + 1, visited); /*          [Ignored]             */
        FloodFillUncover(x - 1, y, visited); /*          [My Cell]                 */ FloodFillUncover(x + 1, y, visited);
        /*          [Ignored]               */ FloodFillUncover(x, y - 1, visited); /*          [Ignored]             */
    }

    private static void PropagateFloodFillUncover8Directions(int x, int y, bool[,] visited)
    {
        FloodFillUncover(x - 1, y + 1, visited); FloodFillUncover(x, y + 1, visited); FloodFillUncover(x + 1, y + 1, visited);
        FloodFillUncover(x - 1, y    , visited); /*           [My Cell]           */  FloodFillUncover(x + 1, y    , visited);
        FloodFillUncover(x - 1, y - 1, visited); FloodFillUncover(x, y - 1, visited); FloodFillUncover(x + 1, y - 1, visited);
    }
}
