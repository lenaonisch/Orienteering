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
        public Obstacle(Map owner, Coord position, bool isVisible = true, ObstacleType type = ObstacleType.River)
            : base(owner, position, isVisible)
        {
            Type = type;
            CanBeCrossedBy = CrossingType.None;
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
                return _cells[i];
            }
            set
            {
                _cells[i] = value;
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
                    Water.CreateRandom(map, square, type);
                    break;
                case ObstacleType.Tree:
                    Tree.CreateRandom(map);
                    break;
            }
            return obstacle;
        }

        protected Cell[] _cells;
        protected uint _square;
        public ObstacleType Type
        {
            get;
            protected set;
        }
        public CrossingType CanBeCrossedBy
        {
            get;
            protected set;
        }
    }
}
