using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WilsonAlgorithm;
class Program
{
    static void Main()
    {
        int randomSeed = -1;
        int width = -1;
        int height = -1;
        bool inputAccepted = false;
        Console.WriteLine("Test case chooser: ");
        Console.WriteLine("if the input doesn't correspond to any of the cases you'll be prompted with the manual input\n"+
            "1- randomSeed = 1; width = 20; height = 20 \n" +
            "2- randomSeed = 9000; width = 18; height = 20 \n" +
            "3- randomSeed = 578365; width = 10; height = 10 \n" +
            "4- randomSeed = 500; width = 30; height = 30 \n" +
            "5- randomSeed = 6; width = 10; height = 40 \n");
        int caseTest = -1;
        if (int.TryParse(Console.ReadLine(), out caseTest))
        {
            if (caseTest >= 1 && caseTest <= 5)
            {
                inputAccepted = true;
                switch (caseTest)
                {
                    case 1:
                        randomSeed = 1;
                        width = 20;
                        height = 20;
                        break;
                    case 2:
                        randomSeed = 9000;
                        width = 18;
                        height = 20;
                        break;
                    case 3:
                        randomSeed = 578365;
                        width = 10;
                        height = 10;
                        break;
                    case 4:
                        randomSeed = 500;
                        width = 30;
                        height = 30;
                        break;
                    case 5:
                        randomSeed = 6;
                        width = 10;
                        height = 40;
                        break;
                }
            }
        }
        while (!inputAccepted)
        {
            inputAccepted = true;
            Console.WriteLine("Enter a random seed:");
            if (int.TryParse(Console.ReadLine(), out randomSeed))
            {
                if (randomSeed < 0) {
                    Console.WriteLine("Random seed can't be negative");
                    inputAccepted = false;
                }
            }
            else
            {
                Console.WriteLine("Random seed needs to be a non negative integer");
                inputAccepted = false;
            }
            if (inputAccepted)
            {
                Console.WriteLine("Enter width:");
                if (int.TryParse(Console.ReadLine(), out width))
                {
                    if (width < 1)
                    {
                        Console.WriteLine("Width needs to be greater than 0");
                        inputAccepted = false;
                    }
                }
                else
                {
                    Console.WriteLine("Width needs to be a positive integer");
                    inputAccepted = false;
                }
            }
            if (inputAccepted)
            {
                Console.WriteLine("Enter height:");
                if (int.TryParse(Console.ReadLine(), out height))
                {
                    if (height < 1)
                    {
                        Console.WriteLine("Height needs to be greater than 0");
                        inputAccepted = false;
                    }
                }
                else
                {
                    Console.WriteLine("Height needs to be a positive integer");
                    inputAccepted = false;
                }
            }
        }
        string fileName = "r = "+randomSeed+" w = "+width+" h = "+height+".txt";
        Console.WriteLine("Write to file?(y/n) File name will be: "+fileName);
        string line = Console.ReadLine();
        bool generateFile = line.Equals("y")||line.Equals("Y");

        MazeCreation mazeCreation = new MazeCreation();
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Vertex[,] maze = mazeCreation.CreateMaze(randomSeed, width, height);
        stopwatch.Stop();
        Console.WriteLine("Time taken to create maze: " + stopwatch.ElapsedMilliseconds + " ms");
        PrintMaze(maze, width, height);
        PrintCells(maze, width, height);
        if (generateFile)
        {
            WriteFile(fileName,stopwatch, maze, randomSeed,width, height);
        }
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }
    static void WriteFile(string fileName,Stopwatch stopwatch, Vertex[,] maze, int randomSeed, int width, int height)
    {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter("./Reports/"+fileName,false))
        {
            file.WriteLine("Random seed: " + randomSeed);
            file.WriteLine("Width: " + width);
            file.WriteLine("Height: " + height);
            file.WriteLine("Time taken to create maze: " + stopwatch.ElapsedMilliseconds + " ms");
            string horizontalLine = ""; //Prints +-- or +  
            string verticalLine = ""; //Prints | or   
            for (int i = height - 1; i >= 0; i--)
            {
                verticalLine += "|";
                for (int j = 0; j < width; j++)
                {
                    if (i == height - 1)
                    {
                        horizontalLine += "+--";
                    }
                    else
                    {
                        if (maze[j, i].North)
                        {
                            horizontalLine += "+  ";
                        }
                        else
                        {
                            horizontalLine += "+--";
                        }
                    }
                    if (j == width - 1)
                    {
                        horizontalLine += "+";
                        verticalLine += "  |";
                    }
                    else
                    {
                        if (maze[j, i].East)
                        {
                            verticalLine += "   ";
                        }
                        else
                        {
                            verticalLine += "  |";
                        }
                    }
                }
                file.WriteLine(horizontalLine);
                horizontalLine = "";
                file.WriteLine(verticalLine);
                verticalLine = "";
            }
            string bottomLine = "";
            for (int i = 0; i < width; i++)
            {
                bottomLine += "+--";
            }
            bottomLine += "+";
            file.WriteLine(bottomLine);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    file.WriteLine(maze[i, j].ToString());
                }
            }
        }
    }
    static void PrintCells(Vertex[,] maze, int width, int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Console.WriteLine(maze[i,j]);
            }
        }
    }
    static void PrintMaze(Vertex[,] maze, int width, int height)
    {
        string horizontalLine = ""; //Prints +-- or +  
        string verticalLine = ""; //Prints | or   
        for (int i = height-1; i >= 0; i--)
        {
            verticalLine += "|";
            for (int j = 0; j < width; j++)
            {
                if(i == height - 1)
                {
                    horizontalLine += "+--";
                }
                else
                {
                    if (maze[j, i].North)
                    {
                        horizontalLine += "+  ";
                    }
                    else
                    {
                        horizontalLine += "+--";
                    }
                }
                if (j == width - 1)
                {
                    horizontalLine += "+";
                    verticalLine += "  |";
                }
                else
                {
                    if (maze[j, i].East)
                    {
                        verticalLine += "   ";
                    }
                    else
                    {
                        verticalLine += "  |";
                    }
                }
            }
            Console.WriteLine(horizontalLine);
            horizontalLine = "";
            Console.WriteLine(verticalLine);
            verticalLine = "";
        }
        string bottomLine = "";
        for (int i = 0; i < width; i++)
        {
            bottomLine += "+--";
        }
        bottomLine += "+";
        Console.WriteLine(bottomLine);
    }
}
