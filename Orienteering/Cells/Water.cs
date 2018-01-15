using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    class Water : Obstacle
    {
        public const uint DEFAULT_AREA = 10;

        #region ---- ctors + Clone -----
        public Water(Map owner, bool isVisible = true, ObstacleType type = ObstacleType.River, uint area = DEFAULT_AREA)
            : base(owner, isVisible, type)
        {
            coordinates = new Coord[area];
            _type = type;
        }

        public Water(Map owner, Coord[] area, bool isVisible = true, ObstacleType type = ObstacleType.River)
            :base(owner)
        {
            coordinates = new Coord[area.Length];
            for (uint i = 0; i < area.Length; i++)
            {
                coordinates[i] = area[i];
                owner[coordinates[i]] = this[i];
            }
        }

        public Water(Water w)
            : base(w._owner, w._position, w._isVisible)
        {

        }
        public override Cell Clone()
        {
            return new Water(this);
        }
        
        #endregion

        public static Water CreateRandom(Map map, uint square = DEFAULT_AREA, ObstacleType type = ObstacleType.River)
        {
            Water newWat = new Water(map, true, ObstacleType.River, square);
            newWat.coordinates[0] = map.GetRandomEmptyCell();

            Direction prevDirection = Direction.NoDirection;
            uint lastfilledindex = 0;
            while (lastfilledindex < square - 1)
            {
                Direction newDirection = Randomizer.NextDirection(prevDirection);
                uint len = (uint)Randomizer.rand.Next(1, (int)(square - lastfilledindex - 1));
                lastfilledindex += newWat.CloneCellInLine(type, newDirection, newWat.coordinates[lastfilledindex], len);
                prevDirection = newDirection;
            }
            return newWat;
        }
    }
}
