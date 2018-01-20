using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public class Obstacle : Cell
    {
        #region ---- ctors + Clone -----
        //public Obstacle(Map owner, bool isVisible = true, ObstacleType type = ObstacleType.River)
        //    :base(owner, isVisible)
        //{
        //    Visible = isVisible;
        //    _type = type;
        //}

        public Obstacle(Map owner, Coord position, bool isVisible = true, ObstacleType type = ObstacleType.River)
            : base(owner, position, isVisible)
        {
            _type = type;
        }

        public Obstacle(Obstacle c)
            :this(c._owner, c._position, c.Visible)
        {
            
        }
        public override Cell Clone()
        {
            return new Obstacle(this);
        }
        #endregion

        #region indexators
        public Cell this[uint i]
        {
            get
            {
                return _owner[coordinates[i]];
            }
            set
            {
                _owner[coordinates[i]] = value;
            }
        }
        #endregion
        public bool HasIntersection(Obstacle otherObstacle)
        {
            return false;
        }

        public static Obstacle CreateRandom(Map map, byte square, ObstacleType type = ObstacleType.River)
        {
            Obstacle obstacle = null;
            switch (type)
            {
                case ObstacleType.River:
                case ObstacleType.Swamp:
                    obstacle = Water.CreateRandom(map, square, type);
                    break;
                case ObstacleType.Tree:
                    obstacle = Tree.CreateRandom(map);
                    break;
            }
            return obstacle;
        }

        protected static Coord[] CloneCellInLine(Direction direction, Coord curCoord, uint len, Coord maxSize)
        {
            Coord[] coordinates = new Coord[len];
            Offset offset = Offset.Get(direction);
            for (uint i = 0; i < len; i++)
            {
                int tmpx = (int)curCoord.x + offset.x;
                int tmpy = (int)curCoord.y + offset.y;
                if (tmpx < maxSize.x && tmpx >= 0 && tmpy < maxSize.y && tmpy >= 0)
                {
                    curCoord.x = (uint)tmpx;
                    curCoord.y = (uint)tmpy;
                    coordinates[i] = curCoord;
                }
                else
                {
                    Array.Resize(ref coordinates, (int)i);
                    break;
                }
            }
            return coordinates;
        }

//        protected void PlaceLine(MapContainer map, Direction direction, MapCellType type, uint len)
//        {
//            Coord[] newCoord = map.CloneCellInLine(direction, type, coordinates[lastfilledindex], len);
//            newCoord.CopyTo(coordinates, lastfilledindex + 1);
//            lastfilledindex += (uint)newCoord.Length;
//            Coord curCoord = coordinates[lastfilledindex];
//            Point offset = UI.offsets[(int)direction];
//            for (uint i = 0; i < len; i++)
//            {
//                int tmpx = (int)curCoord.x + offset.x;
//                int tmpy = (int)curCoord.y + offset.y;
//                if (tmpx < map.size.x && tmpx >= 0 && tmpy < map.size.y && tmpy >= 0)
//                {
//                    curCoord.x = (uint)tmpx;
//                    curCoord.y = (uint)tmpy;
//                    lastfilledindex++;
//                    coordinates[lastfilledindex] = curCoord;
//                    map.Field[curCoord.y, curCoord.x] = type;
//                }
//                else
//                {
//#if DEBUG
//                    UI.PrintText("max or 0 coord is reached!");
//                    i = len;
//#endif
//                }
//            }
//        }

        public Coord[] coordinates; // offsets of points
        protected uint _square;
        public ObstacleType _type
        {
            get;
            protected set;
        }
    }
}
