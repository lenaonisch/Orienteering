using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public class GameMaze : Game
    {
        public override void MakeMove(object sender, ChangePositionEventArgs args)
        {
            if (_timer == null) // first step
            {
                _timer = new System.Diagnostics.Stopwatch();
                _timer.Start();
            }

            Coord newCoord = new Coord();
            bool moved = _player.CanMove(args.Offset, ref newCoord);
            if (moved) // move player if possible
            {
                if (_map.TakeCheckpoint(newCoord)) // take checkpoint on new player's position
                {
                    _chkpTaken(this, new CellsEventArgs((Checkpoint)_map[newCoord]));
                    if (_map.AreAllCheckpointsTaken())
                    {
                        GameControlEventArgs endArgs = new GameControlEventArgs();
                        _endGame(this, ref endArgs); // no restart, no interrupt
                        return;
                    }
                    
                }
                _player.Move(newCoord);
            }
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
