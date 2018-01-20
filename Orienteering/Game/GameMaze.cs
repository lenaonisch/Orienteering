using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orienteering
{
    public class GameMaze : Game//, IGame
    {
        public GameMaze(IView view)
            : base(view)
        { }

        public int score = 0;

        //public bool MakeMove(Offset offset)
        //{
        //    bool res = true;
        //    Coord newCoord;

        //    if (Runner.Move(offset))
        //    {
                
        //    }

        //    Cell newCell = _map.CanGo(_player, offset, out newCoord);
        //    switch (newCell.Type)
        //    {
        //        case MapCellType.Checkpoint:
        //            Checkpoint checkpoint = _map.GetCheckpoint(newCoord);
        //            checkpoint.Taken = true;
        //            score += checkpoint.Price;
        //           // _map.Field[newCoord.y, newCoord.x].Type = MapCellType.Route;
        //            _map.TakenCheckpoints++;
        //           // newCell.Type = MapCellType.Route;
        //           // _player.position = newCoord;
        //            break;
        //        case MapCellType.Route:
        //        case MapCellType.Swamp:
        //        case MapCellType.NoFill:
        //            //Map.Field[Player.position.y, Player.position.x] = newCell;
        //          //  _player.position = newCoord;
        //            break;
        //        default:
        //            break;
        //    }
        //    return res;
        //}       

        ////public void PlayTheGame()
        ////{
        ////    //GameMaze game = new GameMaze();
            
            
        ////    do
        ////    {
        ////        _view.PrintMap(_map, ConsoleView.DEFAULT_MAP_OFFSET);
        ////        System.Threading.Thread.Sleep(20);
        ////        this.MakeMove(Offset.Get(_view.GetUserInput()));
        ////    }
        ////    while (!_map.AreAllCheckpointsTaken());
        ////    _view.PrintMap(_map, ConsoleView.DEFAULT_MAP_OFFSET);
        ////    _view.PrintMessage("Game is complete!");
        ////}

        //public bool InitNew(GameParams parameters)
        //{
        //    _map = MapContainer.CreateRandomMap(parameters.mapSize);
        //    _player = new Player(_map);
        //    return true;
        //}

        public override void OnGameEnded(object sender, ref EndGameEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override bool MakeMove(Offset offset/*object sender, ChangePositionEventArgs args*/)
        {
            throw new NotImplementedException();
        }

        public override Game InitNew(MapParams parameters)
        {
            throw new NotImplementedException();
        }

        public override Game InitNew()
        {
            throw new NotImplementedException();
        }

        public int GetScore
        {
            get { throw new NotImplementedException(); }
        }
    }
}
