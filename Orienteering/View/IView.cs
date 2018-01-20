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
        void ReprintChangedCells(params Cell[] cells);
        void PrintMessage(string format, params object[] args);
        void PrintError(string format, params object[] args);
        void PrintWarning(string format, params object[] args);
        bool GetYesNoAnswer(string format, params object[] args);
        void ShowHint(string hint, params object[] args);
        
        Key GetUserInput();
        GameType GetNewGameType();
        MapParams GetMapParameters();

        Game CurrentGame { get; set; }

        void OnCheckpointTaken(object sender, CellsEventArgs args);
        void OnGameEnded(object sender, ref EndGameEventArgs args);
        void OnHiddenChkpFound(object sender, CellsEventArgs args);

        event EndGameDelegate EndGame;
    }
}
