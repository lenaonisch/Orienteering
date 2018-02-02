using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    class Tree : Obstacle
    {
        //public Tree(Map owner)
        //    :base(owner)
        //{
        //    _square = 1;
        //    _type = ObstacleType.Tree;
        //}

        public Tree(Map owner, Coord coord)
            : base(owner, coord)
        {
            _square = 1;
            _type = ObstacleType.Tree;
            owner[coord] = this;
        }

        public Tree(Tree t)
            : this(t._owner, t._position)
        { }
        public override Cell Clone()
        {
            return new Tree(this);
        }

        public static void CreateRandom(Map map)
        {
            new Tree(map, map.GetRandomEmptyCell());
        }
    }
}
