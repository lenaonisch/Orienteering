using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public class Person : Cell
    {
        public const uint DEFAULT_VIEW_RADIUS = 5;

        #region ctors + Clone

        public Person(Map owner, Coord position, uint viewRadius = DEFAULT_VIEW_RADIUS)
            : base(owner, position, true)
        {
            ViewRadius = viewRadius;
        }

        public Person(Person p)
            : this(p._owner, p._position)
        { }

        public override Cell Clone()
        {
            return new Person(this);
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

        public void Move(Coord newCoord)
        {
            _owner[Position] = null;
            _position = newCoord;
            _owner[Position] = this;
        }
        #endregion

        #region properties

        public uint ViewRadius { get; set; }
        #endregion
    }
}
