using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum Direction
{
    Down = 1,
    Left = 2,
    Right = 4
}

public class Cvor
{
    public int x, y;

    public Cvor(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class Path : List<Direction>
{

    public Direction GetNewDirection(Direction allowed, System.Random rnd)
    {
        Direction newd;
        int maxd = Enum.GetValues(typeof(Direction)).Length;
        int[] vals = (int[])Enum.GetValues(typeof(Direction));
        do
        {
            var t = rnd.Next(0, maxd);
            newd = (Direction)vals[t];
        }
        while ((newd & allowed) == 0);
        return newd;
    }


    public List<Cvor> GenerateRandomPath(int startx, int starty, int endx, int endy, double prob, int width, int height)
    {
        Path newpath = new Path();
        System.Random rnd = new System.Random();

        List<Cvor> put = new List<Cvor>();
        bool canTurn = true;
        int inrow = 0;

        int curx = startx; int cury = starty; Direction curd = Direction.Right;
        Direction newd = curd;

        while (!(curx == endx && cury == endy))
        {
            if (inrow >= 2)
            {
                inrow = 0;
                canTurn = true;
            }


            if (canTurn && rnd.NextDouble() <= prob) // let's generate a turn
            {
                canTurn = false;

                do
                {
                    if (curx == endx) newd = GetNewDirection(Direction.Left | Direction.Down, rnd);
                    else if (cury == endy) newd = Direction.Right;
                    else if (curx <= 0) newd = GetNewDirection(Direction.Right | Direction.Down, rnd);
                    else newd = GetNewDirection(Direction.Right | Direction.Down | Direction.Left, rnd);

                }
                while ((newd | curd) == (Direction.Left | Direction.Right)); // excluding going back
                
                newpath.Add(newd);
                put.Add(new Cvor(curx, cury));
                curd = newd;
                switch (newd)
                {
                    case Direction.Left:
                        curx--;
                        break;
                    case Direction.Right:
                        curx++;
                        break;
                    case Direction.Down:
                        cury++;
                        break;
                }
            }
                
            

            inrow++;

        }

        return put;
    }

    public int[,] GetMap(int height, int width, int startx, int starty, int endx, int endy)
    {
        List<Cvor> put = GenerateRandomPath(startx, starty, endx, endy, 0.25, width, height);
        int[,] map = new int[height, width];

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (i == startx && j == starty)
                    map[i, j] = 2;
                else if (i == endx && j == endy)
                    map[i, j] = 3;
                else
                    map[i, j] = 0;
            }
        }

        foreach (var p in put)
        {
            map[p.x, p.y] = 1;
        }

        string str = "";
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                str += map[i, j];
            }
            str += "\n";
        }

        Debug.Log("MAP: " + str);
        return map;
    }

}
