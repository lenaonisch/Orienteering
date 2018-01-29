using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orienteering
{
    public interface IView
    {
        void PrintMap(Map map);
        void PrintCells(params Cell[] cells);
        void PrintNullCell(Coord coord); // print cell with background
        void PrintMessage(string format, params object[] args);
        void PrintError(string format, params object[] args);
        void PrintWarning(string format, params object[] args);
        bool GetYesNoAnswer(string format, params object[] args);
        void ShowHint(string hint, params object[] args);
        void ShowResults(GameResults gr);

        // for ConsoleView - contain calls of PrintMap + GetUserInput (endless loop for user's input)
        // for WinForms - contain PrintMap only + maybe smth else
        void LaunchNewGame(Map map); 

        //void GetUserInput();
        GameType GetNewGameType();
        MapParams GetMapParameters();

        bool exit { get; set; }
        //Game CurrentGame { get; set; }
        //SuperController Owner { get; set; }

        event GameControlDelegate EndGame;
        event GameControlDelegate StartGame;
        event ChangePositionDelegate MoveInitiated;

        void OnCheckpointTaken(object sender, CellsEventArgs args);
        void OnHiddenChkpFound(object sender, CellsEventArgs args);
        void OnPersonMoved(object sender, ChangePositionEventArgs args);
    }
}
