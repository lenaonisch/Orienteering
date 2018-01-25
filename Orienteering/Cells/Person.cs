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
            owner[position] = this;
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

        public bool CanMove(Offset offset, ref Coord newCoord)
        {
            int newx = (int)_position.x + offset.x;
            int newy = (int)_position.y + offset.y;
            return CanMove(newy, newx, ref newCoord);
        }

        public bool CanMove(int newy, int newx, ref Coord newCoord)
        {
            bool ret = false;

            if (Validate(newy, newx))
            {
                if (!(_owner[(uint)newy, (uint)newx] is Obstacle))
                {
                    ret = true;
                    newCoord = new Coord((uint)newy, (uint)newx);
                }
            
            }
            return ret;
        }

        public void Move(Coord newCoord)
        {
            Coord old = Position;
            _owner[Position] = null;
            _position = newCoord;
            _owner[Position] = this;
            _move(this, new ChangePositionEventArgs(old, this));
        }
        #endregion

        public event ChangePositionDelegate PlayerMoved
        {
            add
            {
                _move += value;
            }
            remove
            {
                _move -= value;
            }
        }

        public ChangePositionDelegate _move;

        #region properties

        public uint ViewRadius { get; set; }
        #endregion
    }
}
