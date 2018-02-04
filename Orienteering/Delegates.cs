using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orienteering
{
    public class ChangePositionEventArgs : EventArgs
    {
        public Key Control { get; set; }
        public Offset Offset
        {
            get
            {
                return _offset;
            }
            
        }
        public Coord OldCoord { get; set; }
        public Cell OldCell { get; set; }
        public Cell NewCell { get; set; }

        Offset _offset;

        #region ctors
        public ChangePositionEventArgs()
        {

        }
        public ChangePositionEventArgs(Offset offset)
        {
            _offset = offset;
        }
        public ChangePositionEventArgs(Coord oldCoord, Cell oldCell, Cell newCell)
        {
            this.NewCell = newCell;
            this.OldCell = oldCell;
            this.OldCoord = oldCoord;
        }
        public ChangePositionEventArgs(Key key)
        {
            Key tmp;
            if (Enum.TryParse(key.ToString(), out tmp))
            {
                Control = tmp;
                _offset = Offset.Get(Control);
            }
        }
        #endregion
    }

    public class GameControlEventArgs : EventArgs
    {
        public bool StartNew { get; set; }
        public MapParams MapParameters { get; set; }
        public GameType NewGameType { get; set; }
        //public bool Aborted { get; set; } // true - if game was finished itself, false - user interrupted it on the middle
    }

    public class CellsEventArgs : EventArgs
    {
        public Cell[] _cells { get; private set; }
        public uint[] _len { get; set; }
        public Direction[] _direct { get; set; }
        public CellsEventArgs(params Cell[] cells)
        {
            _cells = new Cell[cells.Length];
            
            for (int i = 0; i < cells.Length; i++)
            {
                _cells[i] = cells[i];
            }
        }
    }

    public class CrossingEventArgs : EventArgs
    {
        public Direction Direct { get; set; }
        public uint MaxLen { get; set; }
        //public Coord StartPosition { get; set; }
        public CrossingType Type { get; set; }

        public CrossingEventArgs(Key key, CrossingType type = CrossingType.Rope)
        {
            Type = type;
            switch (key)
            {
                case Key.Down:
                    Direct = Direction.South;
                    break;
                case Key.Up:
                    Direct = Direction.North;
                    break;
                case Key.Left:
                    Direct = Direction.West;
                    break;
                case Key.Right:
                    Direct = Direction.East;
                    break;
                default:
                    Direct = Direction.NoDirection;
                    break;
            }
        }
    }

    public delegate void GameControlDelegate(object sender, ref GameControlEventArgs args);
    public delegate void ChangePositionDelegate(object sender, ChangePositionEventArgs args);
    public delegate void CellsAffectedDelegate(object sender, CellsEventArgs args);
    public delegate void CrossingDelegate(object sender, CrossingEventArgs args);
}
