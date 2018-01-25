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
        public ChangePositionEventArgs(Coord oldCoord, Cell newCell)
        {
            this.NewCell = newCell;
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
        public int Score { get; set; }
        public long SecondsPassed { get; set; }
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

    public delegate void EndGameDelegate(object sender, ref GameControlEventArgs args);
    public delegate void ChangePositionDelegate(object sender, ChangePositionEventArgs args);
    public delegate void CellsAffectedDelegate(object sender, CellsEventArgs args);
}
