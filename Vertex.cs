using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WilsonAlgorithm
{
    internal class Vertex
    {
        public Vertex(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public enum Directions
        {
            north,
            south,
            east,
            west

        };
        public Directions Direction { get; set; }
        public bool North { get; set; }
        public bool South { get; set; }
        public bool East { get; set; }
        public bool West { get; set; }

        public void setBoolOnDirectionToTrue(Directions direction)
        {
            switch (direction)
            {
                case Directions.north:
                    North = true;
                    break;
                case Directions.south:
                    South = true;
                    break;
                case Directions.east:
                    East = true;
                    break;
                case Directions.west:
                    West = true;
                    break;
            }
        }
        public void setBoolOnOpositeDirectionToTrue(Directions direction)
        {
            switch (direction)
            {
                case Directions.north:
                    South = true;
                    break;
                case Directions.south:
                    North = true;
                    break;
                case Directions.east:
                    West = true;
                    break;
                case Directions.west:
                    East = true;
                    break;
            }
        }
        public class AdjacentReturn
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Directions DirectionOfMovement { get; set; }
            public AdjacentReturn(int x, int y, Directions directionOfMovement)
            {
                X = x;
                Y = y;
                DirectionOfMovement = directionOfMovement;
            }
        }
        public AdjacentReturn giveAdjacent(int randomSeed)
        {
            System.Random random = new System.Random(randomSeed);
            Directions[] allDirections = (Directions[])Enum.GetValues(typeof(Directions));
            Directions randomDirection = (Directions)allDirections.GetValue(random.Next(4));
            bool viableAdjacent = false;
            while (!viableAdjacent)
            {
                randomDirection = (Directions)allDirections.GetValue(random.Next(4));
                viableAdjacent = true;
                if (X == 0 && randomDirection == Directions.west)
                {
                    viableAdjacent = false;
                }
                else if (X == Width - 1 && randomDirection == Directions.east)
                {
                    viableAdjacent = false;
                }
                else if (Y == Height - 1 && randomDirection == Directions.north)
                {
                    viableAdjacent = false;
                }
                else if (Y == 0 && randomDirection == Directions.south)
                {
                    viableAdjacent = false;
                }
            };
            AdjacentReturn adjacentReturn = new AdjacentReturn(X, Y, randomDirection);
            if (randomDirection == Directions.north)
            {
                adjacentReturn.Y++;
                //coordinates[1]--;
            }
            else if (randomDirection == Directions.south)
            {
                adjacentReturn.Y--;
                //coordinates[1]++;
            }
            else if (randomDirection == Directions.east)
            {
                adjacentReturn.X++;
                //coordinates[0]++;
            }
            else if (randomDirection == Directions.west)
            {
                adjacentReturn.X--;
                //coordinates[0]--;
            }
            return adjacentReturn;
        }
        public override string ToString()
        {
            String returnString = "[" + X + "," + Y + "] ";
            if (North)
                returnString += "N:T ";
            else
                returnString += "N:F ";
            if (South)
                returnString += "S:T ";
            else
                returnString += "S:F ";
            if (East)
                returnString += "E:T ";
            else
                returnString += "E:F ";
            if (West)
                returnString += "W:T ";
            else
                returnString += "W:F ";
            return returnString;
        }
    }
}