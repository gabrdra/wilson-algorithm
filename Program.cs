using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WilsonAlgorithm;
class Program
{
    static void Main()
    {
        MazeCreation mazeCreation = new MazeCreation();
        Vertex[,] maze = mazeCreation.CreateMaze(1, 4, 4);
        PrintMaze(maze, 4, 4);
    }
    static void PrintMaze(Vertex[,] maze, int width, int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Console.WriteLine(maze[i,j]);
            }
        }
    }
}
