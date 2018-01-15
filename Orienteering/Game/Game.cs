using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Orienteering
{
    public abstract class Game
    {
        public abstract void OnEnd(object sender, EndGameEventArgs args);
        public abstract bool MakeMove(Offset offset);
        public abstract Game InitNew(MapParams parameters);
        public abstract Game InitNew();

        public Game(IView view)
        {
            _view = view;
        }

        public Map Map
        {
            get { return _map; }
            set { _map = value; }
        }

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public long ElapsedTime 
        {
            get
            {
                return _timer.ElapsedMilliseconds;
            }
        }

        public virtual int Score
        {
            get
            {
                int res = 0;
                foreach(Checkpoint chkp in _map.Checkpoints)
                {
                    if (chkp.Taken)
                    {
                        res += chkp.Price;
                    }
                }
                return res;
            }
        }

        protected Map           _map = null;
        protected Player        _player = null;
        protected IView         _view = null;
        protected Stopwatch     _timer = null;

        #region events
        public event CheckpointsDelegate CheckpointWasTaken
        {
            add
            {
                _chkpTaken += value;
            }
            remove
            {
                _chkpTaken -= value;
            }
        }
        public event CheckpointsDelegate FoundSurrondingCheckpoint
        {
            add
            {
                _chkpSurrondingFound += value;
            }
            remove
            {
                _chkpSurrondingFound -= value;
            }
        }
        public event EndGameDelegate EndGame
        {
            add
            {
                _endGame += value;
            }
            remove
            {
                _endGame -= value;
            }
        }
        protected CheckpointsDelegate _chkpTaken;
        protected CheckpointsDelegate _chkpSurrondingFound;
        protected EndGameDelegate _endGame;
        
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
        #endregion
    }
}
