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
        private Water(Map owner, Coord c, bool isVisible = true, ObstacleType type = ObstacleType.River)
            : base(owner, c, isVisible, type) { }
        public Water(Map owner, Coord[] area, bool isVisible = true, ObstacleType type = ObstacleType.River)
            :base(owner, area[0], isVisible, type)
        {
            cells = new Cell[area.Length];
            cells[0] = owner[area[0]];
            for (uint i = 1; i < area.Length; i++)
            {
                owner[area[i]] = new Water(owner, area[i], isVisible, type);
                cells[i] = owner[area[i]];
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

        public static void CreateRandom(Map map, uint square = DEFAULT_AREA, ObstacleType type = ObstacleType.River)
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
                Array.Copy(ctemp, 0, coordinates, lastfilledindex+1, ctemp.Length);
                lastfilledindex += (uint)ctemp.Length;
                prevDirection = newDirection;
            }
            new Water(map, coordinates, true, type);
        }
    }
}
