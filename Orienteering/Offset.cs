using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orienteering
{
    public struct Offset
    {
        public int x, y;
        public Offset(int y, int x)
        {
            this.x = x;
            this.y = y;
        }

        private static Offset[] offsets = new Offset[] 
        { 
            new Offset(0,  0),
            new Offset(-1, 0), // N - 1
            new Offset(0,  1), // E - 2
            new Offset(-1, 1), // NE- 3
            new Offset(1,  0), // S - 4
            new Offset(0,  0), //no for 5
            new Offset(1,  1), // SE - 6
            new Offset(0,  0), //no for 7
            new Offset(0, -1), // W - 8
            new Offset(-1,-1), // NW - 9
            new Offset(0,  0), //no for 10
            new Offset(0,  0), //no for 11
            new Offset(1, -1), // SW - 12
        };

        public static Offset Get(Direction direction)
        {
            Offset offset = offsets[0];
            return offsets[(byte)direction];
        }

        public static Offset Get(Key key)
        {
            Offset offset = offsets[0];
            Direction direction = Direction.NoDirection;

            switch (key)
            {
                case Key.Down:
                    direction = Direction.South;
                    break;
                case Key.Up:
                    direction = Direction.North;
                    break;
                case Key.Left:
                    direction = Direction.West;
                    break;
                case Key.Right:
                    direction = Direction.East;
                    break;

            }
            return offsets[(byte)direction];
        }
    }
}
