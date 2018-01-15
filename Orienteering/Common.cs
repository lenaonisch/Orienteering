using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public struct Point
    {
        public int x, y;
        public Point(int y, int x)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct Coord
    {
        public uint x, y;

        public Coord(uint y, uint x)
        {
            this.x = x;
            this.y = y;
        }
    } 
}
