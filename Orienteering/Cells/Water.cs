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
            : base(owner, c, isVisible, type) 
        {
            CanBeCrossedBy = CrossingType.Rope;
        }
        public Water(Map owner, Coord[] area, bool isVisible = true, ObstacleType type = ObstacleType.River)
            :this(owner, area[0], isVisible, type)
        {
            _cells = new Cell[area.Length];
            owner[area[0]] = this;
            _cells[0] = this;
            for (uint i = 1; i < area.Length; i++)
            {
                owner[area[i]] = new Water(owner, area[i], isVisible, type);
                _cells[i] = owner[area[i]];
            }
        }
        public Water(Map owner, List<Coord> area, bool isVisible = true, ObstacleType type = ObstacleType.River)
            : this(owner, area[0], isVisible, type)
        {
            _cells = new Cell[area.Count];
            owner[area[0]] = this;
            _cells[0] = this;
            for (int i = 1; i < area.Count; i++)
            {
                owner[area[i]] = new Water(owner, area[i], isVisible, type);
                _cells[i] = owner[area[i]];
            }
        }

        public Water(Water w)
            : base(w._owner, w._position, w.Visible)
        {
            CanBeCrossedBy = w.CanBeCrossedBy;
        }
        public override Cell Clone()
        {
            return new Water(this);
        }
        
        #endregion

        public static void CreateRandom(Map map, uint square = DEFAULT_AREA, ObstacleType type = ObstacleType.River)
        {
            List<Coord> coordinates = new List<Coord>((int)square);
            coordinates.Add(map.GetRandomEmptyCell());

            Direction prevDirection = Direction.NoDirection;
            //uint lastfilledindex = 0;
            while (coordinates.Count < square - 1)
            {
                Direction newDirection = Randomizer.NextNotOppositeDirection(prevDirection);
                uint len = (uint)Randomizer.rand.Next(1, (int)(square - coordinates.Count - 1));

                List<Coord> ctemp = coordinates.Last().GetLineFromStart(newDirection, ref len, map.Size);
                coordinates.AddRange(ctemp);
                prevDirection = newDirection;
            }
            new Water(map, coordinates, true, type);
        }
    }
}
