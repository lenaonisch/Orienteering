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
            Active = false;
        }

        public void CreateNewGame()
        {
            CreateNewGame(_view.GetNewGameType(), _view.GetMapParameters());
        }

        private void CreateNewGame(GameType gt, MapParams parameters)
        {
            switch (gt)
            {
                case GameType.Maze:
                    _game = new GameMaze();
                    break;
                case GameType.Orienteering:
                    _game = new GameOrienteering();
                    break;
                default:
                    break;
            }
            _game.InitNew(parameters);
            _view.CurrentGame = _game;
            _view.PrintMap(_game.Map);
            Active = true;
        }

        // returns true, if new game should be started after this one ended
        public void PlayTheGame()
        {
            _game.CheckpointWasTaken += _view.OnCheckpointTaken;
            _game.FoundSurrondingCheckpoint += _view.OnHiddenChkpFound;
            _game.Player.PlayerMoved += _view.OnPersonMoved;


            // SuperController is subscribed on both events from view & game
            _game.EndGame += ProcessNewRound;
            _view.EndGame += ProcessNewRound;
            /////
            _view.MoveInitiated += _game.MakeMove;
            
            do
            {  
                _view.GetUserInput();
            }
            while (Active);
        }

        public void ProcessNewRound(object sender, ref GameControlEventArgs args)
        {
            if (sender != null)
            {
                _view.OnGameEnded(sender, ref args);
            }
            if (args != null && args.StartNew)
            {
                if (_game != null)
                {
                    ////??????????
                    _view.MoveInitiated -= _game.MakeMove; 
                    _game.Player.PlayerMoved -= _view.OnPersonMoved;
                }
                CreateNewGame();
                PlayTheGame();
            }
            else  
            {
                Active = false;
            }
        }

        Game _game = null;
        IView _view = null;
        public bool Active { get; set; }
    }
}
