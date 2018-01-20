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
        public Water(Map owner, Coord[] area, bool isVisible = true, ObstacleType type = ObstacleType.River)
            :base(owner, area[0], isVisible, type)
        {
            coordinates = new Coord[area.Length];
            for (uint i = 0; i < area.Length; i++)
            {
                coordinates[i] = area[i];
                owner[coordinates[i]] = this[i];
            }
        }

        public Water(Water w)
            : base(w._owner, w._position, w.Visible)
        {

        }
        public override Cell Clone()
        {
            return new Water(this);
        }
        
        #endregion

        public static Water CreateRandom(Map map, uint square = DEFAULT_AREA, ObstacleType type = ObstacleType.River)
        {
            Coord[] coordinates = new Coord[square];
            coordinates[0] = map.GetRandomEmptyCell();

            Direction prevDirection = Direction.NoDirection;
            uint lastfilledindex = 0;
            while (lastfilledindex < square - 1)
            {
                Direction newDirection = Randomizer.NextDirection(prevDirection);
                uint len = (uint)Randomizer.rand.Next(1, (int)(square - lastfilledindex - 1));
                Coord[] ctemp = CloneCellInLine(newDirection, coordinates[lastfilledindex], len, map.size);
                Array.Copy(ctemp, 0, coordinates, lastfilledindex, ctemp.Length);
                lastfilledindex += (uint)ctemp.Length;
                prevDirection = newDirection;
            }
            return new Water(map, coordinates, true, type);
        }
    }
}
