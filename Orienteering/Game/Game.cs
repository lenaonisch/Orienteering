﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Orienteering
{
    public abstract class Game
    {
        public virtual void OnGameEnded(object sender, ref GameControlEventArgs args)
        {
            _timer.Stop();
            args.SecondsPassed = _timer.ElapsedMilliseconds / 1000;
        }
        public abstract void MakeMove(object sender, ChangePositionEventArgs args);
        public abstract Game InitNew(MapParams parameters);
        public abstract Game InitNew();

        public Game(/*IView view*/)
        {
            //IsActive = true;
            //_view = view;
        }

        public Map Map
        {
            get { return _map; }
            set { _map = value; }
        }

        public Person Player
        {
            get { return _player; }
            set { _player = value; }
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
        protected Person        _player = null;
        protected Stopwatch     _timer = null;

        #region events
        public event CellsAffectedDelegate CheckpointWasTaken
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
        public event CellsAffectedDelegate FoundSurrondingCheckpoint
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
        
        protected CellsAffectedDelegate _chkpTaken;
        protected CellsAffectedDelegate _chkpSurrondingFound;
        
        //public event ChangePositionDelegate PlayerMoved
        //{
        //    add
        //    {
        //        _move += value;
        //    }
        //    remove
        //    {
        //        _move -= value;
        //    }
        //}

        //public ChangePositionDelegate _move;
        protected EndGameDelegate _endGame;
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
        #endregion
    }
}
