using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WilsonAlgorithm
{
    internal class MazeCreation
    {
        public Vertex[,] CreateMaze(int randomSeed, int width, int height)
        {
            System.Random random = new System.Random(randomSeed);
            Vertex[,] maze = new Vertex[width, height];
            HashSet<Vertex> notVisited = new HashSet<Vertex>();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vertex newVertex = new Vertex(i, j, width, height);
                    maze[i, j] = newVertex;
                    notVisited.Add(newVertex);
                }
            }
            Vertex firstVertex = notVisited.ElementAt(random.Next(notVisited.Count));//this enumerates all elements so it's quite inefficient but I don't know any better way to do it
            notVisited.Remove(firstVertex);
            while (notVisited.Count > 0)
            {
                //Console.WriteLine("inside outer while");
                Vertex currentVertex = notVisited.ElementAt(random.Next(notVisited.Count));
                //notVisited.Remove(currentVertex);
                Queue<Vertex> currentPath = new Queue<Vertex>();
                currentPath.Enqueue(currentVertex);
                while (notVisited.Contains(currentVertex))
                {
                    //Console.WriteLine("inside inner while");
                    Vertex.AdjacentReturn adjacentReturn = currentVertex.giveAdjacent(random.Next());
                    currentVertex.Direction = adjacentReturn.DirectionOfMovement;
                    currentVertex = maze[adjacentReturn.X, adjacentReturn.Y];
                    //Debug.Log(currentVertex);
                    int firstInstance = -1;
                    for (int i = 0; i < currentPath.Count; i++)
                    {
                        if (currentPath.ElementAt(i).Equals(currentVertex))
                        {
                            firstInstance = i;
                        }
                    }
                    if (firstInstance != -1)
                    {
                        currentPath = new Queue<Vertex>(currentPath.Take(firstInstance + 1).ToArray());
                    }
                    else
                    {
                        currentPath.Enqueue(currentVertex);
                    }
                }

                Vertex firstVertexOnPath = currentPath.Dequeue();
                notVisited.Remove(firstVertexOnPath);
                firstVertexOnPath.setBoolOnDirectionToTrue(firstVertexOnPath.Direction);
                if (currentPath.Count != 0)
                {
                    Vertex previousVertex = firstVertexOnPath;
                    while (currentPath.Count > 1)
                    {
                        Vertex localVertex = currentPath.Dequeue();
                        notVisited.Remove(localVertex);
                        localVertex.setBoolOnOpositeDirectionToTrue(previousVertex.Direction);
                        localVertex.setBoolOnDirectionToTrue(localVertex.Direction);
                        previousVertex = localVertex;
                    }
                    Vertex lastVertexOnPath = currentPath.Dequeue();
                    lastVertexOnPath.Direction = previousVertex.Direction;
                    notVisited.Remove(lastVertexOnPath);
                    lastVertexOnPath.setBoolOnOpositeDirectionToTrue(previousVertex.Direction);
                }
            }
            return maze;
        }
    }
}
