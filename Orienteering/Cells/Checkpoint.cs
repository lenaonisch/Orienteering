using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public class Checkpoint : Cell
    {
        public const byte DEFAULT_PRICE = 1;

        #region ---- ctors + Clone -----
        public Checkpoint(Map owner, bool isVisible = true)
            :base(owner, isVisible)
        {
            _isVisible = isVisible;
        }

        public Checkpoint(Map owner, Coord position)
            : this(owner)
        {
            _position.x = position.x;
            _position.y = position.y;
        }

        public Checkpoint(Map owner, Coord position, bool isVisible = true, byte price = DEFAULT_PRICE, bool taken = false)
            :this(owner, position)
        {
            _price = price;
            _taken = taken;
            _isVisible = isVisible;
        }

        public Checkpoint(Checkpoint c)
            :this(c._owner, c._position, c._isVisible, c._price, c._taken)
        {
            
        }

        public override Cell Clone()
        {
            return new Checkpoint(this);
        }
        #endregion

        public bool Taken
        {
            get { return _taken; }
            set { _taken = value; }
        }
        public byte Price
        {
            get { return _price; }
        }

        private bool _taken;
        private byte _price;
    }
}
