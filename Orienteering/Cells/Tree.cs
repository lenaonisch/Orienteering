using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    class Tree : Obstacle
    {
        public Tree(Map owner)
            :base(owner)
        {
            _square = 1;
            _type = ObstacleType.Tree;
        }

        public Tree(Map owner, Coord coord)
            : this(owner)
        {
            coordinates = new Coord[] {coord};
            owner[coord] = this;
        }

        public Tree(Tree t)
            : this(t._owner, t._position)
        { }
        public override Cell Clone()
        {
            return new Tree(this);
        }

        public static Tree CreateRandom(Map map)
        {
            return new Tree(map, map.GetRandomEmptyCell());
        }
    }
}
