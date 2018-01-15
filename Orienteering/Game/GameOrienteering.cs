using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public class GameOrienteering : Game//, IGame
    {
        public GameOrienteering(IView view)
            :base(view)
        {
            //_view.MakeMove += MakeMove;
        }

        public override void OnEnd(object sender, EndGameEventArgs args)
        {
            if (sender != this) // if game was interrupted from outside, outside interrupters should take care theirselves
            {
                _endGame = null;
            }
            _timer.Stop();
        }

        public override bool MakeMove(Offset offset/*object sender, ChangePositionEventArgs args*/)
        {
            
            if (_timer == null) // first step
            {
                _timer = new System.Diagnostics.Stopwatch();
                _timer.Start();
            }

            bool moved = _player.Move(offset);
            if (moved) // move player if possible
            {
                
                if (_map.TakeCheckpoint(_player.Position)) // take checkpoint on new player's position
                {
                    _chkpTaken(this, new CheckpointsEventArgs((Checkpoint)_map[_player.Position]));
                    if (_map.AreAllCheckpointsTaken())
                    {
                        EndGameEventArgs endArgs = new EndGameEventArgs(false, false);
                        OnEnd(this, endArgs); // summarize results, write results to history, etc...
                        _endGame(this, endArgs); // no restart, no interrupt
                        return true;
                    }
                }
                Checkpoint[] surround;
                if (_map.AreCheckpointsClose(_player.Position, out surround, true, _player.ViewRadius)) // if some checkpoints are close - give possibility for player to know about it
                {
                    _chkpSurrondingFound(this, new CheckpointsEventArgs(surround));
                }
            }
            return moved;
        }

        public override Game InitNew(MapParams parameters)
        {
            Game go = new GameOrienteering(_view);

            _map = (Map)Map.CreateRandom(parameters);
            _player = new Player(_map);
            return go;
        }

        public override Game InitNew()
        {
            MapParams parameters = new MapParams();
            return InitNew(parameters);
        }
    }
}
