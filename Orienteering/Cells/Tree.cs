using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    class Tree : Obstacle
    {
        public const byte DEFAULT_TREE_HEIGHT = 2;
        public const byte MAX_TREE_HEIGHT = 5;

        public Tree(Map owner, Coord coord, byte height = DEFAULT_TREE_HEIGHT)
            : base(owner, coord)
        {
            _square = 1;
            Type = ObstacleType.Tree;
            owner[coord] = this;
            Height = height;
        }

        public Tree(Tree t)
            : this(t._owner, t._position, t.Height)
        { }
        public override Cell Clone()
        {
            return new Tree(this);
        }

        public static void CreateRandom(Map map)
        {
            new Tree(map, map.GetRandomEmptyCell(), (byte)Randomizer.Next(1, MAX_TREE_HEIGHT));
        }

        public byte Height { get; set; }
    }
}
