﻿using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public abstract class Cell
    {
        #region ---- ctors + Clone -----
        public Cell(Map owner, bool isVisible = true)
        {
            _owner = owner;
            _isVisible = isVisible;
        }

        public Cell(Map owner, Coord position, bool isVisible = true)
            : this(owner, isVisible)
        {
            _position.x = position.x;
            _position.y = position.y;
        }

        public Cell(Cell c)
            :this(c._owner, c._position, c._isVisible)
        {
            
        }

        public abstract Cell Clone();
        
        #endregion

        public bool Validate(int y, int x)
        {
            return (x > 0 && y > 0 && x < _owner.size.x && y < _owner.size.y);
        }

        
        public Coord Position
        {
            get { return _position; }
            set { _position = value; }
        }

        protected Coord _position;
        protected Map _owner;
        protected bool _isVisible;
    }
}