using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public class Crossing : Cell
    {
        #region ctors + Clone
        private Crossing(Map owner, Coord c, CrossingType type)
            : base(owner, c) { Type = type; }
        public Crossing(Map owner, CrossingType type, params Coord[] coordinates)
            : base(owner, coordinates[0])
        {
            _cells = new Cell[coordinates.Length];
            for (uint i = 0; i < coordinates.Length; i++)
            {
                owner[coordinates[i]] = new Crossing(owner, coordinates[i], type);
                _cells[i] = owner[coordinates[i]];
            }
        }
        
        public Crossing(Crossing p)
            : base(p._owner, p._position)
        {
            Type = p.Type;
            _cells = new Cell[p._cells.Length];
            for (int i = 0; i< _cells.Length;i++)
            {
                _cells[i] = p._cells[i].Clone();
            }
        }

        public override Cell Clone()
        {
            return new Crossing(this);
        }
        #endregion

        public Cell[] _cells
        {
            get;
            protected set;
        }
        public CrossingType Type
        {
            get;
            protected set;
        }
    }
}
