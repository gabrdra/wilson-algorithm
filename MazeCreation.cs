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
            Random random = new Random(randomSeed);
            Vertex[,] maze = new Vertex[width, height];
            HashSet<Vertex> notVisited = new HashSet<Vertex>();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vertex newVertex = new Vertex(i,j);
                    maze[i, j] = newVertex;
                    notVisited.Add(newVertex);
                }
            }
            Vertex firstVertex = notVisited.ElementAt(random.Next(notVisited.Count));//this enumerates all elements so it's quite inefficient but I don't know any better way to do it
            notVisited.Remove(firstVertex);
            while (notVisited.Count > 0)
            {
                //Console.WriteLine("inside outer while");
                Vertex previousVertex = notVisited.ElementAt(random.Next(notVisited.Count));
                //notVisited.Remove(currentVertex);
                Queue<Vertex> currentPath = new Queue<Vertex>();
                currentPath.Enqueue(previousVertex);
                while (notVisited.Contains(previousVertex))
                {
                    //Console.WriteLine("inside inner while");
                    Vertex.AdjacentReturn adjacentReturn = previousVertex.giveAdjacent(random.Next(),width,height);
                    Vertex currentVertex = maze[adjacentReturn.X, adjacentReturn.Y];
                    currentVertex.Direction = adjacentReturn.DirectionOfMovement;
                    currentPath.Enqueue(currentVertex);
                    
                    for (int i = 0; i<currentPath.Count-1; i++)
                    {
                        if (currentPath.ElementAt(i).Equals(currentVertex))
                        {
                            currentPath = new Queue<Vertex>(currentPath.Take(i).ToArray());
                            break;
                        }
                    }
                    Console.WriteLine(notVisited.Count);
                    previousVertex = currentVertex;
                }
                
                Vertex firstVertexOnPath = currentPath.Dequeue();
                notVisited.Remove(firstVertexOnPath);
                firstVertexOnPath.setBoolOnDirectionToTrue();
                if (currentPath.Count != 0)
                {
                    while (currentPath.Count > 1)
                    {
                        Vertex currentVertex = currentPath.Dequeue();
                        notVisited.Remove(currentVertex);
                        currentVertex.setBoolOnDirectionAndOnOpositeToTrue();
                    }
                    Vertex lastVertexOnPath = currentPath.Dequeue();
                    notVisited.Remove(lastVertexOnPath);
                    lastVertexOnPath.setBoolOnOpositeDirectionToTrue();
                }
                
            }
            return maze;
        }
    }
}
