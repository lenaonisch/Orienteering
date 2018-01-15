using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public class Player : Cell
    {
        public const uint DEFAULT_VIEW_RADIUS = 1;

        #region ctors + Clone
        public Player(Map owner, uint viewRadius = DEFAULT_VIEW_RADIUS)
            : base (owner, true)
        {
            _position = owner.GetRandomEmptyCell();
            ViewRadius = viewRadius;
        }

        public Player(Map owner, Coord position)
            : base(owner, true)
        {
            _position.x = position.x;
            _position.y = position.y;
        }

        public Player(Player p)
            : this(p._owner, p._position)
        { }

        public override Cell Clone()
        {
            return new Player(this);
        }
        #endregion

        #region methods

        public bool CanMove(Offset offset, out Coord newCoord)
        {
            int newx = (int)_position.x + offset.x;
            int newy = (int)_position.y + offset.y;
            return CanMove(newy, newx, out newCoord);
        }

        public bool CanMove(int newy, int newx, out Coord newCoord)
        {
            bool ret = false;

            if (Validate(newy, newx) && !(_owner[(uint)newy, (uint)newx] is Obstacle))
            {
                ret = true;
                newCoord = new Coord((uint)newy, (uint)newx);
            }
            else
            {
                newCoord = _position;
            }
            return ret;
        }

        public bool Move(Offset offset)
        {
            Coord newCoord;
            if (CanMove(offset, out newCoord))
            {
                _position = newCoord;
                return true;
            }
            return false;
        }
        #endregion

        #region properties

        public uint ViewRadius { get; set; }
        #endregion
    }
}
