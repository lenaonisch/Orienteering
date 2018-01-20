using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orienteering
{
    public class SuperController
    {
        public SuperController(IView view)
        {
            _view = view;
        }

        public void CreateNewGame(GameType gt)
        {
            MapParams mp = new MapParams();
            CreateNewGame(gt, mp);
            _view.Owner = (Game)_game;
            _view.PrintMap(_game.Map);
        }

        public void CreateNewGame(GameType gt, MapParams parameters)
        {
            switch (gt)
            {
                case GameType.Maze:
                    _game = new GameMaze(_view);
                    break;
                case GameType.Orienteering:
                    _game = new GameOrienteering(_view);
                    break;
                default:
                    break;
            }
            _game.InitNew(parameters);
        }

        // returns true, if new game should be started after this one ended
        public void PlayTheGame()
        {
            Key key;
            _game.CheckpointWasTaken += _view.OnCheckpointTaken;
            _game.FoundSurrondingCheckpoint += _view.OnHiddenChkpFound;

            // SuperController is subscribed on both events from view & game
            _game.EndGame += ProcessGameEnding;
            _view.EndGame += ProcessGameEnding;
            /////

            do
            {  
                key = _view.GetUserInput();
                switch (key)
                {
                    case Key.Up:
                    case Key.Down:
                    case Key.Left:
                    case Key.Right:
                        if (_game.MakeMove(Offset.Get(key)))
                        {
                            _view.PrintMap(_game.Map);
                        }
                        break;
                }
            }
            while (key != Key.Escape);

        }

        public void ProcessGameEnding(object sender, ref EndGameEventArgs args)
        {
 
        }

        Game _game = null;
        IView _view = null;

        
    }
}
