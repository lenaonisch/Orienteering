using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orienteering;
using System.Windows.Input;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleView view = new ConsoleView(ConsoleView.DEFAULT_MAP_OFFSET);
            SuperController sc = new SuperController(view);
            
            do
            {
                sc.CreateNewGame(GameType.Orienteering);
                sc.PlayTheGame();
            }
            while (true);
            Console.ReadLine();
        }
    }
}
