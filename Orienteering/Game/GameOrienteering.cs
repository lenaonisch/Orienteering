using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public class GameOrienteering : Game
    {
        public GameOrienteering(IView view)
            :base(view)
        {
        }

        public override void OnGameEnded(object sender, ref EndGameEventArgs args)
        {
            _timer.Stop();
            
        }

        // return true if player changed his position
        public override bool MakeMove(Offset offset)
        {
            if (_timer == null) // first step
            {
                _timer = new System.Diagnostics.Stopwatch();
                _timer.Start();
            }

            Coord newCoord;
            bool moved = _player.CanMove(offset, out newCoord);
            if (moved) // move player if possible
            {
                if (_map.TakeCheckpoint(newCoord)) // take checkpoint on new player's position
                {
                    _chkpTaken(this, new CellsEventArgs((Checkpoint)_map[newCoord]));
                    _player.Move(newCoord);
                    if (_map.AreAllCheckpointsTaken())
                    {
                        EndGameEventArgs endArgs = new EndGameEventArgs(false);
                        _endGame(this, ref endArgs); // no restart, no interrupt
                        return true;
                    }
                }
                else
                {
                    _player.Move(newCoord);
                }
                Checkpoint[] surround;
                if (_map.AreCheckpointsClose(_player.Position, out surround, true, _player.ViewRadius)) // if some checkpoints are close - give possibility for player to know about it
                {
                    _chkpSurrondingFound(this, new CellsEventArgs(surround));
                }
            }
            return moved;
        }

        public override Game InitNew(MapParams parameters)
        {
            _map = Map.CreateRandom(parameters);
           _player = Map.PlacePlayer(_map);

            return this;
        }

        public override Game InitNew()
        {
            MapParams parameters = new MapParams();
            return InitNew(parameters);
        }
    }
}
