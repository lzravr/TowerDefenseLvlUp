using UnityEngine;
using System.Collections.Generic;


public class GenerateEnvironment : MonoBehaviour {

    public int height = 10;
    public int width = 10;
    public GameObject ground;
    public GameObject node;
    public GameObject start;
    public GameObject end;
    public float spacing = 6f;
    public int startx, starty, endx, endy;

    private Path path;
    private float X = 0;
    private float Y = 0;
    private float Z = 0;

    private void Start()
    {
        path = new Path();
        int[,] map = path.GetMap(10, 10, startx, starty, endx, endy);

        
   
        for (int i = 0; i < width; i++)
        {
            X = 0;

            for (int j = 0; j < height; j++)
            {
                switch(map[i,j])
                {
                    case 0:
                        {
                            Instantiate(node, new Vector3(X, Y, Z), Quaternion.identity);
                            break;
                        }
                    case 1:
                        {
                            Instantiate(ground, new Vector3(X, Y, Z), Quaternion.identity);
                            break;
                        }
                    case 2:
                        {
                            Instantiate(start, new Vector3(startx, Y, starty), Quaternion.identity);
                            break;
                        }
                    case 3:
                        {
                            Instantiate(end, new Vector3(endx, Y, endy), Quaternion.identity);
                            break;
                        }
                }

                X += 6;
            }

            Z += 6;
        }
    }
}
