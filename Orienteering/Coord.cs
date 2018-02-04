using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public struct Coord
    {
        public uint x, y;

        public Coord(uint y, uint x)
        {
            this.x = x;
            this.y = y;
        }

        public List<Coord> GetLineFromStart(Direction direction, ref uint len, Coord maxSize)
        {
            List<Coord> coordinates = new List<Coord>((int)len);
            Offset offset = Offset.Get(direction);
            int tmpx = (int)x;
            int tmpy = (int)y; 
            for (uint i = 0; i < len; i++)
            {
                tmpx += offset.x;
                tmpy += offset.y;
                if (Validate(tmpy, tmpx, maxSize))
                {
                    coordinates.Add(new Coord((uint)tmpy, (uint)tmpx));
                }
                else
                {
                    len = i;
                    break;
                }
            }
            return coordinates;
        }

        public static bool Validate(int y, int x, Coord maxSize)
        {
            return (x >= 0 && y >= 0 && x < maxSize.x && y < maxSize.y);
        }

        public static bool Validate(uint y, uint x, Coord maxSize)
        {
            return (x < maxSize.x && y < maxSize.y);
        }

        public static Direction GetOppositeDirection(Direction d)
        {
            Direction ret = Direction.NoDirection;
            switch (d)
            {
                case Direction.East:
                    ret = Direction.West;
                    break;
                case Direction.West:
                    ret = Direction.East;
                    break;
                case Direction.North:
                    ret = Direction.South;
                    break;
                case Direction.South:
                    ret = Direction.North;
                    break;
                default:
                    ret = Direction.North;
                    break;
            }
            return ret;
        }
    }

    public class CoordOutOfMapException : Exception
    {
        public int x { get; set; }
        public int y { get; set; }
        public CoordOutOfMapException(string message)
            : base (message)
        {

        }
        public CoordOutOfMapException(string message, int y, int x)
            : base(message)
        {

        }
    }
}
