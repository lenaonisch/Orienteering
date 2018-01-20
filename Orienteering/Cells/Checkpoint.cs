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
        public Checkpoint(Map owner, Coord position, bool isVisible = true, byte price = DEFAULT_PRICE, bool taken = false)
            :base(owner, position, isVisible)
        {           
            _price = price;
            _taken = taken;
        }

        public Checkpoint(Checkpoint c)
            :this(c._owner, c._position, c.Visible, c._price, c._taken)
        {
            
        }

        public override Cell Clone()
        {
            return new Checkpoint(this);
        }
        #endregion

        public override string ToString()
        {
            return String.Format("({0}, {1}) with price {2}", _position.y, _position.x, _price);
        }

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
