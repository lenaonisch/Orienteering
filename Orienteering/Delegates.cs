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
        public Key control { get; set; }
        public Offset offset
        {
            get
            {
                return _offset;
            }
            
        }
        public Coord newCoord { get; set; }
        public Cell newCell { get; set; }

        Offset _offset;

        #region ctors
        public ChangePositionEventArgs()
        {

        }
        public ChangePositionEventArgs(Offset offset)
        {
            _offset = offset;
        }
        public ChangePositionEventArgs(Offset offset, Coord newCoord)
            : this(offset)
        {
            this.newCoord = newCoord;
        }

        public ChangePositionEventArgs(Offset offset, Coord newCoord, Cell newCell)
            : this(offset, newCoord)
        {
            this.newCell = newCell;
        }
        public ChangePositionEventArgs(Key key)
        {
            Key tmp;
            if (Enum.TryParse(key.ToString(), out tmp))
            {
                control = tmp;
                _offset = Offset.Get(control);
            }
        }
        #endregion
    }

    public class EndGameEventArgs : EventArgs
    {
        public bool Restart { get; set; }
        //public bool Aborted { get; set; } // true - if game was finished itself, false - user interrupted it on the middle
        public EndGameEventArgs(bool restart = true, bool aborted = false)
        {
            Restart = restart;
            //Aborted = aborted;
        }
    }

    public class CheckpointsEventArgs : EventArgs
    {
        public Checkpoint[] _checkpoints { get; private set; }
        public CheckpointsEventArgs(params Checkpoint[] ckps)
        {
            _checkpoints = new Checkpoint[ckps.Length];
            for (int i = 0; i < ckps.Length; i++)
            {
                _checkpoints[i] = ckps[i];
            }
        }
    }

    public class PlayerMovedEventArgs : EventArgs
    {

    }

    public delegate void EndGameDelegate(object sender, EndGameEventArgs args);
    public delegate void ChangePositionDelegate(object sender, ChangePositionEventArgs args);
    public delegate void CheckpointsDelegate(object sender, CheckpointsEventArgs args);
}
