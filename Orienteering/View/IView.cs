using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        MapParams MapParameters { get; set; }
        Game Owner { get; set; }
        System.Windows.Input.Key GetUserInput();

        //event ChangePositionDelegate MakeMove;
    }
}
