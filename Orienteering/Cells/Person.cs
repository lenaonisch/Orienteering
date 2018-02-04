using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public class Person : Cell
    {
        public const byte DEFAULT_VIEW_RADIUS = 5;
        public const byte DEFAULT_ROPE_LENGTH = 5;

        #region ctors + Clone

        public Person(Map owner, Coord position, byte viewRadius = DEFAULT_VIEW_RADIUS, byte ropeLength = DEFAULT_ROPE_LENGTH)
            : base(owner, position, true)
        {
            ViewRadius = viewRadius;
            RopeLength = ropeLength;
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

            if (Coord.Validate(newy, newx, _owner.Size))
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
            Coord old = Position; // position before move
            Cell oldCell = currentCell == null? null: currentCell;
            _owner[Position] = currentCell;
            _position = newCoord;
            currentCell = _owner[newCoord];
            _owner[Position] = this;
            _move(this, new ChangePositionEventArgs(old, oldCell, this));
        }

        public void MakeCross(object sender, CrossingEventArgs args)
        {
            CrossingType type = args.Type;
            uint maxLen = RopeLength;
            ++maxLen; // to check last cell is not obstacle - it means crossing has sense
            List<Coord> coordinates = _position.GetLineFromStart(args.Direct, ref maxLen, _owner.Size);
            Obstacle first = _owner[coordinates[0]] as Obstacle;
            if (first != null && first.CanBeCrossedBy == type) // first cell to args.Direction from player's position 
            {
                int i;
                for (i = 1; i < coordinates.Count-1; i++)
                {
                    Cell cell = _owner[coordinates[i]];
                    if (!(cell is Obstacle))
                    {
                        break; // end point for crossing found
                    }
                    else 
                    {
                        if ((cell as Obstacle).CanBeCrossedBy == CrossingType.None)
                        {
                            return; // end point of crossing is against non-crossible obstacle - no sense to make crossing
                        }
                    }
                }

                if (!(_owner[coordinates[i]] is Obstacle)) // last cell (end point) should be not obstacle - player should be able to move on it! 
                {
                    Crossing c = new Crossing(_owner, type, coordinates.Take(i).ToArray());
                    _crossingCreated(this, new CellsEventArgs(c._cells));
                }
            }
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

        public event CellsAffectedDelegate CrossingCreated
        {
            add
            {
                _crossingCreated += value;
            }
            remove
            {
                _crossingCreated -= value;
            }
        }
        protected CellsAffectedDelegate _crossingCreated;

        #region properties
        // when person moves to other cell, this variable should be used to restore map
        // for example, when person walked through a crossing
        private Cell currentCell = null;
        public byte ViewRadius { get; protected set; }
        public byte RopeLength { get; protected set; }
        #endregion
    }
}
